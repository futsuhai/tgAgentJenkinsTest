using TgInstanceAgent.Application.Abstractions.TelegramClient.Functions.Components.Visitors;

namespace TgInstanceAgent.Application.Abstractions.TelegramClient.Functions.Components;

/// <summary>
/// Абстрактный класс, представляющий чат
/// </summary>
public abstract class TgInputUser
{
    /// <summary>
    /// Метод, принимающий посетителя фона асинхронно
    /// </summary>
    public abstract Task AcceptAsync(IUserVisitor visitor);
}

/// <summary>
/// Класс, представляющий чат, полученный через Id 
/// </summary>
public class TgInputUserId : TgInputUser
{
    /// <summary>
    /// Данные чата.
    /// </summary>
    public required long UserId { get; init; }
    
    /// <summary>
    /// Метод, принимающий посетителя фона асинхронно
    /// </summary>
    public override Task AcceptAsync(IUserVisitor visitor)
    {
        return visitor.VisitAsync(this);
    }
}

/// <summary>
/// Класс, представляющий чат, полученный через Username 
/// </summary>
public class TgInputUserUsername : TgInputUser
{
    /// <summary>
    /// Имя пользователя чата. Если установлено, Chat может быть не задан.
    /// </summary>
    public required string Username { get; init; }
    
    /// <summary>
    /// Метод, принимающий посетителя фона асинхронно
    /// </summary>
    public override Task AcceptAsync(IUserVisitor visitor)
    {
        return visitor.VisitAsync(this);
    }
}

/// <summary>
/// Класс, представляющий чат, полученный через номер телефона
/// </summary>
public class TgInputUserPhoneNumber : TgInputUser
{
    /// <summary>
    /// Номер телефона пользователя. Если установлено, Chat и Username могут быть не заданы.
    /// </summary>
    public required string PhoneNumber { get; init; }
    
    /// <summary>
    /// Метод, принимающий посетителя фона асинхронно
    /// </summary>
    public override Task AcceptAsync(IUserVisitor visitor)
    {
        return visitor.VisitAsync(this);
    }
}