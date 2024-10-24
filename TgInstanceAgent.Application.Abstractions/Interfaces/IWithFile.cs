namespace TgInstanceAgent.Application.Abstractions.Interfaces;

/// <summary>
/// Интерфейс, определяющий наличие файла для его отправки.
/// </summary>
public interface IWithFile
{
    /// <summary>
    /// Файл.
    /// </summary>
    public FileData? File { get; init; }

    /// <summary>
    /// Локальный идентификатор файла.
    /// </summary>
    public int? LocalId { get; init; }

    /// <summary>
    /// Удаленный идентификатор файла.
    /// </summary>
    public string? RemoteId { get; init; }
}

/// <summary>
/// Данные файла
/// </summary>
public class FileData : IDisposable, IAsyncDisposable
{
    /// <summary>
    /// Поток файла
    /// </summary>
    public required Stream Stream { get; init; }

    /// <summary>
    /// Название файла
    /// </summary>
    public required string Name { get; init; }

    /// <summary>
    /// Уничтожает объект.
    /// </summary>
    public void Dispose()
    {
        // Предотвращаем вызов деструктора
        GC.SuppressFinalize(this);

        // Уничтожаем поток
        Stream.Dispose();
    }

    /// <summary>
    /// Уничтожает объект асинхронно.
    /// </summary>
    public async ValueTask DisposeAsync()
    {
        // Предотвращаем вызов деструктора
        GC.SuppressFinalize(this);
        
        // Уничтожаем поток
        await Stream.DisposeAsync();
    }
}