using TgInstanceAgent.Application.Abstractions.TelegramClient.Events.Visitors;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Objects.Messages;

namespace TgInstanceAgent.Application.Abstractions.TelegramClient.Events;

/// <summary>
/// Событие изменения содержимого сообщения
/// </summary>
public class TgUpdateMessageContent : TgEvent
{
    /// <summary>
    /// Идентификатор чата
    /// </summary>
    public required long ChatId { get; init; }

    /// <summary>
    /// Идентификатор сообщения
    /// </summary>
    public required long MessageId { get; init; }

    /// <summary>
    /// Новое содержимое сообщения
    /// </summary>
    public required TgMessageContent NewContent { get; init; }

    /// <summary>
    /// Принять посетителя события.
    /// </summary>
    public override void Accept(ITgEventVisitor eventVisitor) => eventVisitor.Visit(this);
}