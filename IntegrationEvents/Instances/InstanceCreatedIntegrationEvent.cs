namespace IntegrationEvents.Instances;

/// <summary>
/// Событие интеграции, которое генерируется при создании инстанса.
/// </summary>
public class InstanceCreatedIntegrationEvent
{
    /// <summary>
    /// Идентификатор созданного инстанса.
    /// </summary>
    public required Guid InstanceId { get; init; }

    /// <summary>
    /// Идентификатор пользователя, который создал инстанс.
    /// </summary>
    public required Guid UserId { get; init; }

    /// <summary>
    /// Время истечения срока действия инстанса в формате UTC.
    /// </summary>
    public required DateTime ExpirationTimeUtc { get; init; }
}