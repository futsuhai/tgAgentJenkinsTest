namespace TgInstanceAgent.Infrastructure.Telegram.Structs;

/// <summary>
/// Класс для представления приложения Telegram.
/// </summary>
public class TelegramApp
{
    /// <summary>
    /// Идентификатор приложения.
    /// </summary>
    public long AppId { get; init; }

    /// <summary>
    /// Хэш приложения.
    /// </summary>
    public required string AppHash { get; init; }

    /// <summary>
    /// Версия приложения.
    /// </summary>
    public required string ApplicationVersion { get; init; }

    /// <summary>
    /// Местоположение файлов приложения.
    /// </summary>
    public required string FilesLocation { get; init; }
}