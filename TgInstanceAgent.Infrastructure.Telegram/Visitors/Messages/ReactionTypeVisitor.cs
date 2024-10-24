using TdLib;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Functions.Components;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Functions.Components.Visitors;

namespace TgInstanceAgent.Infrastructure.Telegram.Visitors.Messages;

/// <summary>
/// Класс для асинхронного посещения типа реакции.
/// </summary>
public class ReactionTypeVisitor : IReactionVisitor
{
    /// <summary>
    /// Тип реакции.
    /// </summary>
    public TdApi.ReactionType? Type { get; private set; }

    /// <inheritdoc/>
    /// <summary>
    /// Асинхронное посещение реакции с эмодзи.
    /// </summary>
    public void Visit(TgInputReactionEmoji emoji)
    {
        // Присваивание нового объекта ReactionTypeEmoji свойству Type с указанным эмодзи
        Type = new TdApi.ReactionType.ReactionTypeEmoji
        {
            Emoji = emoji.Emoji
        };
    }

    /// <inheritdoc/>
    /// <summary>
    /// Асинхронное посещение реакции с пользовательским премиум эмодзи.
    /// </summary>
    public void Visit(TgInputCustomPremiumEmoji customEmoji)
    {
        // Присваивание нового объекта ReactionTypeCustomEmoji свойству Type с указанным id эмодзи
        Type = new TdApi.ReactionType.ReactionTypeCustomEmoji
        {
            CustomEmojiId = customEmoji.EmojiId
        };
    }
}