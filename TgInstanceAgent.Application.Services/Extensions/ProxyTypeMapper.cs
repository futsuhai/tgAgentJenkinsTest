using TgInstanceAgent.Application.Abstractions.Commands.TgProxies;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Exceptions;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Functions.Components;
using TgInstanceAgent.Domain.SystemProxy.Enums;

namespace TgInstanceAgent.Application.Services.Extensions;

/// <summary>
/// Класс, предоставляющий методы для преобразования типов прокси.
/// </summary>
public static class ProxyTypeMapper
{
    /// <summary>
    /// Метод получения типа прокси
    /// </summary>
    /// <param name="command">Команда, содержащая тип прокси</param>
    /// <returns>Тип прокси</returns>
    /// <exception cref="InvalidProxyTypeException">Вызывается, если не определен тип прокси</exception>
    public static TgInputProxyType GetProxyType(this SetProxyCommand command)
    {
        // Если тип прокси Http не Null
        if (command.ProxyTypeHttp != null) 
        {
            // Возвращаем тип прокси Http
            return new TgInputProxyTypeHttp
            {
                // Имя пользователя
                Username = command.ProxyTypeHttp.Username,
                
                // Пароль
                Password = command.ProxyTypeHttp.Password,
                
                // Истинно, если прокси-сервер поддерживает только HTTP-запросы
                HttpOnly = command.ProxyTypeHttp.HttpOnly
            };
        }

        // Если тип прокси Socks не null
        if (command.ProxyTypeSocks != null)
        {
            // Возвращаем тип прокси Socks
            return new TgInputProxyTypeSocks
            {
                // Имя пользователя
                Username = command.ProxyTypeSocks.Username,
                
                // Пароль
                Password = command.ProxyTypeSocks.Password
            };
        }

        // Если тип прокси Mtproto не null
        if (command.ProxyTypeMtproto != null)
        {
            // Возвращаем тип прокси Mtproto
            return new TgInputProxyTypeMtproto
            {
                // Секрет прокси в шестнадцатеричной кодировке
                Secret = command.ProxyTypeMtproto.Secret
            };
        }

        // Вызываем исключение невалидного типа прокси
        throw new InvalidProxyTypeException();
    }

    /// <summary>
    /// Метод получения типа прокси
    /// </summary>
    /// <param name="command">Команда, содержащая тип прокси</param>
    /// <returns>Тип прокси</returns>
    /// <exception cref="InvalidProxyTypeException">Вызывается, если не определен тип прокси</exception>
    public static ProxyType GetProxyTypeEnum(this SetProxyCommand command)
    {
        // Если тип прокси Http не Null
        if (command.ProxyTypeHttp != null) 
        {
            // Возвращаем тип прокси Http
            return ProxyType.Http;
        }

        // Если тип прокси Socks не null
        if (command.ProxyTypeSocks != null)
        {
            // Возвращаем тип прокси Socks
            return ProxyType.Socks;
        }

        // Если тип прокси Mtproto не null
        if (command.ProxyTypeMtproto != null)
        {
            // Возвращаем тип прокси Mtproto
            return ProxyType.Mtproto;
        }

        // Вызываем исключение невалидного типа прокси
        throw new InvalidProxyTypeException();
    }
}