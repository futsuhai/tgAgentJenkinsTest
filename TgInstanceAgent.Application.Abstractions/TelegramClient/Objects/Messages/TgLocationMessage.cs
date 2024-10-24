using TgInstanceAgent.Application.Abstractions.TelegramClient.Objects.Components;

namespace TgInstanceAgent.Application.Abstractions.TelegramClient.Objects.Messages;

/// <summary>
/// Данные сообщения типа местоположение.
/// </summary>
public class TgLocationMessage : TgMessageContent
{
    /// <summary>
    /// Локация
    /// </summary>
    public required TgLocation Location { get; init; }
}