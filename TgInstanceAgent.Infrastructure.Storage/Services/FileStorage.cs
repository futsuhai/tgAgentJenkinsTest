using TgInstanceAgent.Application.Abstractions.FileStorage;

namespace TgInstanceAgent.Infrastructure.Storage.Services;

/// <inheritdoc/>
/// <summary>
/// Реализация хранилища файлов
/// </summary>
public class FileStorage(string path) : IFileStorage
{
    /// <summary>
    /// Сохранение файла из потока асинхронно.
    /// </summary>
    public async Task<string> StoreAsync(Stream fileStream, string directory, string fileName)
    {
        // Получаем полный путь к директории
        var fullDirectory = Path.Combine(path, directory);

        // Проверяем наличие директории, если нет — создаем
        if (!Directory.Exists(fullDirectory)) Directory.CreateDirectory(fullDirectory);

        // Сохраняем исходное имя файла без номера итерации и расширения
        var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(fileName);
        
        // Сохраняем расширение файла
        var extension = Path.GetExtension(fileName);
        
        // Определяем путь к файлу
        var filePath = Path.Combine(fullDirectory, fileName);
        
        var counter = 1;
        
        // Пока файл существует
        while (File.Exists(filePath))
        {
            // Если такой файл уже существует, то добавляем номер итерации
            fileName = $"{fileNameWithoutExtension}({counter}){extension}";
            
            // Определяем путь к файлу
            filePath = Path.Combine(fullDirectory, fileName);
            
            // Увеличиваем счетчик
            counter++;
        }

        // Открываем файл для записи и копируем данные из потока
        await using var fileStreamToWrite = new FileStream(filePath, FileMode.Create, FileAccess.Write);
        
        // Сохраняем файл
        await fileStream.CopyToAsync(fileStreamToWrite);

        // Возвращаем полный путь к файлу
        return filePath;
    }

    /// <inheritdoc/>
    /// <summary>
    /// Удаление файла асинхронно.
    /// </summary>
    public Task DeleteAsync(string pathToFile)
    {
        // Удаляем файл
        File.Delete(pathToFile);

        // Возвращаем завершенный таск
        return Task.CompletedTask;
    }

    /// <inheritdoc/>
    /// <summary>
    /// Открывает файл на чтение асинхронно.
    /// </summary>
    public Task<Stream> OpenAsync(string pathToFile)
    {
        // Открываем файл на чтение
        var file = File.OpenRead(pathToFile);

        // Возвращаем поток с файлом
        return Task.FromResult<Stream>(file);
    }
}