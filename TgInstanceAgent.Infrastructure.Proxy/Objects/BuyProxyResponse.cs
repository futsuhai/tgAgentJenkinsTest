using Newtonsoft.Json;

namespace TgInstanceAgent.Infrastructure.Proxy.Objects;

/// <summary>
/// Ответ на запрос покупки прокси
/// </summary>
public class BuyProxyResponse : SuccessResponse
{
    /// <summary>
    /// Прокси
    /// </summary>
    [JsonProperty("list")]
    public required ProxyResponse[] List { get; init; }
    
    /// <summary>
    /// Страна
    /// </summary>
    [JsonProperty("country")]
    public required string County { get; init; }
}