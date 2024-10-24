using MassTransit;
using Newtonsoft.Json;

namespace TgInstanceAgent.Infrastructure.Proxy.Objects;

/// <summary>
/// Модель ошибки для прокси
/// </summary>
public class ErrorResponse : Response
{
    /// <summary>
    /// Текст ошибки
    /// </summary>
    [JsonProperty("error")]
    public required string Error { get; init; }
    
    /// <summary>
    /// Идентификатор ошибки
    /// </summary>
    [JsonProperty("error_id")]
    public required int ErrorId { get; set; }
}