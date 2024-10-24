using MediatR;
using TgInstanceAgent.Application.Abstractions.Interfaces;

namespace TgInstanceAgent.Application.Abstractions.Commands.TgMessages;

/// <summary>
/// Команда для удаления реакции с сообщения.
/// </summary>
public class RemoveMessageReactionCommand : IWithChat, IWithReaction, IWithInstanceId, IRequest
{
    /// <summary>
    /// Уникальный идентификатор инстанса.
    /// </summary>
    public required Guid InstanceId { get; init; }
    
    /// <summary>
    /// Идентификатор сообщения
    /// </summary>
    public required long MessageId { get; init; }
    
    /// <summary>
    /// Идентификатор чата.
    /// При отправке запроса по идентификатору, необходимо быть уверенным, что приложению известен данный чат.
    /// </summary>
    public long? ChatId { get; init; }
    
    /// <summary>
    /// Имя пользователя для удаления реакции. Если установлено, ChatId может быть не задан.
    /// </summary>
    public string? Username { get; init; }
    
    /// <summary>
    /// Номер телефона для удаления реакции. Если установлено, ChatId и Username могут быть не заданы.
    /// </summary>
    public string? PhoneNumber { get; init; }
    
    /// <summary>
    /// Удаляемая реакция. Если установлено, EmojiId может быть не задан.
    /// </summary>
    public string? Emoji { get; init; }
    
    /// <summary>
    /// Удаляемая премиум-реакция. Если установлено, Emoji может быть не задан.
    /// </summary>
    public long? EmojiId { get; init; }
}