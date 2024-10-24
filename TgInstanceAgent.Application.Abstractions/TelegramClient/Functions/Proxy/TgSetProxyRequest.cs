using TgInstanceAgent.Application.Abstractions.TelegramClient.Functions.Components;

namespace TgInstanceAgent.Application.Abstractions.TelegramClient.Functions.Proxy;

/// <summary>
/// Запрос на установку прокси сервера
/// </summary>
public class TgSetProxyRequest
{
    /// <summary>
    /// Домен или ip адрес прокси сервера
    /// </summary>
    public required string Server { get; init; }
    
    /// <summary>
    /// Порт прокси сервера
    /// </summary>
    public required ushort Port { get; init; }
    
    /// <summary>
    /// Тип прокси
    /// </summary>
    public required TgInputProxyType ProxyType { get; init; }
}