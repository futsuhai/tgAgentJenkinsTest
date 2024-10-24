using TgInstanceAgent.Domain.Instances.Enums;
using TgInstanceAgent.Domain.SystemProxy.Enums;

namespace TgInstanceAgent.Domain.Instances.ValueObjects;

/// <summary>
/// Value Object представляющий прокси-сервер.
/// </summary>
public class Proxy
{
    /// <summary>
    /// Хост прокси-сервера.
    /// </summary>
    public required string Host { get; init; }

    /// <summary>
    /// Порт прокси-сервера.
    /// </summary>
    public required ushort Port { get; init; }

    /// <summary>
    /// Логин для аутентификации на прокси-сервере.
    /// </summary>
    public required string? Login { get; init; }

    /// <summary>
    /// Пароль для аутентификации на прокси-сервере.
    /// </summary>
    public required string? Password { get; init; }

    /// <summary>
    /// Тип прокси-сервера.
    /// </summary>
    public required ProxyType Type { get; init; }

    /// <summary>
    /// Время истечения срока действия в формате UTC.
    /// </summary>
    public DateTime? ExpirationTimeUtc { get; init; }

    /// <summary>
    /// Признак истечения срока действия.
    /// </summary>
    public bool IsExpired => ExpirationTimeUtc.HasValue && ExpirationTimeUtc < DateTime.UtcNow;
}