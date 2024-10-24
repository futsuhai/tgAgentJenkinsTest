using TgInstanceAgent.Infrastructure.Web.Components.Interfaces;

namespace TgInstanceAgent.Infrastructure.Web.Chats.InputModels;

/// <summary>
/// Модель входных данных для получения истории чата.
/// </summary>
public class GetChatHistoryInputModel : IWithInputChat, IWithInputLimit
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
    /// Лимит сообщений.
    /// </summary>
    public int Limit { get; init; } = 15;

    /// <summary>
    /// Смещение.
    /// </summary>
    public int? Offset { get; init; }

    /// <summary>
    /// Начиная с идентификатора сообщения.
    /// </summary>
    public long? FromMessageId { get; init; }
    
    /// <summary>
    /// Флаг, необходимо ли включить в ответ данные пользователей
    /// </summary>
    public bool IncludeUsers { get; init; }
}