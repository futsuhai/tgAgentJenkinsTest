using TgInstanceAgent.Application.Abstractions.TelegramClient.Functions.Components;

namespace TgInstanceAgent.Application.Abstractions.TelegramClient.Functions.Profile;

/// <summary>
/// Запрос на установку фотографии профиля.
/// </summary>
public class TgSetProfilePhotoRequest
{
    /// <summary>
    /// Фотография профиля
    /// </summary>
    public required TgInputProfilePhoto ProfilePhoto { get; init; }
}