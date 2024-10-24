using TgInstanceAgent.Domain.Abstractions;
using TgInstanceAgent.Domain.SystemProxy.Enums;

namespace TgInstanceAgent.Domain.SystemProxy;

/// <summary>
/// Агрегат SystemProxyAggregate  
/// </summary>
public class SystemProxyAggregate(Guid id)  : AggregateRoot(id)
{
    /// <summary>
    /// Идентификатор прокси во внешней системе
    /// </summary>
    public required string IdInProviderSystem { get; init; }
    
    /// <summary>
    /// IP адрес прокси
    /// </summary>
    public required string Ip { get; init; }
    
    /// <summary>
    /// Хост прокси.
    /// </summary>
    public required string Host { get; init; }

    /// <summary>
    /// Порт прокси.
    /// </summary>
    public required ushort Port { get; init; }

    /// <summary>
    /// Логин для аутентификации на прокси.
    /// </summary>
    public required string Login { get; init; }

    /// <summary>
    /// Пароль для аутентификации на прокси.
    /// </summary>
    public required string Password { get; init; }

    /// <summary>
    /// Время истечения срока действия в формате UTC.
    /// </summary>
    public required DateTime ExpirationTimeUtc { get; set; }

    /// <summary>
    /// Признак истечения срока действия.
    /// </summary>
    public bool IsExpired => ExpirationTimeUtc < DateTime.UtcNow;
    
    /// <summary>
    /// Количество инстансов
    /// </summary>
    public required int InstancesCount { get; init; }
    
    /// <summary>
    /// Тип прокси.
    /// </summary>
    public required ProxyType Type { get; init; }
    
    /// <summary>
    /// Версия прокси
    /// </summary>
    public required IpVersion IpVersion { get; init; }
}