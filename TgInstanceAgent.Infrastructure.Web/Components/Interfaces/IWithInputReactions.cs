namespace TgInstanceAgent.Infrastructure.Web.Components.Interfaces;

/// <summary>
/// Интерфейс для моделей, содержащий информацию о списках реакций
/// </summary>
public interface IWithInputReactions
{
    /// <summary>
    /// Реакции
    /// </summary>
    public string[]? Emojis { get; init; }
    
    /// <summary>
    /// Премиум-реакции
    /// </summary>
    public long[]? EmojiIds { get; init; }
}