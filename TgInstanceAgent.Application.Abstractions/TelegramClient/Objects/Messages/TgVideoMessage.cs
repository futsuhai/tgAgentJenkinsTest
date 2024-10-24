using TgInstanceAgent.Application.Abstractions.TelegramClient.Objects.Components;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Objects.Interfaces;

namespace TgInstanceAgent.Application.Abstractions.TelegramClient.Objects.Messages;

/// <summary>
/// Данные сообщения типа видео.
/// </summary>
public class TgVideoMessage : TgMessageContent, ITgObjectWithFile, ITgObjectWithThumbnail
{
    /// <summary>
    /// Видео.
    /// </summary>
    public required TgVideo Video { get; init; }

    /// <summary>
    /// Подпись к файлу.
    /// </summary>
    public TgFormatedText? Caption { get; init; }
    
    /// <summary>
    /// Метод возвращает файл сообщения
    /// </summary>
    /// <returns>Файл прикрепленный к сообщению</returns>
    public TgFile GetFile() => Video.File;
    
    /// <summary>
    /// Метод возвращает превью сообщения
    /// </summary>
    /// <returns>Файл превью сообщения</returns>
    public TgThumbnail? GetThumbnail() => Video.Thumbnail;
}