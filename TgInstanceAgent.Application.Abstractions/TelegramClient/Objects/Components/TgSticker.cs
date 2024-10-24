namespace TgInstanceAgent.Application.Abstractions.TelegramClient.Objects.Components;

/// <summary>
/// Представляет стикер.
/// </summary>
public class TgSticker
{
    /// <summary>
    /// Файл
    /// </summary>
    public required TgFile File { get; init; }

    /// <summary>
    /// Идентификатор набора
    /// </summary>
    public long? SetId { get; init; }

    /// <summary>
    /// Идентификатор стикера
    /// </summary>
    public long? Id { get; init; }

    /// <summary>
    /// С какими Emoji связан
    /// </summary>
    public string? Emoji { get; init; }

    /// <summary>
    /// Превью
    /// </summary>
    public TgThumbnail? Thumbnail { get; init; }
}