namespace TgInstanceAgent.Application.Abstractions.TelegramClient.Functions.Chats;

/// <summary>
/// Структура запроса для поиска чатов
/// </summary>
public class TgSearchChatsRequest
{
    /// <summary>
    /// Число необходимых контактов.
    /// </summary>
    public int Limit { get; init; }
    
    /// <summary>
    /// Строка с ключевыми словами для поиска.
    /// </summary>
    public required string Query { get; init; }
}