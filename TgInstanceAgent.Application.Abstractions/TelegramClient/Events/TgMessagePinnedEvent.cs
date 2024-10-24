using TgInstanceAgent.Application.Abstractions.TelegramClient.Events.Visitors;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Objects.Messages;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Objects.Users;

namespace TgInstanceAgent.Application.Abstractions.TelegramClient.Events;

/// <summary>
/// Событие, генерируемое при изменении статуса закрепления сообщения.
/// </summary>
public class TgMessagePinnedEvent : TgEvent
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
    /// Закреплено ли сообщение
    /// </summary>
    public required bool IsPinned { get; init; }
    
    /// <summary>
    /// Принять посетителя события.
    /// </summary>
    public override void Accept(ITgEventVisitor eventVisitor) => eventVisitor.Visit(this);
}