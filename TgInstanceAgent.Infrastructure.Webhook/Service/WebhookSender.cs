using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Events;
using TgInstanceAgent.Application.Abstractions.WebhookSender;
using TgInstanceAgent.Infrastructure.Webhook.Converters;

namespace TgInstanceAgent.Infrastructure.Webhook.Service;

/// <summary>
/// Класс, реализующий интерфейс IWebhookSender для отправки веб-хуков.
/// Также реализует интерфейс IDisposable для освобождения ресурсов.
/// </summary>
public class WebhookSender : IWebhookSender, IDisposable
{
    /// <summary>
    /// Объект подключения к очереди сообщений.
    /// </summary>
    private readonly IConnection _connection;

    /// <summary>
    /// Объект канала связи с очередью сообщений.
    /// </summary>
    private readonly IModel _channel;

    /// <summary>
    /// Клиент для отправки HTTP-запросов.
    /// </summary>
    private readonly HttpClient _httpClient = new();

    /// <summary>
    /// Объект-потребитель событий из очереди сообщений.
    /// </summary>
    private readonly EventingBasicConsumer _consumer;

    /// <summary>
    /// Настройки сериализации JSON.
    /// </summary>
    private readonly JsonSerializerSettings _serializerSettings;

    /// <summary>
    /// Формат имени для индивидуальных обменников веб-хуков.
    /// </summary>
    private const string IndividualExchange = "webhook_individual_{0}";

    /// <summary>
    /// Имя глобального обменника веб-хуков.
    /// </summary>
    private const string GlobalExchange = "webhook_global";

    /// <summary>
    /// Имя очереди для веб-хуков.
    /// </summary>
    private const string WebhookQueue = "webhook_queue";

    /// <summary>
    /// Имя обменника для мертвых веб-хуков.
    /// </summary>
    private const string DlxGlobalExchange = "dlx_webhook_global";

    /// <summary>
    /// Формат имени очередей для мертвых веб-хуков.
    /// </summary>
    private const string DlxWebhookQueue = "dlx_webhook_queue_{0}";

    /// <summary>
    /// Тег потребителя сообщений.
    /// </summary>
    private string? _consumerTag;

    /// <summary>
    /// Конструктор.
    /// </summary>
    /// <param name="connectionString">Строка соединения с RabbitMQ.</param>
    public WebhookSender(string connectionString)
    {
        // Создание настроек сериализатора JSON.
        _serializerSettings = new JsonSerializerSettings
        {
            // Игнорирование свойств с null-значениями.
            NullValueHandling = NullValueHandling.Ignore,
            
            // Отступ в отформаттированном JSON.
            Formatting = Formatting.Indented,
            
            // Преобразование имен свойств в camelCase.
            ContractResolver = new CamelCasePropertyNamesContractResolver(),
            
            // Преобразование enum в строку и добавление типа объекта в JSON.
            Converters = [new StringEnumConverter(), new TypeNameSerializationConverter()]
        };

        // Создание фабрики для подключения к RabbitMQ.
        var connectionFactory = new ConnectionFactory
        {
            // Установка адреса подключения из connectionString.
            Uri = new Uri(connectionString)
        };

        // Создание соединения с RabbitMQ.
        _connection = connectionFactory.CreateConnection();

        // Создание канала для работы с очередью RabbitMQ.
        _channel = _connection.CreateModel();

        // Создание потребителя сообщений из очереди RabbitMQ.
        _consumer = new EventingBasicConsumer(_channel);

        // Обработка полученных сообщений из очереди RabbitMQ.
        _consumer.Received += async (_, ea) => await HandleMessageAsync(ea);
    }

    /// <summary>
    /// Отправка события в обменник RabbitMQ.
    /// </summary>
    /// <param name="event">Событие для отправки.</param>
    /// <param name="url">URL-адрес для отправки веб-хука.</param>
    /// <param name="routing">Имя очереди.</param>
    public void PushToQueue(TgEvent @event, Uri url, string routing)
    {
        // Формирование имени обменника
        var exchangeName = string.Format(IndividualExchange, routing);

        // Объявление обменника RabbitMQ.
        _channel.ExchangeDeclare(exchangeName, ExchangeType.Direct, true, false, null);

        // Объявление обменника RabbitMQ.
        _channel.ExchangeBind(GlobalExchange, exchangeName, string.Empty, null);

        // Сериализация события в JSON.
        var json = JsonConvert.SerializeObject(@event, _serializerSettings);

        // Конвертация JSON в массив байт
        var body = Encoding.UTF8.GetBytes(json);

        // Создание свойств сообщения
        var properties = _channel.CreateBasicProperties();
        
        // Создание заголовков сообщения
        properties.Headers = new Dictionary<string, object>
        {
            
            // URL отправки вебхуков
            { "url", url.ToString() },
            
            // Идентификатор маршрутизации
            { "id", routing }
        };

        // Отправка сообщения в очередь RabbitMQ.
        _channel.BasicPublish(exchangeName, string.Empty, properties, body);
    }

    /// <summary>
    /// Запуск сервиса отправки веб-хуков.
    /// </summary>
    public void Start()
    {
        // Объявление обменника для мертвых веб-хуков
        _channel.ExchangeDeclare(DlxGlobalExchange, ExchangeType.Headers, true, false, null);

        // Объявление очередей с заданным TTL (время жизни сообщений) для мертвых вебхуков
        DeclareQueueWithTtl(10 * 60 * 1000, 1); // 10 минут
        DeclareQueueWithTtl(30 * 60 * 1000, 2); // 30 минут
        DeclareQueueWithTtl(60 * 60 * 1000, 3); // 1 час
        DeclareQueueWithTtl(12 * 60 * 60 * 1000, 4); // 12 часов
        DeclareQueueWithTtl(24 * 60 * 60 * 1000, 5); // 24 часа
        
        // Таким образом, при неудачной отправке вебхука, он попадет в обменник, где будет направлен в определенную очередь
        // с определенным TTL.
        // При первой неудачной отправке попадет в очередь с TTL 10 минут и вернется в главный обменник,
        // при второй - в очередь с TTL 30 минут и т.д.

        // Объявление глобального обменника веб-хуков
        _channel.ExchangeDeclare(GlobalExchange, ExchangeType.Direct, true, false, null);

        // Объявление очереди для веб-хуков
        _channel.QueueDeclare(WebhookQueue, true, false, false, null);

        // Привязка очереди для веб-хуков к глобальному обменнику веб-хуков
        _channel.QueueBind(WebhookQueue, GlobalExchange, string.Empty, null);

        // Начало потребления сообщений из очереди веб-хуков
        _consumerTag = _channel.BasicConsume(queue: WebhookQueue, autoAck: false, _consumer);
    }

    /// <summary>
    /// Остановка сервиса отправки веб-хуков.
    /// </summary>
    public void Stop() => _channel.BasicCancel(_consumerTag);

    /// <summary>
    /// Обработка входящего сообщения из очереди RabbitMQ и отправка веб-хука на указанный URL.
    /// </summary>
    /// <param name="deliverEventArgs">Данные о доставке сообщения.</param>
    private async Task HandleMessageAsync(BasicDeliverEventArgs deliverEventArgs)
    {
        // Проверка наличия URL в заголовке сообщения
        if (deliverEventArgs.BasicProperties.Headers["url"] is not byte[] url)
        {
            // Подтверждение обработки сообщения, если в нем не указан url
            _channel.BasicAck(deliverEventArgs.DeliveryTag, false);
            
            // Не продолжаем выполнение, так как url не задан
            return;
        }
        
        // Проверка наличия ID в заголовке сообщения
        if (deliverEventArgs.BasicProperties.Headers["id"] is not byte[] id)
        {
            // Подтверждение обработки сообщения, если в нем не указан url
            _channel.BasicAck(deliverEventArgs.DeliveryTag, false);
            
            // Не продолжаем выполнение, так как url не задан
            return;
        }

        // Преобразование тела сообщения в массив байтов
        var body = deliverEventArgs.Body.ToArray();

        // Преобразование массива байтов в JSON-строку
        var json = Encoding.UTF8.GetString(body);

        // Создание контента для отправки веб-хука
        var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
        
        // Добавляем заголовок с параметром маршрутизации (идентификатором инстанса)
        httpContent.Headers.Add("id", Encoding.UTF8.GetString(id));

        try
        {
            // Отправка веб-хука на указанный URL
            var response = await _httpClient.PostAsync(Encoding.UTF8.GetString(url), httpContent);

            // Проверка статуса ответа сервера
            if (!response.IsSuccessStatusCode)
            {
                // Переотправка сообщения, если статус ответа неуспешен
                Retry(deliverEventArgs);
            }
            else
            {
                // Подтверждение обработки сообщения
                _channel.BasicAck(deliverEventArgs.DeliveryTag, false);
            }
        }
        catch (Exception ex)
        {
            // Переотправка сообщения при возникновении исключения
            Retry(deliverEventArgs, ex);
        }
    }
    
    /// <summary>
    /// Отправка сообщения в обменник мертвых вебхуков в случае неуспешной отправки веб-хука.
    /// </summary>
    /// <param name="deliverEventArgs">Данные о доставке сообщения.</param>
    /// <param name="ex">Исключение, возникшее при отправке веб-хука (по умолчанию null).</param>
    private void Retry(BasicDeliverEventArgs deliverEventArgs, Exception? ex = null)
    {
        // Создание свойств сообщения
        var properties = _channel.CreateBasicProperties();
        
        // Инициализация словаря заголовков для свойств сообщения
        properties.Headers = new Dictionary<string, object>();

        // Копирование заголовка URL из исходного сообщения
        if (deliverEventArgs.BasicProperties.Headers.TryGetValue("url", out var url))
        {
            properties.Headers["url"] = url;
        }

        // Копирование заголовка идентификатора из исходного сообщения
        if (deliverEventArgs.BasicProperties.Headers.TryGetValue("id", out var id))
        {
            properties.Headers["id"] = id;
        }

        // Увеличение счетчика повторов. Если есть такой заголовок
        if (deliverEventArgs.BasicProperties.Headers.TryGetValue("retry-count", out var value))
        {
            // Получаем текущее число повторов
            var retryCount = (int)value;
            
            // Устанавливаем заголовок с числом повторов больше на 1
            properties.Headers["retry-count"] = retryCount + 1;
        }
        else
        {
            // Устанавливаем заголовок с числом повторов как 1
            properties.Headers["retry-count"] = 1;
        }

        // Добавление сообщения об исключении в заголовки, если оно есть
        if (ex != null)
        {
            properties.Headers["exception"] = ex.Message;
        }

        // Отправка сообщения в обменник мертвых вебхуков RabbitMQ.
        _channel.BasicPublish(DlxGlobalExchange, string.Empty, properties, deliverEventArgs.Body);

        // Подтверждение получения сообщения (чтоб оно ушло из очереди вебхуков)
        _channel.BasicAck(deliverEventArgs.DeliveryTag, false);
    }

    /// <summary>
    /// Объявление очереди с заданным TTL (время жизни сообщений) и счетчиком повторов.
    /// </summary>
    /// <param name="ttl">Время жизни сообщений в очереди в миллисекундах.</param>
    /// <param name="retryCount">Счетчик повторов.</param>
    private void DeclareQueueWithTtl(int ttl, int retryCount)
    {
        // Формирование имени очереди на основе TTL
        var queueName = string.Format(DlxWebhookQueue, ttl);
        
        // Создание аргументов для очереди
        var queueArguments = new Dictionary<string, object>
        {
            // Установка обменника для мертвой очереди
            { "x-dead-letter-exchange", GlobalExchange },
            
            // Установка TTL для сообщений в очереди.
            // Через указанное время вебхук в этой очереди будет помечен мертвым и отправлен в "x-dead-letter-exchange"
            { "x-message-ttl", ttl }
        };

        // Объявление очереди
        _channel.QueueDeclare(queueName, true, false, false, queueArguments);

        // Создание аргументов для привязки очереди
        var bindArguments = new Dictionary<string, object>
        {
            // Установка типа сопоставления заголовков
            { "x-match", "any" },
            
            // Установка, что в данную очередь вебхук попадает при определенном заданном числе неудачных попыток отправки
            { "retry-count", retryCount }
        };

        // Привязка очереди к обменнику мертвой очереди глобальных веб-хуков
        _channel.QueueBind(queueName, DlxGlobalExchange, string.Empty, bindArguments);

    }

    /// <summary>
    /// Освобождение ресурсов.
    /// </summary>
    public void Dispose()
    {
        // Подавляем вызов финализатора для текущего объекта. 
        GC.SuppressFinalize(this);

        // Уничтожаем соединение
        _connection.Dispose();
        
        // Уничтожаем канал связи
        _channel.Dispose();
        
        // Уничтожаем HttpClient
        _httpClient.Dispose();
    }
}