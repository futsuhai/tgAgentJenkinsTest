namespace TgInstanceAgent.Application.Abstractions.TelegramClient.Functions.Components.Interfaces;

/// <summary>
/// Интерфейс, представляющий файл.
/// </summary>
public interface ITgInputDataWithFile
{
    /// <summary>
    /// Файл
    /// </summary>
    public TgInputFileData File { get; init; }
}