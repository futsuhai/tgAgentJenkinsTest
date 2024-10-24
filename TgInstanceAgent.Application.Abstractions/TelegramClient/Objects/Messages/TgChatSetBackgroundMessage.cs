using TgInstanceAgent.Application.Abstractions.TelegramClient.Objects.Chats;

namespace TgInstanceAgent.Application.Abstractions.TelegramClient.Objects.Messages;

/// <summary>
/// Класс, представляющий сообщение о смене фона чата
/// </summary>
public class TgChatSetBackgroundMessage: TgMessageContent
{
    /// <summary>
    /// Идентификатор сообщения с ранее установленным таким же фоном
    /// </summary>
    public long? OldBackgroundMessageId { get; set; }    
    
    /// <summary>
    /// Новый фон
    /// </summary>
    public TgChatBackground Background { get; set; }
    
    /// <summary>
    /// True, если установили фон для себя
    /// </summary>
    public bool OnlyForSelf { get; set; }
}