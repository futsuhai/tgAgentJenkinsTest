namespace TgInstanceAgent.Application.Abstractions.TelegramClient.Objects.Components;

/// <summary>
/// Представляет документ.
/// </summary>
public class TgDocument
{
    /// <summary>
    /// Оригинальное имя файла; как определено отправителем
    /// </summary>
    public required string FileName { get; init; }

    /// <summary>
    /// MIME тип файла; как определено отправителем
    /// </summary>
    public required string MimeType { get; init; }
    
    /// <summary>
    /// Файл.
    /// </summary>
    public required TgFile File { get; init; }

    /// <summary>
    /// Превью.
    /// </summary>
    public TgThumbnail? Thumbnail { get; init; }
    
    /// <summary>
    /// Маленькое превью.
    /// </summary>
    public TgMiniThumbnail? MiniThumbnail { get; init; }
}