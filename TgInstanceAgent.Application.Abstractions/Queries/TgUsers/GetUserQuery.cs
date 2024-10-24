using MediatR;
using TgInstanceAgent.Application.Abstractions.Interfaces;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Objects.Users;

namespace TgInstanceAgent.Application.Abstractions.Queries.TgUsers;

/// <summary>
/// Запрос на получение информации о пользователе Telegram.
/// Результат запроса - данные пользователя.
/// </summary>
public class GetUserQuery : IWithUser, IWithInstanceId, IRequest<TgUser>
{
    /// <summary>
    /// Уникальный идентификатор инстанса.
    /// </summary>
    public required Guid InstanceId { get; init; }
    
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
    /// Номер телефона пользователя. Если установлено, Chat и Username могут быть не заданы.
    /// </summary>
    public string? PhoneNumber { get; init; }
}