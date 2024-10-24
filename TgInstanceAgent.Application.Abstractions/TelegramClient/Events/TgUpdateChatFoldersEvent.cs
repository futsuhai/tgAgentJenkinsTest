using TgInstanceAgent.Application.Abstractions.TelegramClient.Events.Visitors;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Objects.Chats;

namespace TgInstanceAgent.Application.Abstractions.TelegramClient.Events;

/// <summary>
/// Список папок чатов или папка чатов изменилась
/// </summary>
public class TgUpdateChatFoldersEvent : TgEvent
{
    /// <summary>
    /// Новый список папок чатов
    /// </summary>
    public required TgChatFolderInfo[] ChatFolders { get; init; }

    /// <summary>
    /// Позиция основного списка чатов среди папок чатов, 0-based
    /// </summary>
    public required int MainChatListPosition { get; init; }

    /// <summary>
    /// True, если теги папок включены
    /// </summary>
    public required bool AreTagsEnabled { get; init; }

    /// <summary>
    /// Принять посетителя события.
    /// </summary>
    public override void Accept(ITgEventVisitor eventVisitor) => eventVisitor.Visit(this);
}
