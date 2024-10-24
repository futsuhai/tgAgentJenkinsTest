using TgInstanceAgent.Application.Abstractions.TelegramClient.Functions.Components;

namespace TgInstanceAgent.Application.Abstractions.TelegramClient.Functions.Chats;

/// <summary>
/// Запрос на установку времени автоудаления сообщений в чате
/// </summary>
public class TgSetChatMessageAutoDeleteTimeRequest
{
    /// <summary>
    /// Чат
    /// </summary>
    public required TgInputChat Chat { get; init; }
    
    /// <summary>
    /// Время автоудаления
    /// </summary>
    public required int MessageAutoDeleteTime { get; init; }
}