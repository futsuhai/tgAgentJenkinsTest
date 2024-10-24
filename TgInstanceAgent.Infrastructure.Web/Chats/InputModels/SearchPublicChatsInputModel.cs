namespace TgInstanceAgent.Infrastructure.Web.Chats.InputModels;

/// <summary>
/// Модель для поиска публичных чатов.
/// </summary>
public class SearchPublicChatsInputModel
{
    /// <summary>
    /// Строка с ключевыми словами для поиска.
    /// </summary>
    public string? Query { get; init; }
}