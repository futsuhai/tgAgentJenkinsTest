using TgInstanceAgent.Application.Abstractions.TelegramClient.Objects.Components;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Objects.Interfaces;

namespace TgInstanceAgent.Application.Abstractions.TelegramClient.Objects.Messages;

/// <summary>
/// Данные сообщения типа видео сообщение.
/// </summary>
public class TgVideoNoteMessage : TgMessageContent, ITgObjectWithFile, ITgObjectWithThumbnail
{
    /// <summary>
    /// Видео-сообщение.
    /// </summary>
    public required TgVideoNote VideoNote { get; init; }
    
    /// <summary>
    /// Верно, если миниатюра видеозаписи должна быть размыта и видеозапись должна отображаться только при нажатии
    /// </summary>
    public required bool IsSecret { get; init; }
    
    /// <summary>
    /// Верно, если хотя бы один из получателей просмотрел видеозапись
    /// </summary>
    public required bool IsViewed { get; init; }
    
    /// <summary>
    /// Метод возвращает файл сообщения
    /// </summary>
    /// <returns>Файл прикрепленный к сообщению</returns>
    public TgFile GetFile() => VideoNote.File;
    
    /// <summary>
    /// Метод возвращает превью сообщения
    /// </summary>
    /// <returns>Файл превью сообщения</returns>
    public TgThumbnail? GetThumbnail() => VideoNote.Thumbnail;
}