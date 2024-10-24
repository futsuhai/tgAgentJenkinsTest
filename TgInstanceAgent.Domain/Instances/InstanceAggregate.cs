using TgInstanceAgent.Domain.Abstractions;
using TgInstanceAgent.Domain.Instances.Enums;
using TgInstanceAgent.Domain.Instances.Exceptions;
using TgInstanceAgent.Domain.Instances.ValueObjects;

namespace TgInstanceAgent.Domain.Instances;

/// <summary>
/// Агрегат InstanceAggregate  
/// </summary>
public class InstanceAggregate(Guid id, DateTime expirationTimeUtc, Guid userId) : AggregateRoot(id)
{
    /// <summary>
    /// Коллекция пересылки сообщений
    /// </summary>
    private readonly List<ForwardEntry> _forwardEntries = [];

    /// <summary>
    /// Доступ к коллекции пересылки сообщений в виде только для чтения.
    /// </summary>
    public IReadOnlyCollection<ForwardEntry> ForwardEntries => _forwardEntries.AsReadOnly();

    /// <summary>
    /// Максимальное кол-во сообщений для инстанса
    /// </summary>
    private const int MaxMessageCount = 100;

    /// <summary>
    /// Максимальное кол-во загрузок файлов для инстанса
    /// </summary>
    private const int MaxFileDownloadCount = 5;

    /// <summary>
    /// Идентификатор пользователя.
    /// При отправке запроса по идентификатору, необходимо быть уверенным, что приложению известен данный пользователь.
    /// </summary>
    public Guid UserId { get; } = userId;

    /// <summary>
    /// Время истечения срока действия в формате UTC.
    /// </summary>
    public DateTime ExpirationTimeUtc { get; private set; } = expirationTimeUtc;

    /// <summary>
    /// Состояние инстанса.
    /// </summary>
    public State State { get; set; } = State.NotAuthenticated;

    /// <summary>
    /// Включен ли инстанс
    /// </summary>
    public bool Enabled { get; set; }

    /// <summary>
    /// Коллекция ссылок
    /// </summary>
    private readonly List<Uri> _urls = [];

    /// <summary>
    /// Доступ к коллекции ссылок в виде только для чтения.
    /// </summary>
    public IReadOnlyCollection<Uri> WebhookUrls => _urls.AsReadOnly();

    /// <summary>
    /// Прокси
    /// </summary>
    private Proxy? _proxy;

    /// <summary>
    /// Прокси-сервер.
    /// </summary>
    public Proxy? Proxy
    {
        get => _proxy;
        set
        {
            if (value?.IsExpired ?? false) throw new ProxyExpiredException();
            
            // Устанавливаем прокси
            _proxy = value;
            
            // Обнуляем системную прокси
            SystemProxy = null;
        }
    }
    
    /// <summary>
    /// Системное прокси.
    /// </summary>
    public ValueObjects.SystemProxy? SystemProxy { get; set; }

    /// <summary>
    /// Ограничения
    /// </summary>
    public Restrictions Restrictions { get; private set; } = new();

    /// <summary>
    /// Настройки вебхука.
    /// Определяет, какие данные будут передаваться через вебхук, включая сообщения, чаты, группы и другие данные.
    /// Значения по умолчанию установлены на <c>true</c> для всех типов данных, что означает, что все типы данных будут передаваться через вебхук по умолчанию.
    /// </summary>
    public WebhookSetting WebhookSetting { get; set; } = new()
    {
        Messages = true,
        Chats = true,
        Users = true,
        Files = true,
        Other = true,
        Stories = true
    };

    /// <summary>
    /// Признак истечения срока действия.
    /// </summary>
    public bool IsExpired => ExpirationTimeUtc < DateTime.UtcNow;
    
    /// <summary>
    /// Флаг, необходимо ли установить системную прокси
    /// </summary>
    public bool NeedEnableSystemProxy => (Proxy == null || Proxy.IsExpired) &&
                                         (SystemProxy == null || SystemProxy.SetTime < DateTime.UtcNow.AddHours(-24));

    /// <summary>
    /// Метод для обновления ограничений, если это требуется
    /// </summary>
    private void UpdateRestrictionsIfNeeded()
    {
        // Если ограничения истекли
        if (Restrictions.IsRestrictionsExpired)
        {
            // Создаем новые ограничения
            Restrictions = new Restrictions();
        }
    }

    /// <summary>
    /// Метод для проверки возможности отправки сообщений
    /// </summary>
    /// <exception cref="MessageLimitException">Возникает, если превышен лимит отправки сообщений</exception>
    public void CheckSendRestrictions()
    {
        // Обновляем ограничения если это требуется 
        UpdateRestrictionsIfNeeded();

        // Проверяем - привышен ли лимит сообщений, если превышен, то прокидываем исключения
        if (Restrictions.MessageCount >= MaxMessageCount) throw new MessageLimitException();
    }

    /// <summary>
    /// Метод для обновления ограничений после отправки сообщения
    /// </summary>
    public void UpdateSendRestrictions()
    {
        // Проверяем возможность отправки сообщения
        CheckSendRestrictions();

        // Задаем новые ограничения
        Restrictions = new Restrictions
        {
            // Задаем новое кол-во отправленных сообщений + 1 (которое отправили до вызоыва этого метода)
            MessageCount = Restrictions.MessageCount + 1,

            // Задаем такое же кол-во загрузок файлов
            FileDownloadCount = Restrictions.FileDownloadCount
        };
    }

    /// <summary>
    /// Метод для обновления ограничей после загрузки файла
    /// </summary>
    public void UpdateDownloadRestrictions()
    {
        // Проверяем возможность загрузки файла
        CheckDownloadRestrictions();

        // Задаем новые ограничения
        Restrictions = new Restrictions
        {
            // Задаем такое же кол-во отправленных сообщений
            MessageCount = Restrictions.MessageCount,

            // Задаем новое кол-во загрузок файла + 1 (которая была до вызоыва этого метода)
            FileDownloadCount = Restrictions.FileDownloadCount + 1
        };
    }

    /// <summary>
    /// Проверяем возможность загрузки файла
    /// </summary>
    /// <exception cref="DownloadLimitException">Возникает, если превышен лимит загрузки файлов</exception>
    public void CheckDownloadRestrictions()
    {
        UpdateRestrictionsIfNeeded();
        if (Restrictions.FileDownloadCount >= MaxFileDownloadCount) throw new DownloadLimitException();
    }

    /// <summary>
    /// Добавляет новую пересылку сообщений.
    /// </summary>
    /// <param name="forwardEntry">Сущность пересылки сообщений</param>
    public void AddForwardEntry(ForwardEntry forwardEntry)
    {
        // Проверяем наличие дубликатов перед добавлением новой пересылки
        if (_forwardEntries.Contains(forwardEntry))
        {
            // Выбрасываем исключение, если пересылка с такими же параметрами уже существует
            throw new DuplicateForwardEntryExсeption();
        }

        _forwardEntries.Add(forwardEntry);
    }

    /// <summary>
    /// Удаляет существующую пересылку сообщений.
    /// </summary>
    /// <param name="forwardEntry">Сущность пересылки сообщений</param>
    public bool RemoveForwardEntry(ForwardEntry forwardEntry) => _forwardEntries.Remove(forwardEntry);

    /// <summary>
    /// Добавляет ссылку в список URL.
    /// </summary>
    /// <param name="url">URL-адрес, который нужно добавить. Объект должен быть валидным <see cref="Uri"/>.</param>
    /// <exception cref="DuplicateWebhookUrlException">Выбрасывается, если указанный URL уже присутствует в списке.</exception>
    /// <remarks>
    /// Перед добавлением ссылки метод проверяет наличие дубликатов. Если такой URL уже существует, будет выброшено исключение.
    /// </remarks>
    public void AddWebhookUrl(Uri url)
    {
        // Проверяем наличие дубликатов перед добавлением новой пересылки
        if (_urls.Contains(url))
        {
            // Выбрасываем исключение, если пересылка с такими же параметрами уже существует
            throw new DuplicateWebhookUrlException();
        }

        _urls.Add(url);
    }

    /// <summary>
    /// Удаляет ссылку из списка URL.
    /// </summary>
    /// <param name="url">URL-адрес, который нужно удалить. Объект должен быть валидным <see cref="Uri"/>.</param>
    /// <remarks>
    /// Метод ищет указанный URL в списке и удаляет его, если он найден. Если URL отсутствует в списке, метод не выполняет никаких действий.
    /// </remarks>
    public bool RemoveWebhookUrl(Uri url) => _urls.Remove(url);
}