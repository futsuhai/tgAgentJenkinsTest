using AutoMapper;
using TdLib;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Objects.Components;

namespace TgInstanceAgent.Infrastructure.Telegram.Mapper.Converters;

/// <summary>
/// Преобразует объект TdApi.FormattedText в TgFormatedText.
/// </summary>
public class FormatedTextConverter : ITypeConverter<TdApi.FormattedText?, TgFormatedText?>
{
    /// <summary>
    /// Преобразует объект TdApi.FormattedText в TgFormatedText.
    /// </summary>
    /// <param name="source">Объект TdApi.FormattedText для преобразования.</param>
    /// <param name="destination">Целевой объект TgFormatedText.</param>
    /// <param name="context">Контекст преобразования.</param>
    /// <returns>Преобразованный объект TgFormatedText.</returns>
    public TgFormatedText? Convert(TdApi.FormattedText? source, TgFormatedText? destination, ResolutionContext context)
    {
        // Если объект source равен null или его свойство Text равно null или пустой строке, возвращаем null.
        if (source == null || string.IsNullOrEmpty(source.Text)) return null;
        
        // Создаем новый объект TgFormatedText и заполняем его свойство Text значением из свойства Text объекта source.
        return new TgFormatedText { Text = source.Text };
    }
}