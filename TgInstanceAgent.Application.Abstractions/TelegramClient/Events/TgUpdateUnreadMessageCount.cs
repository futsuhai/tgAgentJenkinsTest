using TgInstanceAgent.Application.Abstractions.TelegramClient.Events.Visitors;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Objects.Enums;

namespace TgInstanceAgent.Application.Abstractions.TelegramClient.Events;

/// <summary>
/// Количество непрочитанных сообщений в списке чатов изменилось.
/// </summary>
public class TgUpdateUnreadMessageCount : TgEvent
{
    /// <summary>
    /// Список чатов
    /// </summary>
    public required TgChatList ChatList { get; init; }

    /// <summary>
    /// Идентификатор папки, в которой находится чат
    /// </summary>
    public int? ChatFolderId { get; init; }

    /// <summary>
    /// Общее количество непрочитанных сообщений
    /// </summary>
    public required int UnreadCount { get; set; }

    /// <summary>
    /// Общее количество непрочитанных сообщений в неотключенных чатах
    /// </summary>
    public required int UnreadUnmutedCount { get; set; }

    /// <summary>
    /// Принять посетителя события.
    /// </summary>
    public override void Accept(ITgEventVisitor eventVisitor) => eventVisitor.Visit(this);
}
