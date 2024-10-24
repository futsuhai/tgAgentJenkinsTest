namespace TgInstanceAgent.Infrastructure.Storage.Entities.Instances;

/// <summary>
/// Модель ссылки в EntityFramework.
/// </summary>
public class WebhookUrlModel
{
    /// <summary>
    /// Ссылка.
    /// </summary>
    public Uri Url { get; set; } = null!;

    /// <summary>
    /// Идентификатор инстанса.
    /// </summary>
    public Guid InstanceId { get; set; }
}