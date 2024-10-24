using TgInstanceAgent.Application.Abstractions.TelegramClient.Objects.Components;

namespace TgInstanceAgent.Application.Abstractions.TelegramClient.Objects.Interfaces;

/// <summary>
/// Интерфейс, представляющий сообщение с файлом
/// </summary>
public interface ITgObjectWithFile
{
    /// <summary>
    /// Метод возвращает файл сообщения
    /// </summary>
    /// <returns>Файл прикрепленный к сообщению</returns>
    public TgFile GetFile();
}