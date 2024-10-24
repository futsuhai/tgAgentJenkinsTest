namespace TgInstanceAgent.Application.Abstractions.TelegramClient.Functions.Users;

/// <summary>
/// Структура запроса контактов
/// </summary>
public class TgGetContactsRequest
{
    /// <summary>
    /// Идентификатор контакта
    /// </summary>
    public long? FromContactId { get; init; }
    
    /// <summary>
    /// Лимит истории чата.
    /// </summary>
    public required int Limit { get; init; }

    /// <summary>
    /// Смещение истории чата.
    /// </summary>
    public int? Offset { get; init; }
}