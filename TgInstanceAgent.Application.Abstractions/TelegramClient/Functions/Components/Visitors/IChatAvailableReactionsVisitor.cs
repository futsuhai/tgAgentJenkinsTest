using TdLib;

namespace TgInstanceAgent.Application.Abstractions.TelegramClient.Functions.Components.Visitors;

/// <summary>
/// Интерфейс для асинхронного посещения типов доступных реакций.
/// </summary>
public interface IChatAvailableReactionsVisitor
{
    /// <summary>
    /// Асинхронное посещение типов доступных реакций
    /// </summary>
    /// <param name="reactions">Реакции</param>
    /// <param name="maxReactionCount">Максимальное количество реакций</param>
    void Visit(TdApi.ReactionType?[] reactions, int maxReactionCount);
}