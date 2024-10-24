namespace TgInstanceAgent.Application.Abstractions.Interfaces;

/// <summary>
/// Интерфейс для моделей, содержащий информацию о списках реакций
/// </summary>
public interface IWithReactions
{
    /// <summary>
    /// Реакции
    /// </summary>
    string[]? Emojis { get; init; }
    
    /// <summary>
    /// Премиум-реакции
    /// </summary>
    long[]? EmojiIds { get; init; }
}