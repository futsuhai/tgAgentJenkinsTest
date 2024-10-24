namespace TgInstanceAgent.Infrastructure.Web.Contacts.InputModels;

/// <summary>
/// Модель входных данных для поиска контактов.
/// </summary>
public class SearchContactsInputModel
{
    /// <summary>
    /// Лимит контактов.
    /// </summary>
    public int Limit { get; init; }
    
    /// <summary>
    /// Строка с ключевыми словами для поиска.
    /// </summary>
    public string? Query { get; init; }
}