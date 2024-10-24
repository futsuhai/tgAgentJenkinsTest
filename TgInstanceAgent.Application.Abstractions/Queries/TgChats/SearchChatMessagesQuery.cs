using MediatR;
using TgInstanceAgent.Application.Abstractions.Interfaces;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Functions.Enums;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Objects.Components;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Objects.Messages;

namespace TgInstanceAgent.Application.Abstractions.Queries.TgChats;

/// <summary>
/// Запрос на поиск сообщений в чате, который представляет собой запрос к инстансу.
/// Результат запроса - коллекция сообщений.
/// </summary>
public class SearchChatMessagesQuery : IWithChat, IWitMessageSender, IWithInstanceId, IRequest<TgCountResult<TgMessage>>
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
    /// Идентификатор пользователя.
    /// </summary>
    public long? SenderUserId { get; init; }
    
    /// <summary>
    /// Идентификатор чата.
    /// </summary>
    public long? SenderChatId { get; init; }
    
    /// <summary>
    /// Строка с ключевыми словами для поиска.
    /// </summary>
    public string? Query { get; init; }

    /// <summary>
    /// Кол-во сообщений в запросе.
    /// </summary>
    public required int Limit { get; init; }
    
    /// <summary>
    /// Смещение
    /// </summary>
    public int? Offset { get; init; }
    
    /// <summary>
    /// Начиная с идентификатора сообщения.
    /// </summary>
    public long? FromMessageId { get; init; }
    
    /// <summary>
    /// Идентификатор потока сообщений (thread) в чате.
    /// Если указан, поиск будет выполнен только внутри этого потока.
    /// </summary>
    public long? MessageThreadId { get; init; }
    
    /// <summary>
    /// Идентификатор темы сохраненных сообщений.
    /// Используется для поиска сообщений внутри определенной темы сохраненных сообщений.
    /// </summary>
    public long? SavedMessagesTopicId { get; init; }
    
    /// <summary>
    /// Тип сообщений
    /// </summary>
    public TgMessageFilterType Filter { get; init; }
}