using TgInstanceAgent.Application.Abstractions.TelegramClient.Objects.Components;

namespace TgInstanceAgent.Application.Abstractions.TelegramClient.Objects.Messages;

/// <summary>
/// Данные сообщения типа текст.
/// </summary>
public class TgTextMessage : TgMessageContent
{
    /// <summary>
    /// Текст сообщения.
    /// </summary>
    public required TgFormatedText Text { get; init; }
}