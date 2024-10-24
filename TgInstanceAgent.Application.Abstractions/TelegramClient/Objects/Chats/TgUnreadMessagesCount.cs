using TgInstanceAgent.Application.Abstractions.TelegramClient.Objects.Enums;

namespace TgInstanceAgent.Application.Abstractions.TelegramClient.Objects.Chats;

/// <summary>
/// Количество непрочитанных сообщений в списке чатов.
/// </summary>
public class TgUnreadMessagesCount
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
    public required int UnreadCount { get; init; }

    /// <summary>
    /// Общее количество непрочитанных сообщений в неотключенных чатах
    /// </summary>
    public required int UnreadUnmutedCount { get; init; }
}
