using MediatR;
using TgInstanceAgent.Application.Abstractions.Interfaces;

namespace TgInstanceAgent.Application.Abstractions.Commands.TgProxies;

/// <summary>
/// Команда для установки прокси сервера
/// </summary>
public class SetProxyCommand : IWithInstanceId, IRequest
{
    /// <summary>
    /// Уникальный идентификатор инстанса.
    /// </summary>
    public required Guid InstanceId { get; init; }
    
    /// <summary>
    /// Домен или ip адресс прокси сервера
    /// </summary>
    public required string Server { get; init; }
    
    /// <summary>
    /// Порт прокси сервера
    /// </summary>
    public required ushort Port { get; init; }
    
    /// <summary>
    /// Время истечения срока действия в формате UTC.
    /// </summary>
    public DateTime? ExpirationTimeUtc { get; init; }
    
    /// <summary>
    /// Тип прокси Http
    /// </summary>
    public ProxyTypeHttp? ProxyTypeHttp { get; init; }
    
    /// <summary>
    /// Тип прокси Socks
    /// </summary>
    public ProxyTypeSocks? ProxyTypeSocks { get; init; }
    
    /// <summary>
    /// Тип прокси Mtproto
    /// </summary>
    public ProxyTypeMtproto? ProxyTypeMtproto { get; init; }
}

/// <summary>
/// Тип прокси Http
/// </summary>
public class ProxyTypeHttp
{
    /// <summary>
    /// Имя пользователя
    /// </summary>
    public string? Username { get; init; }
    
    /// <summary>
    /// Пароль
    /// </summary>
    public string? Password { get; init; }
    
    /// <summary>
    /// Истинно, если прокси-сервер поддерживает только HTTP-запросы
    /// </summary>
    public bool HttpOnly { get; init; }
}

/// <summary>
/// Тип прокси Socks
/// </summary>
public class ProxyTypeSocks
{
    /// <summary>
    /// Имя пользователя
    /// </summary>
    public string? Username { get; init; }
    
    /// <summary>
    /// Пароль
    /// </summary>
    public string? Password { get; init; }
}

/// <summary>
/// Тип прокси Mtproto
/// </summary>
public class ProxyTypeMtproto 
{
    /// <summary>
    /// Секрет прокси в шестнадцатеричной кодировке
    /// </summary>
    public required string Secret { get; init; }
}