using TgInstanceAgent.Application.Abstractions.TelegramClient.Functions.Enums;
using TgInstanceAgent.Infrastructure.Web.Components.Interfaces;

namespace TgInstanceAgent.Infrastructure.Web.Chats.InputModels;

/// <summary>
/// Модель для поиска сообщений в чате.
/// </summary>
public class SearchChatMessagesInputModel : IWithInputChat, IWithInputLimit
{
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
    /// Лимит сообщений.
    /// </summary>
    public int Limit { get; init; } = 100;

    /// <summary>
    /// Смещение.
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
    /// Тип фильтра.
    /// </summary>
    public TgMessageFilterType? Filter { get; init; }
}