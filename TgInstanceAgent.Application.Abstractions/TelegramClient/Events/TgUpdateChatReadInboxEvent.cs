using TgInstanceAgent.Application.Abstractions.TelegramClient.Events.Visitors;

namespace TgInstanceAgent.Application.Abstractions.TelegramClient.Events;

/// <summary>
/// Входящие сообщения были прочитаны или количество непрочитанных сообщений было изменено
/// </summary>
public class TgUpdateChatReadInboxEvent : TgEvent
{
    /// <summary>
    /// Айди чата
    /// </summary>
    public required long ChatId { get; init; }
    
    /// <summary>
    /// Идентификатор последнего прочитанного входящего сообщения
    /// </summary>
    public required long LastReadInboxMessageId { get; init; }
    
    /// <summary>
    /// Количество непрочитанных сообщений, оставшихся в чате
    /// </summary>
    public required int UnreadCount { get; init; }

    /// <summary>
    /// Принять посетителя события.
    /// </summary>
    public override void Accept(ITgEventVisitor eventVisitor) => eventVisitor.Visit(this);
}