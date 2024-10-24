using System.ComponentModel.DataAnnotations.Schema;
using TgInstanceAgent.Domain.Instances.Enums;
using TgInstanceAgent.Infrastructure.Storage.Entities.Abstractions;

namespace TgInstanceAgent.Infrastructure.Storage.Entities.Instances;

/// <summary>
/// Модель инстанса в EntityFramework.
/// </summary>
public class InstanceModel : IAggregateModel
{
    /// <summary>
    /// Идентификатор инстанса.
    /// </summary>
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public Guid Id { get; set; }

    /// <summary>
    /// Идентификатор пользователя.
    /// При отправке запроса по идентификатору, необходимо быть уверенным, что приложению известен данный пользователь.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Время истечения в формате UTC.
    /// </summary>
    public DateTime ExpirationTimeUtc { get; set; }

    /// <summary>
    /// Состояние инстанса.
    /// </summary>
    public State State { get; set; }

    /// <summary>
    /// Включен ли инстанс
    /// </summary>
    public bool Enabled { get; set; }
    
    /// <summary>
    /// Список URL-адресов вебхуков, которые связаны с этим инстансом.
    /// </summary>
    public List<WebhookUrlModel> WebhookUrls { get; set; } = [];

    /// <summary>
    /// Настройки вебхука.
    /// </summary>
    public WebhookSettingModel? WebhookSetting { get; set; }
    
    /// <summary>
    /// Прокси для инстанса.
    /// </summary>
    public ProxyModel? Proxy { get; set; }

    /// <summary>
    /// Ограничения инстанса
    /// </summary>
    public RestrictionsModel? Restrictions { get; set; }
    
    /// <summary>
    /// Пересылки сообщений.
    /// </summary>
    public List<ForwardEntryModel> ForwardEntries { get; set; } = [];
    
    /// <summary>
    /// Идентификатор системного прокси
    /// </summary>
    public Guid? SystemProxyId  { get; set; }

    /// <summary>
    /// Время установки системного прокси
    /// </summary>
    public DateTime? SystemProxySetTime { get; set; }
}