using TgInstanceAgent.Application.Abstractions.TelegramClient.Objects.Components;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Objects.Interfaces;

namespace TgInstanceAgent.Application.Abstractions.TelegramClient.Objects.Messages;

/// <summary>
/// Данные сообщения типа фото.
/// </summary>
public class TgPhotoMessage : TgMessageContent, ITgObjectWithFile
{
    /// <summary>
    /// Фото.
    /// </summary>
    public required TgPhoto Photo { get; init; }
    
    /// <summary>
    /// Подпись к файлу.
    /// </summary>
    public TgFormatedText? Caption { get; init; }
    
    /// <summary>
    /// Метод возвращает файл сообщения
    /// </summary>
    /// <returns>Файл прикрепленный к сообщению</returns>
    public TgFile GetFile() => Photo.Sizes.Last().File;
}