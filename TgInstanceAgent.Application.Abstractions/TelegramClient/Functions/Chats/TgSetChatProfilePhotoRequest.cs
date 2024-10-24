using TgInstanceAgent.Application.Abstractions.TelegramClient.Functions.Components;

namespace TgInstanceAgent.Application.Abstractions.TelegramClient.Functions.Chats;

/// <summary>
/// Запрос на установку фотографии профиля группового чата.
/// </summary>
public class TgSetChatProfilePhotoRequest
{
    /// <summary>
    /// Идентификатор чата.
    /// </summary>
    public required long ChatId { get; init; }
    
    /// <summary>
    /// Фотография профиля группового чата.
    /// </summary>
    public required TgInputProfilePhoto ChatProfilePhoto { get; init; }
}