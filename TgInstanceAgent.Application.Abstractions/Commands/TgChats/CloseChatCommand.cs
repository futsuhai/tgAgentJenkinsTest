using MediatR;
using TgInstanceAgent.Application.Abstractions.Interfaces;

namespace TgInstanceAgent.Application.Abstractions.Commands.TgChats;

/// <summary>
/// Команда закрытия чата.
/// </summary>
public class CloseChatCommand: IWithChat, IWithInstanceId, IRequest
{
    /// <summary>
    /// Идентификатор чата.
    /// При отправке запроса по идентификатору, необходимо быть уверенным, что приложению известен данный чат.
    /// </summary>
    public long? ChatId { get; init; }

    /// <summary>
    /// Имя пользователя для отправки реакции. Если установлено, ChatId может быть не задан.
    /// </summary>
    public string? Username { get; init; }

    /// <summary>
    /// Номер телефона для отправки реакции. Если установлено, ChatId и Username могут быть не заданы.
    /// </summary>
    public string? PhoneNumber { get; init; }

    /// <summary>
    /// Уникальный идентификатор инстанса.
    /// </summary>
    public required Guid InstanceId { get; init; }
}