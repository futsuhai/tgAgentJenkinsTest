using Newtonsoft.Json;

namespace TgInstanceAgent.Infrastructure.Proxy.Objects;

/// <summary>
/// Объект данных о прокси сервере
/// </summary>
public class ProxyResponse
{
    /// <summary>
    /// Идентификатор прокси в API
    /// </summary>
    [JsonProperty("id")]
    public required string Id { get; init; }
    
    /// <summary>
    /// IP адрес прокси
    /// </summary>
    [JsonProperty("ip")]
    public required string Ip { get; init; }
    
    /// <summary>
    /// Хост прокси
    /// </summary>
    [JsonProperty("host")]
    public required string Host { get; init; }
    
    /// <summary>
    /// Порт прокси
    /// </summary>
    [JsonProperty("port")]
    public required string Port { get; init; }

    /// <summary>
    /// Логин прокси
    /// </summary>
    [JsonProperty("user")]
    public required string Login { get; init; }

    /// <summary>
    /// Пароль прокси
    /// </summary>
    [JsonProperty("pass")]
    public required string Password { get; init; }

    /// <summary>
    /// Тип протокола прокси http или socks
    /// </summary>
    [JsonProperty("type")]
    public required string Type { get; init; }

    /// <summary>
    /// Страна прокси
    /// </summary>
    [JsonProperty("country")]
    public string? Country { get; init; }

    /// <summary>
    /// Дата покупки прокси - время московское
    /// </summary>
    [JsonProperty("date")]
    public required DateTime PurchaseTime { get; init; }

    /// <summary>
    /// Дата окончания срока действия прокси - время московское
    /// </summary>
    [JsonProperty("date_end")]
    public required DateTime ExpirationTime { get; init; }
}