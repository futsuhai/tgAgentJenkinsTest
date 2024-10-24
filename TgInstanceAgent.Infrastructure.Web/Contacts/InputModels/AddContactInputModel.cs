using TgInstanceAgent.Infrastructure.Web.Components.Interfaces;

namespace TgInstanceAgent.Infrastructure.Web.Contacts.InputModels;

/// <summary>
/// Модель входных данных для добавления контакта
/// </summary>
public class AddContactInputModel : IWithInputUser
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
    
    /// <summary>
    /// Имя
    /// </summary>
    public string? FirstName { get; init; }

    /// <summary>
    /// Фамилия
    /// </summary>
    public string? LastName { get; init; }
}