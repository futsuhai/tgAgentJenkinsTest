namespace TgInstanceAgent.Application.Abstractions.Interfaces;

/// <summary>
/// Интерфейс, определяющий наличие миниатюры
/// </summary>
public interface IWithThumbnail
{
    /// <summary>
    /// Поток файла с данными миниатюры
    /// </summary>
    public ThumbnailData? Thumbnail { get; init; }
}

/// <summary>
/// Данные превью
/// </summary>
public class ThumbnailData: IDisposable, IAsyncDisposable
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