using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using TgInstanceAgent.Application.Abstractions.Exceptions;
using TgInstanceAgent.Application.Abstractions.InstancesService;
using TgInstanceAgent.Application.Abstractions.InstancesService.Exceptions;
using TgInstanceAgent.Application.Abstractions.ReportService;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Events;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Exceptions;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Functions.Components;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Functions.Proxy;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Interfaces;
using TgInstanceAgent.Application.Services.Extensions;
using TgInstanceAgent.Domain.Abstractions.Repositories;
using TgInstanceAgent.Domain.Instances;
using TgInstanceAgent.Domain.Instances.Enums;
using TgInstanceAgent.Domain.Instances.Exceptions;
using TgInstanceAgent.Domain.Instances.Specifications;
using TgInstanceAgent.Domain.Instances.Specifications.Visitor;
using TgInstanceAgent.Domain.Instances.ValueObjects;
using TgInstanceAgent.Domain.Specifications;
using TgInstanceAgent.Domain.SystemProxy.Enums;
using TgInstanceAgent.Domain.SystemProxy.Ordering;
using TgInstanceAgent.Domain.SystemProxy.Specifications;

namespace TgInstanceAgent.Application.Services.InstancesService;

/// <summary> 
/// Сервис для работы с инстансами Telegram-клиента. 
/// </summary> 
/// <param name="instanceRepository">Репозиторий для работы с инстансами.</param>
/// <param name="systemProxyRepository">Репозиторий для работы с системными прокси.</param> 
/// <param name="factory">Фабрика для создания Telegram-клиента.</param> 
public class InstancesService(
    IInstanceRepository instanceRepository,
    ISystemProxyRepository systemProxyRepository,
    IMemoryCache cache,
    ITelegramClientFactory factory,
    IWebhookService webhook,
    IAutoForwardService autoForward,
    IReportService report,
    ILogger<IInstancesService> logger,
    bool systemProxyEnabled)
    : IInstancesService, IAsyncDisposable
{
    /// <summary> 
    /// Словарь, хранящий информацию об Telegram-клиентах инстансов. 
    /// </summary> 
    private readonly Dictionary<Guid, ITelegramClient> _services = new();

    /// <summary>
    /// Семафор, используется для гарантии, что не будет создано больше одного телеграм клиента для инстанса
    /// </summary>
    private readonly SemaphoreSlim _semaphore = new(1, 1);

    /// <summary> 
    /// Флаг, указывающий, готов ли сервис к использованию. 
    /// </summary> 
    private bool _isReady = true;

    /// <summary>
    /// Запуск всех инстансов асинхронно.
    /// </summary>
    /// <param name="token">Токен отмены</param>
    public async Task StartAsync(CancellationToken token)
    {
        // Спецификация не истекших инстансов
        var notExpiredSpecification = new NotSpecification<InstanceAggregate, IInstanceSpecificationVisitor>(
            new ExpiredInstancesSpecification());

        // Спецификация включенных инстансов
        var enabledSpecification = new EnabledInstancesSpecification();

        // Спецификация включенных инстансов
        var authenticatedSpecification = new AuthenticatedInstancesSpecification();

        // Объединенная спецификация из Enabled и NotExpired
        var compoundSpecification =
            new AndSpecification<InstanceAggregate, IInstanceSpecificationVisitor>(enabledSpecification,
                notExpiredSpecification);

        // Объединенная спецификация из Enabled и NotExpired и Authenticated
        compoundSpecification =
            new AndSpecification<InstanceAggregate, IInstanceSpecificationVisitor>(compoundSpecification,
                authenticatedSpecification);

        // Асинхронно выполняем поиск инстансов
        var instances = await instanceRepository.FindAsync(compoundSpecification);

        // Ждем захвата семафора
        await _semaphore.WaitAsync(token);

        // Запускаем инстансы асинхронно
        await Parallel.ForEachAsync(instances, token, async (i, ct) => await StartInstanceAsync(i, ct));

        // Устанавливаем флаг _isReady в значение true. 
        _isReady = true;

        // Освобождаем семафор
        _semaphore.Release();
    }

    /// <summary>
    /// Остановка всех инстансов асинхронно.
    /// </summary>
    /// <param name="token">Токен отмены</param>
    public async Task StopAsync(CancellationToken token)
    {
        // Устанавливаем, что сервис больше не готов к работе
        _isReady = false;

        // Ждем захвата семафора
        await _semaphore.WaitAsync(token);

        // Останавливаем все инстансы асинхронно
        await Parallel.ForEachAsync(_services, token, async (i, ct) =>
        {
            // Вызываем исключение, если требуется остановка
            ct.ThrowIfCancellationRequested();

            // Уничтожаем клиент
            await i.Value.DisposeAsync();
        });

        // Чистим коллекцию сервисов
        _services.Clear();

        // Ждем захвата семафора
        _semaphore.Release();
    }

    /// <inheritdoc/>
    /// <summary>
    /// Запуск клиента асинхронно.
    /// </summary>
    public async Task<ITelegramClient> StartClientAsync(Guid instanceId, CancellationToken token)
    {
        // Проверяем, готов ли сервис. Если нет, выбрасываем исключение InstanceServiceNotReadyException.
        if (!_isReady) throw new InstanceServiceNotReadyException();

        // Ждем захвата семафора
        await _semaphore.WaitAsync(token);

        try
        {
            // Получаем инстанс
            var instance = await cache.GetInstanceAsync(instanceId, instanceRepository, token);

            // Если инстанс просрочен, выбрасываем исключение InstanceExpiredException.
            if (instance.IsExpired) throw new InstanceExpiredException();

            // Пытаемся получить запись об инстансе из словаря services, используя предоставленный instanceId.
            if (_services.TryGetValue(instanceId, out var telegramClient))
            {
                // Инстанс уже запущен (так как клиент есть в словаре).
                // Обновляем инстансу системную прокси если это необходимо.
                await SetInstanceProxyIfNeedAsync(telegramClient, instance, token);
                
                // Возвращаем уже созданный клиент
                return telegramClient;
            }

            // Если инстанс не включен
            if (!instance.Enabled)
            {
                // Помечаем инстанс включенным
                instance.Enabled = true;

                // Обновляем данные в репозитории
                await instanceRepository.UpdateAsync(instance);
            }

            // Запускаем инстанс асинхронно и возвращаем результат.
            return await StartInstanceAsync(instance, token);
        }
        finally
        {
            // Освобождаем семафор
            _semaphore.Release();
        }
    }

    /// <summary>
    /// Остановка клиента асинхронно.
    /// </summary>
    /// <param name="instanceId">Идентификатор инстанса</param>
    /// <param name="token">Токен отмены операции.</param> 
    public async Task StopClientAsync(Guid instanceId, CancellationToken token)
    {
        // Ждем захвата семафора
        await _semaphore.WaitAsync(token);

        try
        {
            // Пытаемся получить запись об инстансе из словаря services, используя предоставленный instanceId. 
            // Если не найдено, возвращаем завершенную задачу Task.CompletedTask. 
            if (!_services.TryGetValue(instanceId, out var telegramClient)) return;

            // Освобождаем ресурсы, связанные с клиентом инстанса. 
            await telegramClient.DisposeAsync();

            // Удаляем запись об инстансе из словаря services. 
            _services.Remove(instanceId, out _);

            // Получаем инстанс
            var instance = await cache.GetInstanceAsync(instanceId, instanceRepository, token);

            // Помечаем инстанс выключенным
            instance.Enabled = false;

            // Обновляем данные в репозитории
            await instanceRepository.UpdateAsync(instance);
        }
        finally
        {
            // Освобождаем семафор
            _semaphore.Release();
        }
    }

    /// <summary> 
    /// Запускает новый инстанс Telegram-клиента асинхронно. 
    /// </summary> 
    /// <param name="instanceAggregate">Экземпляр, который нужно запустить.</param> 
    /// <param name="token">Токен отмены операции.</param> 
    /// <returns>Объект сервиса Telegram-клиента.</returns> 
    private async Task<ITelegramClient> StartInstanceAsync(InstanceAggregate instanceAggregate, CancellationToken token)
    {
        // Создаем инстанс сервиса, используя фабричный метод FactoryMethod и формируя имя инстанса на основе его Id. 
        var client = factory.FactoryMethod($"instance-{instanceAggregate.Id}");

        // Подписываемся на событие NewEvent сервиса и асинхронно обрабатываем события. 
        client.NewEvent += async @event => { await EventHandler(@event, instanceAggregate.Id, client); };

        // Устанавливаем инстансу системную прокси если это необходимо
        await SetInstanceProxyIfNeedAsync(client, instanceAggregate, token);

        // Устанавливаем соединение с сервисом асинхронно, используя предоставленный токен. 
        await client.ConnectAsync();

        // Добавляем запись об инстансе в словарь services, где ключом является Id инстанса, а значением - кортеж с инстансом и клиентом сервиса. 
        _services[instanceAggregate.Id] = client;

        // Возвращаем объект сервиса. 
        return client;
    }

    /// <summary>
    /// Устанавливает прокси агрегату инстанса
    /// </summary>
    /// <param name="service">Клиент Telegram для инстанса.</param>
    /// <param name="instanceAggregate">Экземпляр, который нужно запустить.</param> 
    /// <param name="token">Токен отмены операции.</param> 
    private async Task SetInstanceProxyIfNeedAsync(ITelegramClient service, InstanceAggregate instanceAggregate,
        CancellationToken token)
    {
        // Если функционал системных прокси выключен или инстанс не нуждается в установке - ничего не делаем
        if (!systemProxyEnabled || !instanceAggregate.NeedEnableSystemProxy) return;

        // Получаем прокси, время жизни которых более 24 часа
        var systemProxies = await systemProxyRepository.FindAsync(new SystemProxyAvailableSpecification(),
            new SystemProxyOrderByInstancesCount(), 0, 1);

        // Если не удалось найти системную прокси - вызываем исключение
        if (systemProxies.Count == 0) throw new NoAvailableSystemProxyException();

        // Выбираем наименее нагруженную прокси
        var proxy = systemProxies.First();
        
        // Определяем тип прокси на основе значения свойства Type объекта proxy
        TgInputProxyType type = proxy.Type switch
        {
            // Если тип прокси HTTP, создаем объект TgInputProxyTypeHttp с логином, паролем и флагом HttpOnly
            ProxyType.Http => new TgInputProxyTypeHttp { Username = proxy.Login, Password = proxy.Password, HttpOnly = true },
            
            // Если тип прокси HTTPS, создаем объект TgInputProxyTypeHttp с логином и паролем
            ProxyType.Https => new TgInputProxyTypeHttp { Username = proxy.Login, Password = proxy.Password },
            
            // Если тип прокси SOCKS, создаем объект TgInputProxyTypeSocks с логином и паролем
            ProxyType.Socks => new TgInputProxyTypeSocks { Username = proxy.Login, Password = proxy.Password },
            
            // Если тип прокси неизвестен, выбрасываем исключение ArgumentOutOfRangeException
            _ => throw new ArgumentOutOfRangeException("Unknown proxy type", innerException: null)
        };

        // Устанавливаем прокси агрегату
        await service.SetProxyAsync(new TgSetProxyRequest
        {
            // Устанавливаем хост
            Server = proxy.Host,

            // Устанавливаем порт
            Port = proxy.Port,

            // Устанавливаем тип прокси
            ProxyType = type
            
        }, token);
        
        // Устанавливаем агрегату прокси
        instanceAggregate.SystemProxy = new SystemProxy
        {
            ProxyId = proxy.Id,
            SetTime = DateTime.UtcNow
        };
        
        // Обновляем агрегат в базе данных
        await instanceRepository.UpdateAsync(instanceAggregate);
    }


    /// <summary>
    /// Обработчик событий для WaEvent.
    /// </summary>
    /// <param name="event">Событие.</param>
    /// <param name="instanceId">Идентификатор экземпляра.</param>
    /// <param name="client">Клиент WhatsApp.</param>
    /// <returns>Задача.</returns>
    private async Task EventHandler(TgEvent @event, Guid instanceId, ITelegramClient client)
    {
        // Если событие является WaNewMessageEvent.
        if (@event is TgNewMessageEvent newMessageEvent)
        {
            try
            {
                // Пытаемся переслать сообщение, если нужно.
                var message = await autoForward.ForwardIfNeededAsync(client, instanceId, newMessageEvent.Message);

                // Если сообщение было отправлено
                if (message != null)
                {
                    // Создаем событие автопересылки
                    var autoForwardedEvent = new TgMessageAutoForwardedEvent
                        { SourceMessage = newMessageEvent.Message, NewMessage = message };

                    // Отправляем веб-хук, о сообщении если нужно.
                    await webhook.PublishWebhookIfNeededAsync(instanceId, autoForwardedEvent);
                }

                // Добавляем сообщение в отчёт
                await report.ProcessMessage(instanceId, newMessageEvent.Message);
            }
            catch (ClientException ex)
            {
                // Логгируем исключение, если не удалось переслать сообщение.
                logger.LogError("Не удалось переотправить сообшение {e}", ex);
            }
        }
        // Если событие является WaAuthenticatedEvent.
        else if (@event is TgAuthenticatedEvent)
        {
            try
            {
                // Отмечаем инстанс авторизованным.
                await AuthenticatedAsync(instanceId);
            }
            catch (Exception ex)
            {
                // Логгируем исключение, если не удалось пометить инстанс.
                logger.LogError("Не удалось авторизовать инстанс {e}", ex);
            }
        }
        // Если событие является WaLoggedOutEvent.
        else if (@event is TgLoggedOutEvent)
        {
            try
            {
                // Отмечаем инстанс неавторизованным.
                await LoggedOutAsync(instanceId);
            }
            catch (Exception ex)
            {
                // Логгируем исключение, если не удалось деавторизовать экземпляр.
                logger.LogError("Не удалось деавторизовать инстанс {e}", ex);
            }
        }

        try
        {
            // Отправляем веб-хук, если нужно.
            await webhook.PublishWebhookIfNeededAsync(instanceId, @event);
        }
        catch (Exception ex)
        {
            // Логгируем исключение, если не удалось отправить веб-хук.
            logger.LogError("Не удалось отправить вебхук {e}", ex);
        }
    }

    /// <summary> 
    /// Обрабатывает событие входа из системы для указанного инстанса Telegram-клиента асинхронно. 
    /// </summary> 
    /// <param name="id">Id инстанса.</param> 
    private async Task AuthenticatedAsync(Guid id)
    {
        // Получаем инстанс
        var instance = await cache.GetInstanceAsync(id, instanceRepository);

        // Устанавливаем состояние инстанса на "Authorized". 
        instance.State = State.Authenticated;

        // Асинхронно обновляем информацию об инстансе в репозитории. 
        await instanceRepository.UpdateAsync(instance);
    }

    /// <summary> 
    /// Обрабатывает событие выхода из системы для указанного инстанса Telegram-клиента асинхронно. 
    /// </summary> 
    /// <param name="id">Id инстанса.</param>
    private async Task LoggedOutAsync(Guid id)
    {
        // В отличие от WhatsApp, клиент не может продолжать работу после выхода с устройства,
        // поэтому мы полностью выключаем инстанс при выходе

        // Проверяем, существует ли запись об инстансе в словаре services, используя предоставленный id. 
        // Если запись найдена, выполняем следующие действия. 
        if (_services.TryGetValue(id, out var telegramClient))
        {
            // Получаем инстанс
            var instance = await cache.GetInstanceAsync(id, instanceRepository);

            // Устанавливаем состояние инстанса на "NotAuthorized". 
            instance.State = State.NotAuthenticated;

            // Выключаем инстанс
            instance.Enabled = false;

            // Асинхронно обновляем информацию об инстансе в репозитории. 
            await instanceRepository.UpdateAsync(instance);

            // Освобождаем ресурсы, связанные с клиентом инстанса. 
            await telegramClient.DisposeAsync();

            // Удаляем запись об инстансе из словаря services. 
            _services.Remove(id, out _);
        }
    }

    /// <summary> 
    /// Освобождает ресурсы клиентов. 
    /// </summary> 
    public async ValueTask DisposeAsync()
    {
        // Подавляем вызов финализатора для текущего объекта. 
        GC.SuppressFinalize(this);

        // Уничтожаем семафор.
        _semaphore.Dispose();

        // Перебираем все записи в словаре services.
        await Parallel.ForEachAsync(_services, async (i, _) =>
        {
            // Уничтожаем клиент
            await i.Value.DisposeAsync();
        });
    }
}