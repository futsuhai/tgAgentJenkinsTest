using TgInstanceAgent.Application.Abstractions.TelegramClient.Objects.Components;

namespace TgInstanceAgent.Application.Abstractions.TelegramClient.Objects.Messages;

/// <summary>
/// Информация о реакции на сообщение.
/// </summary>
public class TgMessageReaction
{
    /// <summary>
    /// Общее количество реакций.
    /// </summary>
    public required int TotalCount { get; init; }
    
    /// <summary>
    /// Выбрана ли реакция пользователем.
    /// </summary>
    public bool IsChosen { get; init; }
    
    /// <summary>
    /// Тип реакции.
    /// </summary>
    public required TgReactionType Type { get; init; }
    
    /// <summary>
    /// Идентификатор пользователя, который использовал реакцию.
    /// </summary>
    public required TgMessageSender UsedSenderId { get; init; }
    
    /// <summary>
    /// Массив пользователей, недавно использовавших реакцию.
    /// </summary>
    public TgMessageSender[]? RecentSenderIds { get; init; }
}