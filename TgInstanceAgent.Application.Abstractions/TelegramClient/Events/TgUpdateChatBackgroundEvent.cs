using TgInstanceAgent.Application.Abstractions.TelegramClient.Events.Visitors;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Objects.Chats;

namespace TgInstanceAgent.Application.Abstractions.TelegramClient.Events;


/// <summary>
/// Фон чата был изменен
/// </summary>
public class TgUpdateChatBackgroundEvent : TgEvent
{
    /// <summary>
    /// айди чата
    /// </summary>
    public required long ChatId { get; init; }
    
    /// <summary>
    /// Новый фон, null, если бы изменен на дефолтный
    /// </summary>
    public TgChatBackground? Background { get; init; }

    /// <summary>
    /// Принять посетителя события.
    /// </summary>
    public override void Accept(ITgEventVisitor eventVisitor) => eventVisitor.Visit(this);
}