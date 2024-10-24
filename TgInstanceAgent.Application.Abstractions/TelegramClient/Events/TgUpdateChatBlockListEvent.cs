using TgInstanceAgent.Application.Abstractions.TelegramClient.Events.Visitors;

namespace TgInstanceAgent.Application.Abstractions.TelegramClient.Events;

/// <summary>
/// Событие блокировки или разблокировки чата
/// </summary>
public class TgUpdateChatBlockListEvent : TgEvent
{
    /// <summary>
    /// Идентификатор чата
    /// </summary>
    public required long ChatId { get; init; }

    /// <summary>
    /// Флаг, заблокирован ли чат.
    /// </summary>
    public required bool IsBlocked { get; init; }
    
    /// <summary>
    /// Флаг, заблокированы ли истории чата.
    /// </summary>
    public required bool IsStoriesBlocked { get; init; }

    /// <summary>
    /// Принять посетителя события.
    /// </summary>
    public override void Accept(ITgEventVisitor eventVisitor) => eventVisitor.Visit(this);
}