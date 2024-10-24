namespace TgInstanceAgent.Application.Abstractions.Proxies.DataModels;

/// <summary>
/// Объект данных о продлении прокси
/// </summary>
public class ExtendResult
{
    /// <summary>
    /// Идентификатор прокси в API
    /// </summary>
    public required string Id { get; init; }
    
    /// <summary>
    /// Дата окончания срока действия прокси - время московское
    /// </summary>
    public required DateTime ExpirationTime { get; init; }
}