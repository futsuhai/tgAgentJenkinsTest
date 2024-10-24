using TgInstanceAgent.Application.Abstractions.Commands.TgChats;
using TgInstanceAgent.Application.Abstractions.Interfaces;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Exceptions;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Functions.Components;

namespace TgInstanceAgent.Application.Services.Extensions;

/// <summary>
/// Класс-расширение для преобразования реакции из команды в формат Telegram API.
/// </summary>
public static class ReactionMapper
{
    /// <summary>
    /// Получить реакцию в формате Telegram API из команды с реакцией.
    /// </summary>
    /// <param name="command">Команда с реакцией.</param>
    /// <returns>Реакция в формате Telegram API.</returns>
    public static TgInputReaction GetReaction(this IWithReaction command)
    {
        // Если эмодзи не пустой
        if (!string.IsNullOrEmpty(command.Emoji))
        {
            // Вернуть реакцию с эмодзи
            return new TgInputReactionEmoji
            {
                // Установить эмодзи
                Emoji = command.Emoji
            };
        }

        // Если идентификатор эмодзи не null
        if (command.EmojiId.HasValue)
        {
            // Вернуть реакцию с настраиваемым эмодзи
            return new TgInputCustomPremiumEmoji
            {
                // Установить идентификатор эмодзи
                EmojiId = command.EmojiId.Value
            };
        }

        // Если не удалось определить реакцию, выбросить исключение
        throw new InvalidReactionRequestException();
    }
    
    /// <summary>
    /// Получить массив реакций в формате Telegram API из команды с реакциями.
    /// </summary>
    /// <param name="command">Команда с реакциями.</param>
    /// <returns>Массив реакций в формате Telegram API.</returns>
    public static TgInputReaction[] GetReactions(this SetChatAvailableReactionsCommand command)
    {
        var reactions = new List<TgInputReaction>();

        // Добавляем обычные эмодзи, если они есть
        if (command.Emojis is { Length: > 0 })
        {
            reactions.AddRange(command.Emojis.Select(emoji => new TgInputReactionEmoji
            {
                Emoji = emoji
            }));
        }

        // Добавляем кастомные эмодзи, если они есть
        if (command.EmojiIds is { Length: > 0 })
        {
            reactions.AddRange(command.EmojiIds.Select(emojiId => new TgInputCustomPremiumEmoji
            {
                EmojiId = emojiId
            }));
        }

        // Возвращаем результат в виде массива
        return reactions.ToArray();
    }

}