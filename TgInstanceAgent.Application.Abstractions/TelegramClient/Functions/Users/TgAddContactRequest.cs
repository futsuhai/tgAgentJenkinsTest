using TgInstanceAgent.Application.Abstractions.TelegramClient.Functions.Components;

namespace TgInstanceAgent.Application.Abstractions.TelegramClient.Functions.Users;

/// <summary>
/// Запрос на обновление контакта (добавление или изменение)
/// </summary>
public class TgAddContactRequest
{
    /// <summary>
    /// Чат
    /// </summary>
    public required TgInputUser User { get; init; }
    
    /// <summary>
    /// Имя
    /// </summary>
    public required string FirstName { get; init; }

    /// <summary>
    /// Фамилия
    /// </summary>
    public string? LastName { get; init; }
}