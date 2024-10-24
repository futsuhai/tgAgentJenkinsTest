namespace TgInstanceAgent.Application.Abstractions.TelegramClient.Objects.Components;

/// <summary>
/// Представляет видео сообщение.
/// </summary>
public class TgVideoNote
{
    /// <summary>
    /// Файл
    /// </summary>
    public required TgFile File { get; init; }

    /// <summary>
    /// Превью
    /// </summary>
    public TgThumbnail? Thumbnail { get; init; }

    /// <summary>
    /// Маленькое превью
    /// </summary>
    public TgMiniThumbnail? MiniThumbnail { get; init; }

    /// <summary>
    /// Длительность видео сообщения.
    /// </summary>
    public required long Duration { get; init; }
}