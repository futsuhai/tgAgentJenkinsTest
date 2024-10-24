using TgInstanceAgent.Application.Abstractions.TelegramClient.Objects.Components;

namespace TgInstanceAgent.Application.Abstractions.TelegramClient.Objects.Messages;

/// <summary>
/// Класс реакции, которая может быть добавлена на сообщение
/// </summary>
public class TgAvailableReaction
{
    /// <summary>
    /// Флаг, является ли реакция премиум-реакцией
    /// </summary>
    public required bool IsNeedPremium { get; init; }
    
    /// <summary>
    /// Тип реакции
    /// </summary>
    public required TgReactionType ReactionType { get; init; }
}