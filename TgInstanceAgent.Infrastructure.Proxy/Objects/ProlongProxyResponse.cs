using Newtonsoft.Json;

namespace TgInstanceAgent.Infrastructure.Proxy.Objects;

/// <summary>
/// Ответ на запрос продления прокси
/// </summary>
public class ProlongProxyResponse : SuccessResponse
{
    /// <summary>
    /// Массив прокси
    /// </summary>
    [JsonProperty("list")]
    public required ProlongResponse[] List { get; init; }
}