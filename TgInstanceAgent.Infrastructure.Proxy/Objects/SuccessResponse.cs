using Newtonsoft.Json;

namespace TgInstanceAgent.Infrastructure.Proxy.Objects;

/// <summary>
/// Модель ошибки для прокси
/// </summary>
public class SuccessResponse : Response
{
    /// <summary>
    /// Идентификатор пользователя в системе покупки прокси
    /// </summary>
    [JsonProperty("user_id")]
    public required string UserId { get; init; }
    
    /// <summary>
    /// Баланс
    /// </summary>
    [JsonProperty("balance")]
    public required double Balance { get; set; }
    
    /// <summary>
    /// Валюта баланса
    /// </summary>
    [JsonProperty("currency")]
    public required string Currency { get; set; }
}