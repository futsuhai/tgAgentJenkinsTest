using TgInstanceAgent.Application.Abstractions.TelegramClient.Events.Visitors;

namespace TgInstanceAgent.Application.Abstractions.TelegramClient.Events;

/// <summary>
/// Событие изменения времени автоудаления сообщений в чате
/// </summary>
public class TgUpdateChatMessageAutoDeleteTime : TgEvent
{
    /// <summary>
    /// айди чата
    /// </summary>
    public required long ChatId { get; init; }
    
    /// <summary>
    /// Время автоудаления
    /// </summary>
    public required int MessageAutoDeleteTime { get; init; }
    
    /// <summary>
    /// Принять посетителя события.
    /// </summary>
    public override void Accept(ITgEventVisitor eventVisitor) => eventVisitor.Visit(this);
}