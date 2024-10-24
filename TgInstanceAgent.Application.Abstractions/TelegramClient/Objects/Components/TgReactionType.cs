namespace TgInstanceAgent.Application.Abstractions.TelegramClient.Objects.Components;

/// <summary>
/// Класс реакции
/// </summary>
public abstract class TgReactionType;

/// <summary>
/// Обычная реакция
/// </summary>
public class TgReactionTypeEmoji : TgReactionType
{
    /// <summary>
    /// Строковое представление реакции (эмодзи)
    /// </summary>
    public required string Emoji { get; init; }
}

/// <summary>
/// Премиум-реакция
/// </summary>
public class TgReactionTypeCustomEmoji : TgReactionType
{
    /// <summary>
    /// Идентификатор премиум-реакции
    /// </summary>
    public required long CustomEmojiId { get; init; }
}