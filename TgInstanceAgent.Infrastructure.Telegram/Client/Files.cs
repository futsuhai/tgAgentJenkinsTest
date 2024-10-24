using TdLib;

namespace TgInstanceAgent.Infrastructure.Telegram.Client;

/// <summary>
/// Класс для работы с файлами.
/// </summary>
public class Files
{
    /// <summary>
    /// Проверяет и обновляет файл в объекте.
    /// </summary>
    /// <param name="updateFile">Обновленный файл.</param>
    /// <param name="obj">Объект, содержащий файл.</param>
    /// <param name="propertyName">Имя свойства, содержащего файл.</param>
    public static void CheckAndUpdateFile(TdApi.File updateFile, object? obj, string propertyName)
    {
        // Если объект равен null, выходим из метода.
        if(obj == null) return;

        // Получаем свойство объекта по имени.
        var property = obj.GetType().GetProperty(propertyName);

        // Если свойство не найдено, выходим из метода.
        if (property == null) return;

        // Получаем текущий файл из свойства объекта.
        if (property.GetValue(obj) is not TdApi.File currentFile) return;

        // Если идентификатор текущего файла не совпадает с идентификатором обновленного файла, выходим из метода.
        if (currentFile.Id != updateFile.Id) return;

        // Устанавливаем обновленный файл в свойство объекта.
        property.SetValue(obj, updateFile);
    }
}
