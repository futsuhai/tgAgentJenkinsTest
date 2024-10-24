using TgInstanceAgent.Application.Abstractions.TelegramClient.Events.Visitors;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Objects.Components;

namespace TgInstanceAgent.Application.Abstractions.TelegramClient.Events;

/// <summary>
/// Событие изменения отправителя сообщений в групповом чате 
/// </summary>
public class TgUpdateChatMessageSender : TgEvent
{
    /// <summary>
    /// айди чата
    /// </summary>
    public required long ChatId { get; init; }
    
    /// <summary>
    /// Отправитель
    /// </summary>
    public required TgMessageSender MessageSenderId { get; init; }
    
    /// <summary>
    /// Принять посетителя события.
    /// </summary>
    public override void Accept(ITgEventVisitor eventVisitor) => eventVisitor.Visit(this);
}