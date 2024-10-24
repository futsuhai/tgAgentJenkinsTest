using TgInstanceAgent.Infrastructure.Web.Components.Interfaces;

namespace TgInstanceAgent.Infrastructure.Web.Messages.InputModels;

/// <summary>
/// Модель получения списка доступных реакций на сообщение.
/// </summary>
public class GetMessageReactionsInputModel : IWithInputChat
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
    /// Идентификатор сообщения.
    /// </summary>
    public long MessageId { get; init; }
}