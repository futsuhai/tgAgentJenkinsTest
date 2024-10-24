using TgInstanceAgent.Infrastructure.Web.Components.Interfaces;

namespace TgInstanceAgent.Infrastructure.Web.Stories.InputModels;

/// <summary>
/// Модель входных данных для получения информацию об активных историях пользователя
/// </summary>
public class GetChatActiveStoriesInputModel : IWithInputChat
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
}
