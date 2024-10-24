namespace TgInstanceAgent.Application.Abstractions.TelegramClient.Functions.Chats;

/// <summary>
/// Запрос на изменение цвета профиля группового чата
/// </summary>
public class TgSetChatProfileAccentColorRequest
{
    /// <summary>
    /// Идентификатор чата.
    /// </summary>
    public required long ChatId { get; init; }
    
    /// <summary>
    /// Идентификатор цвета
    /// </summary>
    public required int ProfileAccentColorId { get; init; }

    /// <summary>
    /// Идентификатор премиум емозди
    /// </summary>
    public required long ProfileBackgroundCustomEmojiId { get; init; }
}