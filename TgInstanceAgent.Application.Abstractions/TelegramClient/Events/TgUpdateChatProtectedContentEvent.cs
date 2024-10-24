using TgInstanceAgent.Application.Abstractions.TelegramClient.Events.Visitors;

namespace TgInstanceAgent.Application.Abstractions.TelegramClient.Events;

/// <summary>
/// Событие изменения настроек защищенного контента в чате.
/// </summary>
public class TgUpdateChatProtectedContentEvent : TgEvent
{
    /// <summary>
    /// Chat identifier
    /// </summary>
    public required long ChatId { get; init; }

    /// <summary>
    /// Новое значение has_protected_content
    /// </summary>
    public required bool HasProtectedContent { get; init; }

    /// <summary>
    /// Принять посетителя события.
    /// </summary>
    public override void Accept(ITgEventVisitor eventVisitor) => eventVisitor.Visit(this);
}