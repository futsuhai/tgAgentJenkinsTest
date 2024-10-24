using Newtonsoft.Json;

namespace TgInstanceAgent.Infrastructure.Proxy.Objects;

/// <summary>
/// Объект данных ответа API прокси о доступных странах прокси
/// </summary>
public class AvailableCountriesResponse : SuccessResponse
{
    /// <summary>
    /// Список доступных стран
    /// </summary>
    [JsonProperty("list")]
    public required string[] Countries { get; init; }
}