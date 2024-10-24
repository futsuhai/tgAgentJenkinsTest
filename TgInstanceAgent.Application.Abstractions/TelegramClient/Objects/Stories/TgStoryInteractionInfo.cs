namespace TgInstanceAgent.Application.Abstractions.TelegramClient.Objects.Stories;

/// <summary>
/// Содержит информацию о взаимодействиях с историей
/// </summary>
public class TgStoryInteractionInfo
{
    /// <summary>
    /// Количество раз, когда история была просмотрена
    /// </summary>
    public int? ViewCount { get; init; }

    /// <summary>
    /// Количество раз, когда история была переслана; 0, если нет или неизвестно
    /// </summary>
    public int? ForwardCount { get; init; }

    /// <summary>
    /// Количество реакций, добавленных к истории; 0, если нет или неизвестно
    /// </summary>
    public int? ReactionCount { get; init; }

    /// <summary>
    /// Идентификаторы не более 3 последних зрителей истории
    /// </summary>
    public long[]? RecentViewerUserIds { get; init; }
}
