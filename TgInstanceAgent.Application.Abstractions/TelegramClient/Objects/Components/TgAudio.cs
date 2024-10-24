namespace TgInstanceAgent.Application.Abstractions.TelegramClient.Objects.Components;

/// <summary>
/// Представляет аудио.
/// </summary>
public class TgAudio
{
    /// <summary>
    /// Файл
    /// </summary>
    public required TgFile File { get; init; }

    /// <summary>
    /// Превью
    /// </summary>
    public TgMiniThumbnail? AlbumCoverMiniThumbnail { get; init; }

    /// <summary>
    /// Название аудио.
    /// </summary>
    public string? Title { get; init; }

    /// <summary>
    /// Исполнитель аудио.
    /// </summary>
    public string? Performer { get; init; }

    /// <summary>
    /// Длительность аудио
    /// </summary>
    public required int Duration { get; init; }

    /// <summary>
    /// Миниатюра обложки альбома в формате JPEG, указанном отправителем.
    /// Миниатюра в натуральную величину должна быть извлечена из загружаемого аудиофайла; может иметь значение null
    /// </summary>
    public TgThumbnail? AlbumCoverThumbnail { get; init; }

    /// <summary>
    /// Варианты обложек альбомов, которые можно использовать, если загруженный аудиофайл не содержит обложки альбома.
    /// Размеры предоставленных миниатюр являются приблизительными
    /// </summary>
    public TgThumbnail[]? ExternalAlbumCovers { get; init; }
}