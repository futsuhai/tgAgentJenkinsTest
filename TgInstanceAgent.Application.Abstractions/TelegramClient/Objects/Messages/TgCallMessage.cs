using TgInstanceAgent.Application.Abstractions.TelegramClient.Objects.Enums;

namespace TgInstanceAgent.Application.Abstractions.TelegramClient.Objects.Messages;

/// <summary>
/// Данные сообщения тип звнока
/// </summary>
public class TgCallMessage : TgMessageContent
{
    /// <summary>
    /// Причина завершения звонка
    /// </summary>
    public required TgCallDiscardReason? DiscardReason { get; init; }
    
    /// <summary>
    /// Истинно, если был видеозвонок
    /// </summary>
    public required bool IsVideo { get; init; }
    
    /// <summary>
    /// Длительность звонка в минутах
    /// </summary>
    public required int Duration { get; init; }
}