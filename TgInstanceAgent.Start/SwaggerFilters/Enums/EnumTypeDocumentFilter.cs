using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace TgInstanceAgent.Start.SwaggerFilters.Enums;

/// <summary>
/// Класс фильтра документа Swagger
/// </summary>
public class EnumTypeDocumentFilter : IDocumentFilter
{
/// <summary>
    /// Переопределение метода Apply для изменения текущего документа
    /// </summary>
    /// <param name="swaggerDoc">Документ</param>
    /// <param name="context">Контекст документа</param>
    public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
    {
        // Проходимся в цикле по каждому параметру каждого метода в документе
        foreach (
            var parameter in
            from path in swaggerDoc.Paths.Values
            from operation in path.Operations.Values
            from parameter in operation.Parameters
            select parameter)
        {
            // Получаем Id схемы параметра
            var schemaReferenceId = parameter.Schema.Reference?.Id;

            // Если Id пустое - возвращаемся из цикла
            if (string.IsNullOrEmpty(schemaReferenceId)) continue;

            // Получаем схему по Id
            var schema = context.SchemaRepository.Schemas[schemaReferenceId];

            // Если не удалось получить схему или в схеме нет перечислений - идем дальше
            if (schema.Enum == null || schema.Enum.Count == 0) continue;

            // Добавляем описание текущему параметру
            parameter.Description += "<p>Варианты</p>";

            if (schema.Description.Contains("<ul>"))
            {
                // Получаем индекс начала описания параметра в схеме
                var curStart = schema.Description.IndexOf("<ul>", StringComparison.Ordinal);

                // Получаем индекс конца описания параметра в схеме
                var curEnd = schema.Description.IndexOf("</ul>", StringComparison.Ordinal) + 5;

                // Добавляем описание из схемы в описание параметра
                parameter.Description += schema.Description.Substring(curStart, curEnd - curStart);
            }
            else
            {
                // Добавляем описание из схемы в описание параметра
                parameter.Description += schema.Description;
            }
        }
    }
}