namespace TgInstanceAgent.Domain.Instances.ValueObjects;

/// <summary>
/// Системное прокси
/// </summary>
public class SystemProxy
{
    /// <summary>
    /// Идентификатор прокси.
    /// </summary>
    public required Guid ProxyId { get; init; }

    /// <summary>
    /// Время установки прокси.
    /// </summary>
    public required DateTime SetTime { get; init; }
}