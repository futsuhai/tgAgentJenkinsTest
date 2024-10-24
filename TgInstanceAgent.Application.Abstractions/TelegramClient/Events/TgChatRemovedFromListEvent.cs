using TgInstanceAgent.Application.Abstractions.TelegramClient.Events.Visitors;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Objects.Enums;

namespace TgInstanceAgent.Application.Abstractions.TelegramClient.Events;

/// <summary>
/// Событие удаления чата из списка чатов
/// </summary>
public class TgChatRemovedFromListEvent : TgEvent
{
    /// <summary>
    /// Идентификатор чата
    /// </summary>
    public required long ChatId { get; init; }

    /// <summary>
    /// Список чатов, из которого был удален чат
    /// </summary>
    public required TgChatList ChatList { get; init; }

    /// <summary>
    /// Принять посетителя события.
    /// </summary>
    public override void Accept(ITgEventVisitor eventVisitor) => eventVisitor.Visit(this);
}