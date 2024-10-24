namespace TgInstanceAgent.Application.Abstractions.TelegramClient.Objects.Components;

/// <summary>
/// Представляет размер фотографии
/// </summary>
public class TgPhotoSize
{
    /// <summary>
    /// Файл.
    /// </summary>
    public required TgFile File { get; init; }

    /// <summary>
    /// Ширина.
    /// </summary>
    public required int Width { get; init; }

    /// <summary>
    /// Высота.
    /// </summary>
    public required int Height { get; init; }
}