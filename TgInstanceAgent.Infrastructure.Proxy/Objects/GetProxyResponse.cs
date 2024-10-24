using Newtonsoft.Json;

namespace TgInstanceAgent.Infrastructure.Proxy.Objects;

/// <summary>
/// Ответ на запрос списка прокси
/// </summary>
public class GetProxyResponse : SuccessResponse
{
    /// <summary>
    /// Массив прокси
    /// </summary>
    [JsonProperty("list")]
    public required ProxyResponse[] List { get; init; }
}