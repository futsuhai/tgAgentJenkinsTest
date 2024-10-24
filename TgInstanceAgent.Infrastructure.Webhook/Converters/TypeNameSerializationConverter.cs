using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using TgInstanceAgent.Application.Abstractions.Interfaces;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Events;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Objects.Chats;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Objects.Components;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Objects.Messages;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Objects.Stories;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Objects.Users;

namespace TgInstanceAgent.Infrastructure.Webhook.Converters;

/// <summary>
/// Конвертер для оборачивания объектов в имена их типов
/// </summary>
public class TypeNameSerializationConverter : JsonConverter
{
    /// <summary>
    /// Список типов, которые должны быть обработаны конвертером
    /// </summary>
    private readonly Type[] _types =
    [
        typeof(TgReactionType),
        typeof(TgChatAvailableReactions),
        typeof(TgChatType),
        typeof(TgBackgroundFill),
        typeof(TgBackgroundType),
        typeof(TgMessageSender),
        typeof(TgMessageContent),
        typeof(TgMessageOrigin),
        typeof(TgMessageReplyTo),
        typeof(TgMessageSchedulingState),
        typeof(TgStoryContent),
        typeof(TgUserStatus),
        typeof(TgEvent),
        typeof(IWithCommandId),
    ];

    /// <summary>
    /// Переопределение метода CanConvert для определения возможности конвертации указанного типа
    /// </summary>
    /// <param name="objectType">Тип объекта</param>
    /// <returns>Истинно, если возможна конвертация</returns>
    public override bool CanConvert(Type objectType) => _types.Any(t => t.IsAssignableFrom(objectType));

    /// <summary>
    /// Переопределение метода WriteJson для записи JSON-представления объекта.
    /// </summary>
    /// <param name="writer">Писатель JSON.</param>
    /// <param name="value">Объект для сериализации.</param>
    /// <param name="serializer">Сериализатор JSON.</param>
    public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
    {
        // Проверка на наличие значения
        ArgumentNullException.ThrowIfNull(value);

        // Получаем тип объекта
        var type = value.GetType();

        // Начинаем запись объекта
        writer.WriteStartObject();

        // Получаем ContractResolver и пытаемся преобразовать его к DefaultContractResolver
        var resolver = serializer.ContractResolver as DefaultContractResolver;

        // Получаем имя объекта с помощью ContractResolver (убираем первые две буквы, то есть Wa)
        var name = resolver?.GetResolvedPropertyName(type.Name[2..]) ?? type.Name[2..];

        // Пишем имя типа как свойство
        writer.WritePropertyName(name);

        // Сериализуем объект
        writer.WriteStartObject();

        // Перебираем все свойства типа
        foreach (var prop in type.GetProperties())
        {
            // Получаем значение свойства
            var propValue = prop.GetValue(value);

            // Если значение свойства null и свойства с null игнорируются настройками - не продолжаем итерацию
            if (propValue == null && serializer.NullValueHandling == NullValueHandling.Ignore) continue;

            // Получаем имя объекта с помощью ContractResolver
            name = resolver?.GetResolvedPropertyName(prop.Name) ?? prop.Name;

            // Пишем имя свойства
            writer.WritePropertyName(name);

            // Пишем значение свойства
            serializer.Serialize(writer, prop.GetValue(value));
        }

        // Заканчиваем запись объекта
        writer.WriteEndObject();

        // Заканчиваем запись объекта
        writer.WriteEndObject();
    }

    /// <summary>
    /// Переопределение метода ReadJson для чтения JSON-представления объекта (не реализовано).
    /// </summary>
    /// <param name="reader">Читатель JSON.</param>
    /// <param name="objectType">Тип объекта для десериализации.</param>
    /// <param name="existingValue">Существующее значение объекта.</param>
    /// <param name="serializer">Сериализатор JSON.</param>
    /// <returns>Результат десериализации объекта.</returns>
    public override object ReadJson(JsonReader reader, Type objectType, object? existingValue,
        JsonSerializer serializer)
    {
        // Проверка на наличие значения existingValue, если значение равно null, выбрасывается исключение ArgumentNullException
        ArgumentNullException.ThrowIfNull(existingValue);

        // Если требуется десериализация, реализуйте ее здесь
        throw new NotImplementedException("Unnecessary because CanRead is false. The type will skip the converter.");
    }

    /// <summary>
    /// Указание возможности десериализации.
    /// </summary>
    public override bool CanRead => false; // Установите в true, если необходима десериализация
}