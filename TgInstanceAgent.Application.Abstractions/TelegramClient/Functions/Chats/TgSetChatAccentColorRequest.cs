namespace TgInstanceAgent.Application.Abstractions.TelegramClient.Functions.Chats;

/// <summary>
/// Запрос на изменение акцентного цвета чата.
/// </summary>
public class TgSetChatAccentColorRequest
{
    /// <summary>
    /// Идентификатор чата.
    /// </summary>
    public required long ChatId { get; init; }

    /// <summary>
    /// Идентификатор акцентного цвета, который нужно использовать.
    /// </summary>
    public int? AccentColorId { get; init; }

    /// <summary>
    /// Идентификатор пользовательского эмодзи, который будет показан в заголовке ответа и фоне предварительного просмотра ссылки.
    /// </summary>
    public long? BackgroundCustomEmojiId { get; init; }
}