using TdLib;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Functions.Components;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Functions.Components.Visitors;

namespace TgInstanceAgent.Infrastructure.Telegram.Visitors.Proxies;

/// <summary>
/// Посетитель типа прокси
/// </summary>
public class ProxyVisitor : IProxyVisitor
{
    /// <summary>
    /// Тип прокси
    /// </summary>
    public TdApi.ProxyType? ProxyType { get; private set; }

    /// <summary>
    /// Метод посещения типа прокси Http
    /// </summary>
    /// <param name="tgInputProxyTypeHttp">Тип прокси Http</param>
    public void Visit(TgInputProxyTypeHttp tgInputProxyTypeHttp)
    {
        // Устанавливаем тип прокси
        ProxyType = new TdApi.ProxyType.ProxyTypeHttp
        {
            // Имя пользователя
            Username = tgInputProxyTypeHttp.Username,
            
            // Пароль
            Password = tgInputProxyTypeHttp.Password,
            
            //Истинно, если прокси-сервер поддерживает только HTTP-запросы
            HttpOnly = tgInputProxyTypeHttp.HttpOnly
        };
    }

    /// <summary>
    /// Метод посещения типа прокси Socks
    /// </summary>
    /// <param name="tgInputProxyTypeSocks">Тип прокси Socks</param>
    public void Visit(TgInputProxyTypeSocks tgInputProxyTypeSocks)
    {
        // Устанавливаем тип прокси
        ProxyType = new TdApi.ProxyType.ProxyTypeSocks5
        {
            // Имя пользователя
            Username = tgInputProxyTypeSocks.Username,
            
            // Пароль
            Password = tgInputProxyTypeSocks.Password
        };
    }

    /// <summary>
    /// Метод посещения типа прокси Mtproto
    /// </summary>
    /// <param name="tgInputProxyTypeMtproto">Тип прокси Mtproto</param>
    public void Visit(TgInputProxyTypeMtproto tgInputProxyTypeMtproto)
    {
        // Устанавливаем тип прокси
        ProxyType = new TdApi.ProxyType.ProxyTypeMtproto
        {
            // Секрет
            Secret = tgInputProxyTypeMtproto.Secret
        };
    }
}