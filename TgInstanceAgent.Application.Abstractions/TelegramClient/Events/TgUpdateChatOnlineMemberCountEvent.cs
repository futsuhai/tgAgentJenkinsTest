using TgInstanceAgent.Application.Abstractions.TelegramClient.Events.Visitors;

namespace TgInstanceAgent.Application.Abstractions.TelegramClient.Events;

/// <summary>
/// Событие изменения количества участников онлайн в групповом чате. Это событие с ненулевым количеством участников онлайн отправляется только для текущих открытых чатов.
/// Не гарантируется, что оно отправляется сразу после изменения количества участников онлайн
/// </summary>
public class TgUpdateChatOnlineMemberCountEvent : TgEvent
{
    /// <summary>
    /// Идентификатор чата
    /// </summary>
    public required long ChatId { get; init; }

    /// <summary>
    /// Новое количество участников онлайн в чате, или 0, если неизвестно
    /// </summary>
    public required int OnlineMemberCount { get; init; }

    /// <summary>
    /// Принять посетителя события.
    /// </summary>
    public override void Accept(ITgEventVisitor eventVisitor) => eventVisitor.Visit(this);
}