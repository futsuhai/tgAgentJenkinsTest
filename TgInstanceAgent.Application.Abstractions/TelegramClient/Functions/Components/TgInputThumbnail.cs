namespace TgInstanceAgent.Application.Abstractions.TelegramClient.Functions.Components;

/// <summary>
/// Класс, представляющий входное изображение для Telegram-бота.
/// </summary>
public class TgInputThumbnail
{
    /// <summary>
    /// Получает или устанавливает путь к изображению.
    /// </summary>
    public required string Path { get; init; }
}