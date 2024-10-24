namespace TgInstanceAgent.Application.Abstractions.TelegramClient.Objects.Components;

/// <summary>
/// Представляет видео.
/// </summary>
public class TgVideo
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
    /// Длительность видео.
    /// </summary>
    public double? Duration { get; init; }

    /// <summary>
    /// Ширина.
    /// </summary>
    public required int Width { get; init; }

    /// <summary>
    /// Высота
    /// </summary>
    public required int Height { get; init; }
}