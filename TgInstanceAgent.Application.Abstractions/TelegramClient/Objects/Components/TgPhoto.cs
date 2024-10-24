namespace TgInstanceAgent.Application.Abstractions.TelegramClient.Objects.Components;

/// <summary>
/// Представляет фото.
/// </summary>
public class TgPhoto
{
    /// <summary>
    /// Вариации фотографии в разных размерах.
    /// </summary>
    public required TgPhotoSize[] Sizes { get; init; }

    /// <summary>
    /// Маленькое превью.
    /// </summary>
    public TgMiniThumbnail? MiniThumbnail { get; init; }
}