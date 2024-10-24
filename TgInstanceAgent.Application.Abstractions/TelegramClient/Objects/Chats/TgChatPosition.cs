using TgInstanceAgent.Application.Abstractions.TelegramClient.Objects.Enums;

namespace TgInstanceAgent.Application.Abstractions.TelegramClient.Objects.Chats;

/// <summary>
/// Содержит информацию о позиции чата в списке чатов
/// </summary>
public class TgChatPosition
{
    /// <summary>
    /// Список чатов
    /// </summary>
    public required TgChatList List { get; init; }
    
    /// <summary>
    /// Идентификатор папки, в которой находится чат
    /// </summary>
    public int? ChatFolderId { get; init; }

    /// <summary>
    /// Параметр, используемый для определения порядка чата в списке чатов. Чат должен быть отсортирован по паре (order, chat.id) в порядке убывания
    /// </summary>
    public required long Order { get; init; }

    /// <summary>
    /// True, если чат закреплен в списке чатов
    /// </summary>
    public required bool IsPinned { get; init; }
}