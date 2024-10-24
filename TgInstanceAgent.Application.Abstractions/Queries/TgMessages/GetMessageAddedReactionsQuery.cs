using MediatR;
using TgInstanceAgent.Application.Abstractions.Interfaces;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Objects.Messages;

namespace TgInstanceAgent.Application.Abstractions.Queries.TgMessages;

/// <summary>
/// Запрос на получение списка добавленных на сообщение реакций
/// </summary>
public class GetMessageAddedReactionsQuery : IWithChat, IWithReaction, IWithInstanceId, IRequest<TgMessageAddedReactions>
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
    /// Кол-во сообщений в запросе.
    /// </summary>
    public required int Limit { get; init; }
    
    /// <summary>
    /// Смещение
    /// </summary>
    public string? Offset { get; init; }
    
    /// <summary>
    /// Идентификатор сообщения
    /// </summary>
    public required long MessageId { get; init; }

    /// <summary>
    /// Реакция на сообщение. Может быть не задано
    /// </summary>
    public string? Emoji { get; init; }
    
    /// <summary>
    /// Идентификатор премиум-реакции. Может быть не задано
    /// </summary>
    public long? EmojiId { get; init; }
}