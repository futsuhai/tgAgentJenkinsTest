using TgInstanceAgent.Application.Abstractions.TelegramClient.Events.Visitors;

namespace TgInstanceAgent.Application.Abstractions.TelegramClient.Events;

/// <summary>
/// Событие просмотра сообщения с живой геолокацией
/// </summary>
public class TgMessageLiveLocationViewedEvent : TgEvent
{
    /// <summary>
    /// Идентификатор чата с сообщением живой геолокации
    /// </summary>
    public required long ChatId { get; init; }

    /// <summary>
    /// Идентификатор сообщения с живой геолокацией
    /// </summary>
    public required long MessageId { get; init; }

    /// <summary>
    /// Принять посетителя события.
    /// </summary>
    public override void Accept(ITgEventVisitor eventVisitor) => eventVisitor.Visit(this);
}