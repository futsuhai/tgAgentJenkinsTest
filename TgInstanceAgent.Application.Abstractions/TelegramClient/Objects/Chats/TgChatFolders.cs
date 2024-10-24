namespace TgInstanceAgent.Application.Abstractions.TelegramClient.Objects.Chats;

/// <summary>
/// Список папок чатов
/// </summary>
public class TgChatFolders
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
}