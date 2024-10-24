using TgInstanceAgent.Application.Abstractions.TelegramClient.Events.Visitors;

namespace TgInstanceAgent.Application.Abstractions.TelegramClient.Events;

/// <summary>
/// Событие, генерируемое при изменении настроек времени удаления сообщений в чате.
/// </summary>
public class TgUpdateChatMessageAutoDeleteTimeEvent  : TgEvent
{
    /// <summary>
    /// Идентификатор чата
    /// </summary>
    public required long ChatId { get; init; }

    /// <summary>
    /// Новое значение времени автоудаления сообщений
    /// </summary>
    public required int MessageAutoDeleteTime { get; init; }
    
    /// <summary>
    /// Принять посетителя события.
    /// </summary>
    public override void Accept(ITgEventVisitor eventVisitor) => eventVisitor.Visit(this);
}