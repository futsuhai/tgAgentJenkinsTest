namespace TgInstanceAgent.Application.Abstractions.TelegramClient.Objects.Components;

/// <summary>
/// Класс отправителя сообщения
/// </summary>
public abstract class TgMessageSender;

/// <summary>
/// Внешний отправитель
/// </summary>
public class TgMessageSenderChat : TgMessageSender
{
    /// <summary>
    /// Идентификатор чата
    /// </summary>
    public required long ChatId { get; init; }
}

/// <summary>
/// Текущий пользователь
/// </summary>
public class TgMessageSenderUser : TgMessageSender
{
    /// <summary>
    /// Идентификатор пользователя
    /// </summary>
    public required long UserId { get; init; }
}