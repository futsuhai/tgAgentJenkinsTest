namespace TgInstanceAgent.Infrastructure.Web.Chats.InputModels;

/// <summary>
/// Модель входных данных для смены цвета акцента чата.
/// </summary>
public class SetChatAccentColorInputModel
{
    /// <summary>
    /// Идентификатор чата.
    /// </summary>
    public long? ChatId { get; init; }

    /// <summary>
    /// Идентификатор акцентного цвета, который нужно использовать.
    /// </summary>
    public int? AccentColorId { get; init; }

    /// <summary>
    /// Идентификатор пользовательского эмодзи, который будет показан в заголовке ответа и фоне предварительного просмотра ссылки.
    /// </summary>
    public long? BackgroundCustomEmojiId { get; init; }
}