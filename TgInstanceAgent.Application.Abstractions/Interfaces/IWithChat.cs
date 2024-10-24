namespace TgInstanceAgent.Application.Abstractions.Interfaces;

/// <summary>
/// Интерфейс для команд взаимодействия с чатом
/// </summary>
public interface IWithChat
{
    /// <summary>
    /// Данные чата.
    /// </summary>
    public long? ChatId { get; init; }

    /// <summary>
    /// Имя пользователя чата. Если установлено, ChatId может быть не задан.
    /// </summary>
    public string? Username { get; init; }

    /// <summary>
    /// Номер телефона пользователя. Если установлено, ChatId и Username могут быть не заданы.
    /// </summary>
    public string? PhoneNumber { get; init; }
}