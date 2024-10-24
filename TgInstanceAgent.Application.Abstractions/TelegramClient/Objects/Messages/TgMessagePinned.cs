namespace TgInstanceAgent.Application.Abstractions.TelegramClient.Objects.Messages;

/// <summary>
/// Специальное сообщение, о том что сообщение было закреплено
/// </summary>
public class TgMessagePinned : TgMessageContent
{
    /// <summary>
    /// Идентификатор сообщения
    /// </summary>
    public required long MessageId { get; init; }
}