using TgInstanceAgent.Application.Abstractions.TelegramClient.Functions.Components;

namespace TgInstanceAgent.Application.Abstractions.TelegramClient.Functions.Chats;

/// <summary>
/// Запрос на получение истории чата.
/// </summary>
public class TgGetChatHistoryRequest
{
    /// <summary>
    /// Чат
    /// </summary>
    public required TgInputChat Chat { get; init; }

    /// <summary>
    /// Идентификатор сообщения начала истории чата.
    /// </summary>
    public long? FromMessageId { get; init; }

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