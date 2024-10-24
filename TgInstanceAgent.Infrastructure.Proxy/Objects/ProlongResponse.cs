using Newtonsoft.Json;

namespace TgInstanceAgent.Infrastructure.Proxy.Objects;

/// <summary>
/// Ответ на запрос продления прокси
/// </summary>
public class ProlongResponse
{
    /// <summary>
    /// Идентификатор прокси в API
    /// </summary>
    [JsonProperty("id")]
    public required string IdProxyInApi { get; init; }
    
    /// <summary>
    /// Дата окончания срока действия прокси - время московское
    /// </summary>
    [JsonProperty("date_end")]
    public required DateTime ExpirationTime { get; init; }
}