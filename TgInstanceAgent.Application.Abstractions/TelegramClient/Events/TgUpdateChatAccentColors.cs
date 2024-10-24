using TgInstanceAgent.Application.Abstractions.TelegramClient.Events.Visitors;

namespace TgInstanceAgent.Application.Abstractions.TelegramClient.Events;

/// <summary>
/// Событие изменения акцентного цвета в чате
/// </summary>
public class TgUpdateChatAccentColors : TgEvent
{
    /// <summary>
    /// Идентификатор чата.
    /// </summary>
    public required long ChatId { get; init; }

    /// <summary>
    /// Идентификатор акцентного цвета, который нужно использовать.
    /// </summary>
    public int AccentColorId { get; init; }

    /// <summary>
    /// Идентификатор пользовательского эмодзи, который будет показан в заголовке ответа и фоне предварительного просмотра ссылки.
    /// </summary>
    public long BackgroundCustomEmojiId { get; init; }

    /// <summary>
    /// Идентификатор акцентного цвета профиля пользователя
    /// </summary>
    public int ProfileAccentColorId { get; init; }

    /// <summary>
    /// Идентификатор пользовательского эмодзи, который будет показан в заголовке ответа и фоне предварительного просмотра ссылки.
    /// </summary>
    public long ProfileBackgroundCustomEmojiId { get; init; }
    
    /// <summary>
    /// Принять посетителя события.
    /// </summary>
    public override void Accept(ITgEventVisitor eventVisitor) => eventVisitor.Visit(this);
}