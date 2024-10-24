using MediatR;
using TgInstanceAgent.Application.Abstractions.Interfaces;

namespace TgInstanceAgent.Application.Abstractions.Commands.TgMessages;

/// <summary>
/// Команда для удаления сообщений в чате
/// </summary>
public class DeleteMessagesCommand : IWithChat, IWithInstanceId, IRequest
{
    /// <summary>
    /// Уникальный идентификатор инстанса.
    /// </summary>
    public required Guid InstanceId { get; init; }
    
    /// <summary>
    /// Идентификатор чата.
    /// При отправке запроса по идентификатору, необходимо быть уверенным, что приложению известен данный чат.
    /// </summary>
    public long? ChatId { get; init; }

    /// <summary>
    /// Имя пользователя чата. Если установлено, Chat может быть не задан.
    /// </summary>
    public string? Username { get; init; }

    /// <summary>
    /// Номер телефона пользователя. Если установлено, Chat и Username могут быть не заданы.
    /// </summary>
    public string? PhoneNumber { get; init; }
    
    /// <summary>
    /// Массив идентификаторо сообщений для пересылки
    /// </summary>
    public required long[] MessageIds { get; init; }
    
    /// <summary>
    /// Истинно, если требуется удалить для всех участников чата
    /// </summary>
    public bool Revoke { get; init; }
}