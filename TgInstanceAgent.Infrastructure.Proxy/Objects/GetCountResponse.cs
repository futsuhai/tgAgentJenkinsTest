using Newtonsoft.Json;

namespace TgInstanceAgent.Infrastructure.Proxy.Objects;

/// <summary>
/// Объект ответа о количестве доступных прокси
/// </summary>

public class GetCountResponse : SuccessResponse
{
    /// <summary>
    /// Количество доступных прокси
    /// </summary>
    [JsonProperty("count")]
    public required int Count { get; init; }
}