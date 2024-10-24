using TgInstanceAgent.Application.Abstractions.TelegramClient.Objects.Components;

namespace TgInstanceAgent.Application.Abstractions.TelegramClient.Objects.Messages;

/// <summary>
/// Класс, представляющий уже добавленную на сообщение реакцию
/// </summary>
public class TgAddedReaction
{
    /// <summary>
    /// Дата добавления реакции
    /// </summary>
    public required DateTime Date { get; init; }
    
    /// <summary>
    /// Флаг, была ли реакция добавлена текущим пользователем
    /// </summary>
    public required bool IsOutgoing { get; init; }
    
    /// <summary>
    /// Идентификатор отправителя реакции
    /// </summary>
    public required TgMessageSender SenderId { get; init; }
    
    /// <summary>
    /// Тип реакции
    /// </summary>
    public required TgReactionType Type { get; init; }
}