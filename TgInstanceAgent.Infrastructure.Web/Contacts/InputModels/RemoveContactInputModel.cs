using TgInstanceAgent.Infrastructure.Web.Components.Interfaces;

namespace TgInstanceAgent.Infrastructure.Web.Contacts.InputModels;

/// <summary>
/// Модель для удаления контакта пользователя
/// </summary>
public class RemoveContactInputModel : IWithInputUser
{
    /// <summary>
    /// Данные чата.
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