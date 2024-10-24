namespace TgInstanceAgent.Application.Abstractions.Interfaces;

/// <summary>
/// Интерфейс для команд взаимодействия с пользователем
/// </summary>
public interface IWithUser
{
    /// <summary>
    /// Идентификатор пользователя.
    /// При отправке запроса по идентификатору, необходимо быть уверенным, что приложению известен данный пользователь.
    /// </summary>
    public long? UserId { get; init; }

    /// <summary>
    /// Имя пользователя чата. Если установлено, Chat может быть не задан.
    /// </summary>
    public string? Username { get; init; }

    /// <summary>
    /// Номер телефона пользователя.
    /// </summary>
    public string? PhoneNumber { get; init; }
}