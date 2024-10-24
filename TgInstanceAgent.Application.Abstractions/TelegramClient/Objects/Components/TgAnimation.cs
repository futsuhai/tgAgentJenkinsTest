namespace TgInstanceAgent.Application.Abstractions.TelegramClient.Objects.Components;

/// <summary>
/// Представляет анимацию (гиф).
/// </summary>
public class TgAnimation
{
    /// <summary>
    /// Файл, содержащий анимацию.
    /// </summary>
    public required TgFile File { get; init; }
    
    /// <summary>
    /// Миниатюра изображения файла.
    /// </summary>
    public TgThumbnail? Thumbnail { get; init; }
    
    /// <summary>
    /// Высоат анимации
    /// </summary>
    public int Height { get; init; }
    
    /// <summary>
    /// Ширина анимации
    /// </summary>
    public int Width { get; init; }
    
    /// <summary>
    /// Истинно, если превью анимации должно быть закрыто.
    /// </summary>
    public required bool HasSpoiler { get; init; }
}