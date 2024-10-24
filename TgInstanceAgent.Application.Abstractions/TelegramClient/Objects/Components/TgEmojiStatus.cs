namespace TgInstanceAgent.Application.Abstractions.TelegramClient.Objects.Components;

/// <summary>
/// Describes a custom emoji to be shown instead of the Telegram Premium badge
/// </summary>
public class TgEmojiStatus
{
    /// <summary>
    /// Identifier of the custom emoji in stickerFormatTgs format
    /// </summary>
    public required long CustomEmojiId { get; init; }

    /// <summary>
    /// Point in time (Unix timestamp) when the status will expire
    /// </summary>
    public required DateTime? ExpirationDate { get; init; }
}