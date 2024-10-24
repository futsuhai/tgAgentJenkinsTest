namespace TgInstanceAgent.Application.Abstractions.TelegramClient.Functions.Chats;

/// <summary>
/// Запрос на изменение заголовка чата
/// </summary>
public class TgSetChatTitleRequest
{
    /// <summary>
    /// Идентификатор чата.
    /// </summary>
    public required long ChatId { get; init; }
    
    /// <summary>
    /// Заголовок.
    /// </summary>
    public required string Title { get; init; }
}