namespace TgInstanceAgent.Application.Abstractions.Interfaces;

/// <summary>
/// Интерфейс, определяющий наличие идентификатора команды
/// </summary>
public interface IWithCommandId
{
    /// <summary>
    /// Уникальный идентификатор команды.
    /// </summary>
    public Guid CommandId { get; init; }
}