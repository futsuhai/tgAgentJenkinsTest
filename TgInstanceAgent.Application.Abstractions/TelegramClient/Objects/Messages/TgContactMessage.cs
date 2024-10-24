using TgInstanceAgent.Application.Abstractions.TelegramClient.Objects.Components;

namespace TgInstanceAgent.Application.Abstractions.TelegramClient.Objects.Messages;

/// <summary>
/// Данные сообщения типа контакт.
/// </summary>
public class TgContactMessage : TgMessageContent
{
    /// <summary>
    /// Контакт.
    /// </summary>
    public required TgContact Contact { get; init; }
}