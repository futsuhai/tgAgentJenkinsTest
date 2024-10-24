namespace TgInstanceAgent.Infrastructure.Web.Components.Interfaces;

/// <summary>
/// Интерфейс для моделей, содержащий информацию об отправляемой реакции
/// </summary>
public interface IWithInputReaction
{
    /// <summary>
    /// Реакция
    /// </summary>
    public string? Emoji { get; init; }
    
    /// <summary>
    /// Премиум-реакция
    /// </summary>
    public long? EmojiId { get; init; }
}