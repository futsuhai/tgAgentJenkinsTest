namespace TgInstanceAgent.Application.Abstractions.TelegramClient.Functions.Components.Visitors;

/// <summary>
/// Интерфейс для асинхронного посещения реакций.
/// </summary>
public interface IReactionVisitor
{
    /// <summary>
    /// Асинхронное посещение реакции с эмодзи.
    /// </summary>
    /// <param name="emoji">Эмодзи для обработки.</param>
    void Visit(TgInputReactionEmoji emoji);

    /// <summary>
    /// Асинхронное посещение реакции с пользовательским премиум эмодзи.
    /// </summary>
    /// <param name="customEmoji">Пользовательское премиум эмодзи для обработки.</param>
    void Visit(TgInputCustomPremiumEmoji customEmoji);
}