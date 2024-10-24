using TgInstanceAgent.Application.Abstractions.TelegramClient.Functions.Components.Visitors;

namespace TgInstanceAgent.Application.Abstractions.TelegramClient.Functions.Components;

/// <summary>
/// Абстрактный тип прокси
/// </summary>
public abstract class TgInputProxyType
{
    /// <summary>
    /// Принять посетителя типов прокси.
    /// </summary>
    /// <param name="privacySettingsAsyncVisitor">Посетитель</param>
    public abstract void Accept(IProxyVisitor privacySettingsAsyncVisitor);
}

/// <summary>
/// Тип прокси Http
/// </summary>
public class TgInputProxyTypeHttp : TgInputProxyType
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

    /// <summary>
    /// Принять посетителя типов прокси.
    /// </summary>
    /// <param name="privacySettingsAsyncVisitor">Посетитель</param>
    public override void Accept(IProxyVisitor privacySettingsAsyncVisitor) => privacySettingsAsyncVisitor.Visit(this);
}


/// <summary>
/// Тип прокси Socks
/// </summary>
public class TgInputProxyTypeSocks : TgInputProxyType
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
    /// Принять посетителя типов прокси.
    /// </summary>
    /// <param name="privacySettingsAsyncVisitor">Посетитель</param>
    public override void Accept(IProxyVisitor privacySettingsAsyncVisitor) => privacySettingsAsyncVisitor.Visit(this);
}

/// <summary>
/// Тип прокси Mtproto
/// </summary>
public class TgInputProxyTypeMtproto : TgInputProxyType
{
    /// <summary>
    /// Секрет прокси в шестнадцатеричной кодировке
    /// </summary>
    public required string Secret { get; init; }

    /// <summary>
    /// Принять посетителя типов прокси.
    /// </summary>
    /// <param name="privacySettingsAsyncVisitor">Посетитель</param>
    public override void Accept(IProxyVisitor privacySettingsAsyncVisitor) => privacySettingsAsyncVisitor.Visit(this);
}