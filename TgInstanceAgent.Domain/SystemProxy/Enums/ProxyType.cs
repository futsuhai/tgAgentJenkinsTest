namespace TgInstanceAgent.Domain.SystemProxy.Enums;

/// <summary>
/// Перечисление типов прокси-серверов.
/// </summary>
public enum ProxyType
{
    /// <summary>
    /// Http
    /// </summary>
    Https,
    
    /// <summary>
    /// Http
    /// </summary>
    Http,
    
    /// <summary>
    /// Socks
    /// </summary>
    Socks,
    
    /// <summary>
    /// Mtproto
    /// </summary>
    Mtproto
}