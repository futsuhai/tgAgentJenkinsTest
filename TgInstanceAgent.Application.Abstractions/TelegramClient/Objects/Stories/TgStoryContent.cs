using TgInstanceAgent.Application.Abstractions.TelegramClient.Objects.Components;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Objects.Interfaces;

namespace TgInstanceAgent.Application.Abstractions.TelegramClient.Objects.Stories;

/// <summary>
/// Абстрактный контект истории.
/// </summary>
public abstract class TgStoryContent;

/// <summary>
/// Представляет историю с фотографией
/// </summary>
public class TgStoryPhoto : TgStoryContent, ITgObjectWithFile
{
    /// <summary>
    /// Фотография
    /// </summary>
    public required TgPhoto Photo { get; init; }
    
    /// <summary>
    /// Метод для получения файла из фотографии
    /// </summary>
    /// <returns>Файл</returns>
    public TgFile GetFile() => Photo.Sizes.Last().File;
}

/// <summary>
/// Представляет историю с видео
/// </summary>
public class TgStoryVideo : TgStoryContent, ITgObjectWithFile
{
    /// <summary>
    /// Видео.
    /// </summary>
    public required TgVideo Video { get; init; }

    /// <summary>
    /// Метод для получения файла из видео
    /// </summary>
    /// <returns>Файл</returns>
    public TgFile GetFile() => Video.File;
}

public class TgUnsupportedStory : TgStoryContent;