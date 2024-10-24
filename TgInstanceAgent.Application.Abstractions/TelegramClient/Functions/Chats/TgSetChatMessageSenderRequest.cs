using TgInstanceAgent.Application.Abstractions.TelegramClient.Functions.Components;

namespace TgInstanceAgent.Application.Abstractions.TelegramClient.Functions.Chats;

/// <summary>
/// Запрос на изменение отправителя сообщений в групповом чате
/// </summary>
public class TgSetChatMessageSenderRequest
{
    /// <summary>
    /// Идентификатор чата.
    /// </summary>
    public required long ChatId { get; init; }
    
    /// <summary>
    /// Отправитель
    /// </summary>
    public TgInputMessageSender? MessageSender { get; init; }
}