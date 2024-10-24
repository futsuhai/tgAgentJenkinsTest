using TgInstanceAgent.Application.Abstractions.TelegramClient.Functions.Components;

namespace TgInstanceAgent.Application.Abstractions.TelegramClient.Functions.Chats;

/// <summary>
/// Запрос на изменение доступных реакций в чате
/// </summary>
public class TgSetChatAvailableReactionsRequest
{
    /// <summary>
    /// Идентификатор чата.
    /// </summary>
    public required long ChatId { get; init; }
    
    /// <summary>
    /// Реакции
    /// </summary>
    public required TgInputReaction[] Reactions { get; init; }
    
    /// <summary>
    /// Максимальное количество реакций
    /// </summary>
    public required int MaxReactionCount { get; init; }
}