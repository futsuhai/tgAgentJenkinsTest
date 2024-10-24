using TgInstanceAgent.Application.Abstractions.TelegramClient.Objects.Components;

namespace TgInstanceAgent.Application.Abstractions.TelegramClient.Objects.Chats;

/// <summary>
/// Базовый класс для информации о доступных реакциях в чате
/// </summary>
public abstract class TgChatAvailableReactions
{
    /// <summary>
    /// Максимально допустимое количество реакций на одно сообщение; от 1 до 11
    /// </summary>
    public required int MaxReactionCount { get; init; }
}

/// <summary>
/// Информация о доступных реакциях в чате (некоторые реакции)
/// </summary>
public class TgChatAvailableReactionsSome : TgChatAvailableReactions
{
    /// <summary>
    /// Список доступных реакций
    /// </summary>
    public required TgReactionType[] Reactions { get; init; }
}

/// <summary>
/// Информация о доступных реакциях в чате (все реакции)
/// </summary>
public class TgChatAvailableReactionsAll : TgChatAvailableReactions;