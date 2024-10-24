namespace TgInstanceAgent.Domain.Abstractions;

/// <summary>
/// Абстрактный класс для агрегата с идентификатором.
/// </summary>
public abstract class AggregateRoot(Guid id)
{
    /// <summary>
    /// Идентификатор агрегата.
    /// </summary>
    public Guid Id { get; } = id;
}