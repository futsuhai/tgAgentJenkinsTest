namespace TgInstanceAgent.Application.Abstractions.TelegramClient.Objects.Messages;

/// <summary>
/// Информация о реакциях на сообщение.
/// </summary>
public class TgMessageReactions
{
    /// <summary>
    /// Указаны ли теги для реакции.
    /// </summary>
    public bool AreTags { get; init; }
    
    /// <summary>
    /// Массив реакций на сообщение.
    /// </summary>
    public TgMessageReaction[]? Reactions { get; init; }
}