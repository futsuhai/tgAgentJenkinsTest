using TgInstanceAgent.Application.Abstractions.TelegramClient.Functions.Components.Visitors;

namespace TgInstanceAgent.Application.Abstractions.TelegramClient.Functions.Components;

/// <summary>
/// Абстрактный класс, представляющий отправителя сообщения
/// </summary>
public abstract class TgInputMessageSender
{
    /// <summary>
    /// Метод, принимающий посетителя фона асинхронно
    /// </summary>
    public abstract void Accept(IMessageSenderVisitor visitor);
}

/// <summary>
/// Класс, представляющий пользователя, полученный через Id 
/// </summary>
public class TgInputMessageSenderUser : TgInputMessageSender
{
    /// <summary>
    /// Идентификатор пользователя.
    /// </summary>
    public required long UserId { get; init; }

    /// <summary>
    /// Метод, принимающий посетителя фона асинхронно
    /// </summary>
    public override void Accept(IMessageSenderVisitor visitor) => visitor.Visit(this);
}

/// <summary>
/// Класс, представляющий чат, полученный через Id 
/// </summary>
public class TgInputMessageSenderChat : TgInputMessageSender
{
    /// <summary>
    /// Идентификатор чата.
    /// </summary>
    public required long ChatId { get; init; }

    /// <summary>
    /// Метод, принимающий посетителя фона асинхронно
    /// </summary>
    public override void Accept(IMessageSenderVisitor visitor) => visitor.Visit(this);
}