using TgInstanceAgent.Application.Abstractions.TelegramClient.Objects.Components;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Objects.Interfaces;

namespace TgInstanceAgent.Application.Abstractions.TelegramClient.Objects.Messages;

/// <summary>
/// Данные сообщения типа документ.
/// </summary>
public class TgDocumentMessage : TgMessageContent, ITgObjectWithFile, ITgObjectWithThumbnail
{
    /// <summary>
    /// Документ.
    /// </summary>
    public required TgDocument Document { get; init; }

    /// <summary>
    /// Подпись к файлу.
    /// </summary>
    public TgFormatedText? Caption { get; init; }

    /// <summary>
    /// Метод возвращает файл сообщения
    /// </summary>
    /// <returns>Файл прикрепленный к сообщению</returns>
    public TgFile GetFile() => Document.File;
    
    /// <summary>
    /// Метод возвращает превью сообщения
    /// </summary>
    /// <returns>Файл превью сообщения</returns>
    public TgThumbnail? GetThumbnail() => Document.Thumbnail;
}