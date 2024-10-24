namespace TgInstanceAgent.Application.Abstractions.TelegramClient.Objects.Messages;

/// <summary>
/// Класс, представляющий доступные реакции для сообщения в Telegram.
/// </summary>
public class TgMessageAvailableReactions
{
    /// <summary>
    /// Возвращает или устанавливает коллекцию наиболее популярных реакций.
    /// </summary>
    public required IReadOnlyCollection<TgAvailableReaction> TopReactions { get; init; }

    /// <summary>
    /// Возвращает или устанавливает коллекцию недавних реакций.
    /// </summary>
    public required IReadOnlyCollection<TgAvailableReaction> RecentReactions { get; init; }
    
    /// <summary>
    /// Возвращает или устанавливает коллекцию недавних реакций.
    /// </summary>
    public required IReadOnlyCollection<TgAvailableReaction> PopularReactions { get; init; }
}