using TgInstanceAgent.Application.Abstractions.TelegramClient.Events.Visitors;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Objects.Chats;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Objects.Components;

namespace TgInstanceAgent.Application.Abstractions.TelegramClient.Events;

/// <summary>
/// Событие изменения активности отправителя сообщений в чате Telegram
/// </summary>
public class TgChatActionEvent : TgEvent
{
    /// <summary>
    /// Идентификатор чата
    /// </summary>
    public required long ChatId { get; init; }

    /// <summary>
    /// Идентификатор отправителя сообщений, выполняющего действие
    /// </summary>
    public required TgMessageSender SenderId { get; init; }
    
    /// <summary>
    /// Идентификатор потока сообщений (thread) в чате.
    /// </summary>
    public required long MessageThreadId { get; init; }
    
    /// <summary>
    /// Действие
    /// </summary>
    public required TgChatAction Action { get; init; }

    /// <summary>
    /// Принять посетителя события.
    /// </summary>
    public override void Accept(ITgEventVisitor eventVisitor) => eventVisitor.Visit(this);
}