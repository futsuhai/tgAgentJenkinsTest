namespace TgInstanceAgent.Application.Abstractions.TelegramClient.Functions.Users;

/// <summary>
/// Запрос на добавление контакта
/// </summary>
public class TgImportContactRequest
{
    /// <summary>
    /// Номер телефона
    /// </summary>
    public required string PhoneNumber { get; init; }
    
    /// <summary>
    /// Имя
    /// </summary>
    public required string FirstName { get; init; }
    
    /// <summary>
    /// Фамилия
    /// </summary>
    public string? LastName { get; init; }
}