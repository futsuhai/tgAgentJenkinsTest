namespace TgInstanceAgent.Application.Abstractions.TelegramClient.Objects.Messages;

/// <summary>
/// Класс, представляющий добавленные для сообщения реакции в Telegram.
/// </summary>
public class TgMessageAddedReactions
{
    /// <summary>
    /// Сдвиг до следующей команды
    /// </summary>
    public required string NextOffset { get; init; }
    
    /// <summary>
    /// Возвращает или устанавливает коллекцию добавленных на сообщение реакций.
    /// </summary>
    public required IReadOnlyCollection<TgAddedReaction> Reactions { get; init; }
    
    /// <summary>
    /// Общее количество реакций в коллекции 
    /// </summary>
    public required int TotalCount { get; init; }
}