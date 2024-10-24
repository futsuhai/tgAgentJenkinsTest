namespace TgInstanceAgent.Infrastructure.Web.Chats.InputModels;

/// <summary>
/// Модель для поиска чатов.
/// </summary>
public class SearchChatsInputModel
{
    /// <summary>
    /// Лимит чатов.
    /// </summary>
    public int Limit { get; init; } = 100;
    
    /// <summary>
    /// Строка с ключевыми словами для поиска.
    /// </summary>
    public string? Query { get; init; }
}