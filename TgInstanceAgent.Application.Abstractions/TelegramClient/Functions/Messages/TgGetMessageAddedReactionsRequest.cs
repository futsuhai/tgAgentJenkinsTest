using TgInstanceAgent.Application.Abstractions.TelegramClient.Functions.Components;

namespace TgInstanceAgent.Application.Abstractions.TelegramClient.Functions.Messages;

/// <summary>
/// Запрос на получение списка добавленных на сообщение реакций.
/// </summary>
public class TgGetMessageAddedReactionsRequest : TgGetMessageReactionsRequest
{
    /// <summary>
    /// Реакция
    /// </summary>
    public required TgInputReaction? Reaction { get; init; }
    
    /// <summary>
    /// Лимит реакций
    /// </summary>
    public required int Limit { get; init; }
    
    /// <summary>
    /// Смещение
    /// </summary>
    public string? Offset { get; init; }
}