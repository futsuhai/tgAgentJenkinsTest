using TgInstanceAgent.Application.Abstractions.TelegramClient.Objects.Components;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Objects.Interfaces;

namespace TgInstanceAgent.Application.Abstractions.TelegramClient.Objects.Messages;

/// <summary>
/// Данные сообщения типа голосовое сообщение.
/// </summary>
public class TgVoiceNoteMessage : TgMessageContent, ITgObjectWithFile
{
    /// <summary>
    /// Голосовое сообщение.
    /// </summary>
    public required TgVoiceNote VoiceNote { get; init; }

    /// <summary>
    /// Подпись к файлу.
    /// </summary>
    public TgFormatedText? Caption { get; init; }

    /// <summary>
    /// Метод возвращает файл сообщения
    /// </summary>
    /// <returns>Файл прикрепленный к сообщению</returns>
    public TgFile GetFile() => VoiceNote.File;
}