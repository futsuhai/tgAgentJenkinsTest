using TgInstanceAgent.Application.Abstractions.TelegramClient.Events.Visitors;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Objects.Chats;

namespace TgInstanceAgent.Application.Abstractions.TelegramClient.Events;

/// <summary>
/// Событие, генерируемое при изменении настроек уведомлений чата.
/// </summary>
public class TgUpdateChatNotificationSettings : TgEvent
{
    /// <summary>
    /// Идентификатор чата
    /// </summary>
    public required long ChatId { get; init; }
    
    /// <summary>
    /// Настройки уведомлений чата
    /// </summary>
    public required TgChatNotificationSettings NotificationSettings { get; init; }
    
    /// <summary>
    /// Принять посетителя события.
    /// </summary>
    public override void Accept(ITgEventVisitor eventVisitor) => eventVisitor.Visit(this);
}