using TgInstanceAgent.Application.Abstractions.Interfaces;
using TgInstanceAgent.Infrastructure.Web.Components.Interfaces;

namespace TgInstanceAgent.Infrastructure.Web.Chats.InputModels;

/// <summary>
/// Входная модель для установки доступных реакций в чате
/// </summary>
public class SetChatAvailableReactionsInputModel : IWithInputReactions
{
    /// <summary>
    /// Идентификатор чата.
    /// </summary>
    public long? ChatId { get; init; }

    /// <summary>
    /// Реакции
    /// </summary>
    public string[]? Emojis { get; init; }
    
    /// <summary>
    /// Премиум-реакции
    /// </summary>
    public long[]? EmojiIds { get; init; }
    
    /// <summary>
    /// Максимальное количество реакций
    /// </summary>
    public int? MaxReactionCount { get; init; }
}