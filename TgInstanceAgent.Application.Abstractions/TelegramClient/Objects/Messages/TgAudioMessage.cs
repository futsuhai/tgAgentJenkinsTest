using TgInstanceAgent.Application.Abstractions.TelegramClient.Objects.Components;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Objects.Interfaces;

namespace TgInstanceAgent.Application.Abstractions.TelegramClient.Objects.Messages;

/// <summary>
/// Данные сообщения типа аудио.
/// </summary>
public class TgAudioMessage : TgMessageContent, ITgObjectWithFile
{
    /// <summary>
    /// Аудио.
    /// </summary>
    public required TgAudio Audio { get; init; }

    /// <summary>
    /// Подпись к файлу.
    /// </summary>
    public TgFormatedText? Caption { get; init; }

    /// <summary>
    /// Метод возвращает файл сообщения
    /// </summary>
    /// <returns>Файл прикрепленный к сообщению</returns>
    public TgFile GetFile() => Audio.File;
}