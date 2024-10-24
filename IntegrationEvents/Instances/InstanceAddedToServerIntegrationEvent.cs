namespace IntegrationEvents.Instances;

/// <summary>
/// Событие интеграции, которое генерируется при добавлении инстанса на сервер.
/// </summary>
public class InstanceAddedToServerIntegrationEvent
{
    /// <summary>
    /// Идентификатор добавленного инстанса.
    /// </summary>
    public required Guid Id { get; init; }
}