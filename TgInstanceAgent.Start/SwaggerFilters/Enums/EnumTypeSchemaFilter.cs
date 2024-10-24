using System.Xml.Linq;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace TgInstanceAgent.Start.SwaggerFilters.Enums;

/// <summary>
/// Класс фильтра схемы xml документа Swagger
/// </summary>
public class EnumTypeSchemaFilter : ISchemaFilter
{
  /// <summary>
    /// XML документ
    /// </summary>
    private readonly XDocument? _xmlComments;

    /// <summary>
    /// Конструктор
    /// </summary>
    public EnumTypeSchemaFilter(string xmlPath)
    {
        if (File.Exists(xmlPath))
        {
            _xmlComments = XDocument.Load(xmlPath);
        }
    }
    
    /// <summary>
    /// Переопределение метода Apply для изменения текущей схемы документа
    /// </summary>
    /// <param name="schema">Схема документа</param>
    /// <param name="context">Контекст</param>
    public void Apply(OpenApiSchema schema, SchemaFilterContext context)
    {
        // Если нет документа, возвращаемся из метода
        if (_xmlComments == null) return;

        // Если в схеме и контексте нет перечислений
        if (schema.Enum is not { Count: > 0 }
            || context.Type is not { IsEnum: true }) return;
        
        // Добавляем описание для перечисления
        schema.Description += "<p>Элементы:</p><ul>";
        
        // Получаем полное имя перечисления
        var fullTypeName = context.Type.FullName;

        // Проходим по каждому члену перечисления
        foreach (var enumMemberType in schema.Enum.OfType<OpenApiString>().Select(v => v.Value))
        {
            // Получаем полное имя члена перечисления
            var fullEnumMemberName = $"F:{fullTypeName}.{enumMemberType}";

            // Получаем комментарии из XML документа по текущему члену перечисления
            var enumMemberComments = _xmlComments.Descendants("member")
                .FirstOrDefault(m =>
                    m.Attribute("name")!.Value.Equals(fullEnumMemberName, StringComparison.OrdinalIgnoreCase));

            // Берем описание из комментария
            var summary = enumMemberComments?.Descendants("summary").FirstOrDefault();
                
            // Если описания нет - идем дальше
            if (summary == null) continue;

            // Добавляем описание для члена перечисления
            schema.Description += $"<li><i>{enumMemberType}</i> - {summary.Value.Trim()}</li>";
        }
        
        // Добавляем закрывающий тег списка к описанию
        schema.Description += "</ul>";
    }
}