namespace TgInstanceAgent.Application.Abstractions.TelegramClient.Exceptions;

/// <summary>
/// Представляет исключение, которое вызывается, когда запрашиваемый файл не загружен локально
/// </summary>
public class FileNotDownloadedException(double progress)
    : Exception($"The inputFile you are trying to retrieve has not been downloaded. Progress: {progress}")
{
    /// <summary>
    /// Прогресс загрузки файла
    /// </summary>
    public double Progress { get; } = progress;
}