namespace TgInstanceAgent.Application.Abstractions.TelegramClient.Objects.Components;

/// <summary>
/// Результат подсчета количества элементов и их получения.
/// </summary>
/// <typeparam name="T">Тип элементов.</typeparam>
public class TgCountResult<T>
{
    /// <summary>
    /// Коллекция элементов.
    /// </summary>
    public required IReadOnlyCollection<T> List { get; init; }

    /// <summary>
    /// Общее количество элементов.
    /// </summary>
    public required int TotalCount { get; init; }
}