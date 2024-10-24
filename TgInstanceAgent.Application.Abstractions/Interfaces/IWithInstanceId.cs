namespace TgInstanceAgent.Application.Abstractions.Interfaces;

/// <summary>
/// Интерфейс, определяющий наличие идентификатора инстанса
/// </summary>
public interface IWithInstanceId
{
    /// <summary>
    /// Идентификатор инстанса
    /// </summary>
    public Guid InstanceId { get; init; }
}