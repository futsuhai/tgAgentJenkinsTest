namespace TgInstanceAgent.Application.Abstractions.FileStorage;

/// <summary>
/// Интерфейс файлового хранилища.
/// </summary>
public interface IFileStorage
{
    /// <summary>
    /// Сохранение файла из потока асинхронно.
    /// </summary>
    /// <param name="fileStream">Поток файла</param>
    /// <param name="directory">Директория</param>
    /// <param name="fileName">Имя файла</param>
    /// <returns>Полный путь к файлу</returns>
    Task<string> StoreAsync(Stream fileStream, string directory, string fileName);

    /// <summary>
    /// Удаление файла асинхронно.
    /// </summary>
    /// <param name="pathToFile">Путь к файлу</param>
    Task DeleteAsync(string pathToFile);

    /// <summary>
    /// Открывает файл на чтение асинхронно.
    /// </summary>
    /// <param name="pathToFile">Путь к файлу</param>
    Task<Stream> OpenAsync(string pathToFile);
}