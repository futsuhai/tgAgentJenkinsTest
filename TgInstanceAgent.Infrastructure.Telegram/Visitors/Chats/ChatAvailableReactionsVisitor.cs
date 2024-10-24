using TdLib;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Functions.Components.Visitors;

namespace TgInstanceAgent.Infrastructure.Telegram.Visitors.Chats;

/// <summary>
/// Класс для асинхронного посещения типов доступных реакций.
/// </summary>
public class ChatAvailableReactionsVisitor : IChatAvailableReactionsVisitor
{
    /// <summary>
    /// Тип доступных реакций.
    /// </summary>
    public TdApi.ChatAvailableReactions? Type { get; private set; }
    
    /// <inheritdoc/>
    /// <summary>
    /// Асинхронное посещение типов доступных реакций
    /// </summary>
    public void Visit(TdApi.ReactionType?[] reactions, int maxReactionCount)
    {
        if (reactions is { Length: > 0 } && reactions.Any(reaction => reaction != null))
        {
            // Если массив содержит хотя бы одну реакцию, используем ChatAvailableReactionsSome
            Type = new TdApi.ChatAvailableReactions.ChatAvailableReactionsSome
            {
                Reactions = reactions.Where(reaction => reaction != null).ToArray(),
                MaxReactionCount = maxReactionCount
            };
        }
        else if (maxReactionCount > 0)
        {
            // Если массив пуст и maxReactionCount больше нуля, используем ChatAvailableReactionsAll
            Type = new TdApi.ChatAvailableReactions.ChatAvailableReactionsAll
            {
                MaxReactionCount = maxReactionCount
            };
        }
        else
        {
            // Если массив пуст и maxReactionCount равен нулю, запрещаем все реакции
            Type = null;
        }
    }
}