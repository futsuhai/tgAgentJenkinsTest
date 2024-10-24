using TgInstanceAgent.Application.Abstractions.TelegramClient.Functions.Components.Visitors;

namespace TgInstanceAgent.Application.Abstractions.TelegramClient.Functions.Components;

/// <summary>
/// Абстрактный класс, представляющий чат
/// </summary>
public abstract class TgInputChat
{
    /// <summary>
    /// Метод, принимающий посетителя фона асинхронно
    /// </summary>
    public abstract Task AcceptAsync(IChatVisitor visitor);
}

/// <summary>
/// Класс, представляющий чат, полученный через Id 
/// </summary>
public class TgInputChatId : TgInputChat
{
    /// <summary>
    /// Данные чата.
    /// </summary>
    public required long ChatId { get; init; }

    /// <summary>
    /// Метод, принимающий посетителя фона асинхронно
    /// </summary>
    public override Task AcceptAsync(IChatVisitor visitor) => visitor.VisitAsync(this);
}

/// <summary>
/// Класс, представляющий чат, полученный через Username 
/// </summary>
public class TgInputChatUsername : TgInputChat
{
    /// <summary>
    /// Имя пользователя чата. Если установлено, Chat может быть не задан.
    /// </summary>
    public required string Username { get; init; }

    /// <summary>
    /// Метод, принимающий посетителя фона асинхронно
    /// </summary>
    public override Task AcceptAsync(IChatVisitor visitor) => visitor.VisitAsync(this);
}

/// <summary>
/// Класс, представляющий чат, полученный через номер телефона
/// </summary>
public class TgInputChatPhoneNumber : TgInputChat
{
    /// <summary>
    /// Номер телефона пользователя. Если установлено, Chat и Username могут быть не заданы.
    /// </summary>
    public required string PhoneNumber { get; init; }

    /// <summary>
    /// Метод, принимающий посетителя фона асинхронно
    /// </summary>
    public override Task AcceptAsync(IChatVisitor visitor) => visitor.VisitAsync(this);
}