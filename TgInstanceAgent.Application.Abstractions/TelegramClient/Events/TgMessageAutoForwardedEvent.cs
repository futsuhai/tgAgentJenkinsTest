using TgInstanceAgent.Application.Abstractions.TelegramClient.Events.Visitors;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Objects.Messages;

namespace TgInstanceAgent.Application.Abstractions.TelegramClient.Events;

/// <summary>
/// Событие, генерируемое при автопересылке сообщения.
/// </summary>
public class TgMessageAutoForwardedEvent : TgEvent
{
    /// <summary>
    /// Исходное сообщение
    /// </summary>
    public required TgMessage SourceMessage { get; init; }

    /// <summary>
    /// Новое сообщение
    /// </summary>
    public required TgMessage NewMessage { get; init; }
    
    /// <summary>
    /// Принять посетителя события.
    /// </summary>
    public override void Accept(ITgEventVisitor eventVisitor) => eventVisitor.Visit(this);
}