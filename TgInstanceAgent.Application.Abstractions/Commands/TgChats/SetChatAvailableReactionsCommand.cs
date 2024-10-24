using MediatR;
using TgInstanceAgent.Application.Abstractions.Interfaces;

namespace TgInstanceAgent.Application.Abstractions.Commands.TgChats;

/// <summary>
/// Команда изменения доступных реакций в чате
/// </summary>
public class SetChatAvailableReactionsCommand : IWithReactions, IWithInstanceId, IRequest
{
    /// <summary>
    /// Уникальный идентификатор инстанса.
    /// </summary>
    public required Guid InstanceId { get; init; }
    
    /// <summary>
    /// Идентификатор чата.
    /// </summary>
    public required long ChatId { get; init; }
    
    /// <summary>
    /// Реакция на сообщение. Если установлено, EmojiId может быть не задан.
    /// </summary>
    public string[]? Emojis { get; init; }
    
    /// <summary>
    /// Идентификатор премиум-реакции. Если установлено, Emoji может быть не задан.
    /// </summary>
    public long[]? EmojiIds { get; init; }
    
    /// <summary>
    /// Максимальное количество реакций
    /// </summary>
    public required int MaxReactionCount { get; init; }
}