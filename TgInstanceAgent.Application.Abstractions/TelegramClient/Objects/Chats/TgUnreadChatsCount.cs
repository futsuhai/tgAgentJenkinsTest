using TgInstanceAgent.Application.Abstractions.TelegramClient.Objects.Enums;

namespace TgInstanceAgent.Application.Abstractions.TelegramClient.Objects.Chats;

/// <summary>
/// Количество непрочитанных чатов в списке чатов.
/// </summary>
public class TgUnreadChatsCount
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
    /// Приблизительное общее количество чатов в списке чатов
    /// </summary>
    public required int TotalCount { get; init; }

    /// <summary>
    /// Общее количество непрочитанных чатов
    /// </summary>
    public required int UnreadCount { get; init; }

    /// <summary>
    /// Общее количество непрочитанных неотключенных чатов
    /// </summary>
    public required int UnreadUnmutedCount { get; init; }

    /// <summary>
    /// Общее количество чатов, помеченных как непрочитанные
    /// </summary>
    public required int MarkedAsUnreadCount { get; init; }

    /// <summary>
    /// Общее количество неотключенных чатов, помеченных как непрочитанные
    /// </summary>
    public required int MarkedAsUnreadUnmutedCount { get; init; }
}
