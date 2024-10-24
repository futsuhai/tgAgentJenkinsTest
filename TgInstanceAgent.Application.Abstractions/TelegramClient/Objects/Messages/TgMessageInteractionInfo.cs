namespace TgInstanceAgent.Application.Abstractions.TelegramClient.Objects.Messages;

/// <summary>
/// Содержит информацию о взаимодействиях с сообщением
/// </summary>
public class TgMessageInteractionInfo
{
    /// <summary>
    /// Количество просмотров сообщения.
    /// </summary>
    public required int ViewCount { get; init; }
    
    /// <summary>
    /// Количество пересылок сообщения.
    /// </summary>
    public required int ForwardCount { get; init; }
    
    /// <summary>
    /// Информация об ответах на сообщение.
    /// </summary>
    public TgMessageReplyInfo? ReplyInfo { get; init; }
    
    /// <summary>
    /// Информация о реакциях на сообщение.
    /// </summary>
    public TgMessageReactions? Reactions { get; init; }
}