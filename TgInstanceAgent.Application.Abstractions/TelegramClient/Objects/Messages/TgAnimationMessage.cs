using TgInstanceAgent.Application.Abstractions.TelegramClient.Objects.Components;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Objects.Interfaces;

namespace TgInstanceAgent.Application.Abstractions.TelegramClient.Objects.Messages;

/// <summary>
/// Данные сообщения типа анимация(гиф).
/// </summary>
public class TgAnimationMessage : TgMessageContent, ITgObjectWithFile
{
    /// <summary>
    /// Анимация (Гиф).
    /// </summary>
    public required TgAnimation Animation { get; init; }
    
    /// <summary>
    /// Подпись к файлу.
    /// </summary>
    public TgFormatedText? Caption { get; init; }

    /// <summary>
    /// Метод возвращает файл сообщения
    /// </summary>
    /// <returns>Файл прикрепленный к сообщению</returns>
    public TgFile GetFile() => Animation.File;
}