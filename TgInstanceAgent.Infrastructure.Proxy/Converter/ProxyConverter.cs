using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using TgInstanceAgent.Application.Abstractions.Proxies.Exceptions;
using TgInstanceAgent.Infrastructure.Proxy.Objects;
using Response = MassTransit.Response;

namespace TgInstanceAgent.Infrastructure.Proxy.Converter;

/// <summary>
/// Конвертер для преобразования объектов JSON в объекты моделей WhatsApp
/// </summary>
public class ProxyConverter : JsonConverter
{
    /// <inheritdoc />
    public override object? ReadJson(JsonReader reader, Type objectType, object? existingValue,
        JsonSerializer serializer)
    {
        // Загружаем JSON-объект из читателя
        var jToken = JToken.Load(reader);

        // Проверяем, является ли объект объектом 
        if (jToken.Type != JTokenType.Object) return null;

        // Преобразуем объект к JObject
        var jObject = (JObject)jToken;

        // Получаем тип объекта из свойства "status"
        var statusProp = jObject["status"];

        // Если тип объекта null, возвращаем null
        if (statusProp == null) return null;

        // Преобразуем обьект в строку
        var status = (string)statusProp!;

        // Если статус "yes" возвращаем результат
        if (status == "yes") return jObject.ToObject(objectType);

        if (jObject.ToObject(typeof(ErrorResponse)) is not ErrorResponse x)
            throw new ProxyClientException(500, "Unknown response");
        
        // Выбираем исключение по коду ошибки
        throw x.ErrorId switch
        {
            // Ошибка кол-ва прокси
            200 => new ProxyClientCountException(x.ErrorId, x.Error),
            
            // Ошибка страны
            220 => new ProxyClientCountryException(x.ErrorId, x.Error),
            
            // Ошибка списка номеров прокси
            230 => new ProxyClientIdsException(x.ErrorId, x.Error),
            
            // Ошибка баланса
            400 => new ProxyClientNoMoneyException(x.ErrorId, x.Error),
            
            // Ошибка периода
            210 =>  new ProxyClientPeriodException(x.ErrorId, x.Error),
            
            // Если не один код ошибки не подходит, выбрасываем базовое исключение
            _ => new ProxyClientException(x.ErrorId, x.Error)
        };
    }
    
    /// <inheritdoc />
    public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public override bool CanConvert(Type objectType)
    {
        // Проверяем, является ли объект наследником класса WaObject.
        return typeof(Response).IsAssignableFrom(objectType);
    }
}