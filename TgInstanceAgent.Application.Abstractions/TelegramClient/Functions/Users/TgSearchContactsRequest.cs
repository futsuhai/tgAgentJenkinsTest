namespace TgInstanceAgent.Application.Abstractions.TelegramClient.Functions.Users;

/// <summary>
/// Структура запроса для поиска контактов
/// </summary>
public class TgSearchContactsRequest
{
    /// <summary>
    /// Число необходимых контактов.
    /// </summary>
    public required int Limit { get; init; }
    
    /// <summary>
    /// Строка с ключевыми словами для поиска.
    /// </summary>
    public required string Query { get; init; }
}