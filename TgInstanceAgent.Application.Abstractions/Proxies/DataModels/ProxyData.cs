using TgInstanceAgent.Domain.Instances.Enums;
using TgInstanceAgent.Domain.SystemProxy.Enums;

namespace TgInstanceAgent.Application.Abstractions.Proxies.DataModels;

/// <summary>
/// Объект данных о прокси сервере из proxy6
/// </summary>
public class ProxyData
{
    /// <summary>
    /// Идентификатор прокси в API
    /// </summary>
    public required string Id { get; init; }
    
    /// <summary>
    /// IP адрес прокси
    /// </summary>
    public required string Ip { get; init; }

    /// <summary>
    /// Хост прокси
    /// </summary>
    public required string Host { get; init; }

    /// <summary>
    /// Порт прокси
    /// </summary>
    public required ushort Port { get; init; }

    /// <summary>
    /// Логин прокси
    /// </summary>
    public required string Login { get; init; }

    /// <summary>
    /// Пароль прокси
    /// </summary>
    public required string Password { get; init; }
    
    /// <summary>
    /// Версия прокси
    /// </summary>
    public required ProxyType Type { get; init; }

    /// <summary>
    /// Страна прокси
    /// </summary>
    public required string Country { get; set; }

    /// <summary>
    /// Дата покупки прокси - время московское
    /// </summary>
    public required DateTime PurchaseTime { get; init; }

    /// <summary>
    /// Дата окончания срока действия прокси - время московское
    /// </summary>
    public required DateTime ExpirationTime { get; init; }
}