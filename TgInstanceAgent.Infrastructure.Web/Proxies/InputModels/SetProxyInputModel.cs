using TgInstanceAgent.Infrastructure.Web.Proxies.InputModels.Components;

namespace TgInstanceAgent.Infrastructure.Web.Proxies.InputModels;

/// <summary>
/// Модель входных данных для установки прокси
/// </summary>
public class SetProxyInputModel
{
    /// <summary>
    /// Домен или ip адресс прокси сервера
    /// </summary>
    public string? Server { get; init; }
    
    /// <summary>
    /// Порт прокси сервера
    /// </summary>
    public ushort Port { get; init; }
    
    /// <summary>
    /// Время истечения срока действия в формате UTC.
    /// </summary>
    public DateTime? ExpirationTimeUtc { get; init; }
    
    /// <summary>
    /// Тип прокси сервера Http
    /// </summary>
    public ProxyTypeHttpInputModel? ProxyTypeHttpInputModel { get; init; }
    
    /// <summary>
    /// Тип прокси сервера Socks
    /// </summary>
    public ProxyTypeSocksInputModel? ProxyTypeSocksInputModel { get; init; }
    
    /// <summary>
    /// Тип прокси сервера Mtproto
    /// </summary>
    public ProxyTypeMtprotoInputModel? ProxyTypeMtprotoInputModel { get; init; }
}