using Microsoft.OpenApi.Models;

namespace TgInstanceAgent.Start.SwaggerFilters.Localization;

/// <summary>
/// Вспомогательный класс для создания .csv документа со всеми имеющимися xml комментариями
/// </summary>
public static class ResourceHelper
{
    // Префикс методов 
    private const string PathPrefix = "/apiTg/";

    // Суффикс методов 
    private const string PathSuffix = "/{instanceId}";

    // Путь к файлу .csv
    private const string FilePath = "./Resources/res.csv";


    // Словарь имеющихся ключей в файле
    private static readonly Dictionary<string, bool> ExistedKeys = new();

    /// <summary>
    /// Метод, добавляет заголовочную строку в файл .csv
    /// </summary>
    private static StreamWriter InitFile()
    {
        var sw = new StreamWriter(File.Open(FilePath, FileMode.Create));

        // Пишем заголовки в файл
        sw.WriteLine(
            "Path,Name,Default Culture,az,be,bg,cs,de,el,en,es,fi,fr,hu,it,ja,kk,ko,nl,pl,pt,ro,ru,sr,sv,tr,uk,zh");

        return sw;
    }

    /// <summary>
    /// Метод, добавляет отформатированную для файла локализации строку в файл .csv
    /// </summary>
    /// <param name="key">Ключ</param>
    /// <param name="value">Значение</param>
    /// <param name="sw">Поток для записи</param>
    private static void WriteFormatStringToFile(string key, string? value, TextWriter sw)
    {
        if (value == null) return;

        // Если такой ключ уже есть - не добавляем его
        if (!ExistedKeys.TryAdd(key, true)) return;

        // Если значение содержит запятые
        if (value.Contains(','))
        {
            // Оборачиваем значение в ковычки 
            value = $"\"{value}\"";
        }

        // Значение для столбца Path .resx документа
        const string innerPath = "UserAgent.Start/Resources/SwaggerFilters/Localization/LocalizationDocumentFilter";

        // Добавляем форматированную строку в файл
        sw.WriteLine($"{innerPath},{key},,,,,,,,,,,,,,,,,,,,,{value},,,,,");
    }

    /// <summary>
    /// Метод создания .csv файла
    /// </summary>
    /// <param name="document">XML документ</param>
    public static void CreateModelResource(OpenApiDocument document)
    {
        // Добвляем заголовки в файл
        using var sw = InitFile();

        // Проходимся по каждому пути (методу) в комментариях
        foreach (var path in document.Paths)
        {
            // Получаем имя метода для локализации
            var localizedName = path.Key
                .Replace(PathPrefix, string.Empty)
                .Replace(PathSuffix, string.Empty)
                .Replace("/", string.Empty);

            // Проходимся по операциям в xml файле
            foreach (var operation in path.Value.Operations)
            {
                // Пишем в файл описание метода
                WriteFormatStringToFile(localizedName, operation.Value.Summary, sw);

                // Проходимся по каждому Query параметру метода
                foreach (var parameter in operation.Value.Parameters)
                {
                    // Составляем имя локализованного комментария
                    var parameterLocalizedName = "Parameter" + parameter.Name;

                    // Пишем в файл описание параметра
                    WriteFormatStringToFile(parameterLocalizedName, parameter.Description, sw);
                }

                // Если у метода есть request body (модель)
                if (operation.Value.RequestBody != null)
                {
                    // Получаем ключ описания модели
                    var requestBodyLocalizeValue = localizedName + "Request";

                    // Пишем в файл описание модели
                    WriteFormatStringToFile(requestBodyLocalizeValue, operation.Value.RequestBody.Description, sw);
                }

                // Проходимся по всем возможным ответам от сервера
                foreach (var (key, value) in operation.Value.Responses)
                {
                    //Пишем в файл описание статуса
                    WriteFormatStringToFile("Response" + key, value.Description, sw);
                }
            }
        }

        // Проходимся по всем схемам (моделям) в xml-документе
        foreach (var (key, value) in document.Components.Schemas)
        {
            // Пишем в файл описание модели
            WriteFormatStringToFile(key, value.Description, sw);

            // Проходимся по каждому свойству модели
            foreach (var (propKey, propValue) in value.Properties)
            {
                // Если у свойства есть описание
                if (propValue.Description != null)
                {
                    // Пишем в файл описание свойства
                    WriteFormatStringToFile("Parameter" + propKey, propValue.Description, sw);
                }
            }
        }
    }
}