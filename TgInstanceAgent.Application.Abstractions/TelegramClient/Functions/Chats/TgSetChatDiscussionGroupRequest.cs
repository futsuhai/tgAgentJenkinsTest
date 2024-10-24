namespace TgInstanceAgent.Application.Abstractions.TelegramClient.Functions.Chats;

/// <summary>
/// Запрос для установки группы обсуждения
/// </summary>
public class TgSetChatDiscussionGroupRequest
{
    /// <summary>
    /// Идентификатор чата.
    /// </summary>
    public required long ChatId { get; init; }
    
    /// <summary>
    /// Идентификатор обсуждаемой группы
    /// </summary>
    public required long DiscussionChatId { get; init; }
}