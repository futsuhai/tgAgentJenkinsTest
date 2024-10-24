using TgInstanceAgent.Application.Abstractions.TelegramClient.Events.Visitors;

namespace TgInstanceAgent.Application.Abstractions.TelegramClient.Events;

/// <summary>
/// Событие изменения количества непрочитанных реакций в чате
/// </summary>
public class TgUpdateChatUnreadReactionEvent : TgEvent
{
    /// <summary>
    /// Идентификатор чата
    /// </summary>
    public required long ChatId { get; init; }

    /// <summary>
    /// Количество сообщений с непрочитанными реакциями в чате
    /// </summary>
    public required int UnreadReactionCount { get; init; }

    /// <summary>
    /// Принять посетителя события.
    /// </summary>
    public override void Accept(ITgEventVisitor eventVisitor) => eventVisitor.Visit(this);
}