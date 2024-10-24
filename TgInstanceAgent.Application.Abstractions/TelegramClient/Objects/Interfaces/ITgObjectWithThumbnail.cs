using TgInstanceAgent.Application.Abstractions.TelegramClient.Objects.Components;

namespace TgInstanceAgent.Application.Abstractions.TelegramClient.Objects.Interfaces;

/// <summary>
/// Интерфейс, представляющий сообщение с превью
/// </summary>
public interface ITgObjectWithThumbnail
{
    /// <summary>
    /// Метод возвращает превью сообщения
    /// </summary>
    /// <returns>Файл превью сообщения</returns>
    public TgThumbnail? GetThumbnail();
}