using TgInstanceAgent.Application.Abstractions.TelegramClient.Objects.Components;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Objects.Interfaces;

namespace TgInstanceAgent.Application.Abstractions.TelegramClient.Objects.Messages;

/// <summary>
/// Данные сообщения типа стикер.
/// </summary>
public class TgStickerMessage : TgMessageContent, ITgObjectWithFile, ITgObjectWithThumbnail
{
    /// <summary>
    /// Стикер.
    /// </summary>
    public required TgSticker Sticker { get; init; }

    /// <summary>
    /// Флаг, является ли стикер премиум
    /// </summary>
    public required bool IsPremium { get; init; }
    
    /// <summary>
    /// Метод возвращает файл сообщения
    /// </summary>
    /// <returns>Файл прикрепленный к сообщению</returns>
    public TgFile GetFile() => Sticker.File;
    
    /// <summary>
    /// Метод возвращает превью сообщения
    /// </summary>
    /// <returns>Файл превью сообщения</returns>
    public TgThumbnail? GetThumbnail() => Sticker.Thumbnail;
}