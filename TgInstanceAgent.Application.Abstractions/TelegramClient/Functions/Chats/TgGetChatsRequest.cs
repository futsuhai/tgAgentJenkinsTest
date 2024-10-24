using TgInstanceAgent.Application.Abstractions.TelegramClient.Objects.Enums;

namespace TgInstanceAgent.Application.Abstractions.TelegramClient.Functions.Chats;

/// <summary>
/// Запрос на получение списка чатов
/// </summary>
public class TgGetChatsRequest
{
    /// <summary>
    /// Тип списка
    /// </summary>
    public required TgChatList List { get; init; }
    
    /// <summary>
    /// Идентификатор папки
    /// </summary>
    public int? ChatFolderId { get; init; }
    
    /// <summary>
    /// Идентификатор чата
    /// </summary>
    public long? FromChatId { get; init; }
    
    /// <summary>
    /// Лимит истории чата.
    /// </summary>
    public required int Limit { get; init; }

    /// <summary>
    /// Смещение истории чата.
    /// </summary>
    public int? Offset { get; init; }
    
    /// <summary>
    /// Флаг, необходимо ли включить в ответ данные пользователей
    /// </summary>
    public required bool IncludeUsers { get; init; }
}