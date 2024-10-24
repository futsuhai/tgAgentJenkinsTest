namespace TgInstanceAgent.Infrastructure.Web.Contacts.InputModels;

/// <summary>
/// Модель входных данных для добавления контакта
/// </summary>
public class ImportContactInputModel
{
    /// <summary>
    /// Имя
    /// </summary>
    public string? FirstName { get; init; }

    /// <summary>
    /// Фамилия
    /// </summary>
    public string? LastName { get; init; }
    
    /// <summary>
    /// Номер телефона пользователя. Если установлено, Chat и Username могут быть не заданы.
    /// </summary>
    public string? PhoneNumber { get; init; }
}