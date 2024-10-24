using Microsoft.Extensions.Localization;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace TgInstanceAgent.Start.SwaggerFilters.Localization;

/// <summary>
/// Класс фильтра документа Swagger для локализации
/// </summary>
/// <param name="localizer">Локализатор</param>
public class LocalizationDocumentFilter(IStringLocalizer<LocalizationDocumentFilter> localizer) : IDocumentFilter
{
    // Префикс методов контроллеров
    private const string PathPrefix = "/apiTg/";
    
    // Суффикс методов контроллеров
    private const string PathSuffix = "/{instanceId}";
    
    /// <summary>
    /// Переопределение метода Apply для изменения текущего документа в соответствии с локализацией
    /// </summary>
    /// <param name="swaggerDoc">Документ</param>
    /// <param name="context">Контекст документа</param>
    public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
    {
        // Создание .csv файла в папке Resources со всеми xml-комментариями для последующего парсинга в .resx 
        ResourceHelper.CreateModelResource(swaggerDoc);

        // Проходимся по каждому пути (методу) в комментариях
        foreach (var path in swaggerDoc.Paths)
        {
            // Получаем имя метода для локализации
            var localizedName = path.Key.Replace(PathPrefix, string.Empty)
                .Replace(PathSuffix, string.Empty).Replace("/", string.Empty);

            // Проходимся по операциям в xml файле
            foreach (var operation in path.Value.Operations)
            {
                // Меняем описание метода в соответствии с локализацией
                operation.Value.Summary = localizer[localizedName];
                
                // Проходимся по каждому Query параметру метода
                foreach (var parameter in operation.Value.Parameters)
                {
                    // Составляем имя локализованного комментария
                    var parameterLocalizedName = "Parameter" + parameter.Name;
                    
                    // Меняем описание параметра на локализованное
                    parameter.Description = localizer[parameterLocalizedName];
                }

                // Если у метода есть request body (модель)
                if (operation.Value.RequestBody != null)
                {
                    // Получаем описание модели
                    var requestBodyLocalizeValue = localizedName + "Request";
                    
                    // Меняем описание модели на локализованное
                    operation.Value.RequestBody.Description = localizer[requestBodyLocalizeValue];
                }

                // Проходимся по всем возможным ответам от сервера
                foreach (var (key, value) in operation.Value.Responses)
                {
                    //Меняем описание статуса на локализованное
                    value.Description = localizer["Response" + key];
                }
            }
        }

        // Проходимся по всем схемам (моделям) в xml-документе
        foreach (var (key, value) in swaggerDoc.Components.Schemas)
        {
            // Меняем описание модели на локализованное
            value.Description = localizer[key];

            // Проходимся по каждому свойству модели
            foreach (var (propKey, propValue) in value.Properties)
            {
                // Если у свойства отсутствует описание - идем дальше
                if (propValue.Description == null) continue;
                
                // Получаем имя свойства с заглавной буквы 
                var upperPropKey = char.ToUpper(propKey[0]) + propKey[1..];
                
                // Получаем описание свойства из локализатора
                var description = localizer["Parameter" + upperPropKey];

                // Если не нашли описание в локализаторе  
                propValue.Description = description.Value.Equals("Parameter" + upperPropKey)
                
                    // Ищем описание без заглавной буквы 
                    ? localizer["Parameter" + propKey]
                    
                    // Иначе меняем описание на найденное
                    : description;
            }
        }
    }
}