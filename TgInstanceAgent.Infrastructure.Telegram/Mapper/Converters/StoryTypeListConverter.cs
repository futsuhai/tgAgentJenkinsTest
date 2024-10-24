using AutoMapper;
using TdLib;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Objects.Enums;

namespace TgInstanceAgent.Infrastructure.Telegram.Mapper.Converters;

/// <summary>
/// Конвертер для преобразования объекта StoryList в объект TgStoryTypeList.
/// </summary>
public class StoryTypeListConverter : ITypeConverter<TdApi.StoryList, TgStoryTypeList?>
{
    /// <summary>
    /// Преобразует объект StoryList в TgStoryTypeList.
    /// </summary>
    /// <param name="source">Объект StoryList для преобразования.</param>
    /// <param name="destination">Целевой объект TgStoryTypeList.</param>
    /// <param name="context">Контекст преобразования.</param>
    /// <returns>Преобразованный объект TgStoryPrivacySettings.</returns>
    public TgStoryTypeList? Convert(TdApi.StoryList source, TgStoryTypeList? destination, ResolutionContext context)
    {
        // Преобразуем объект StoryList в объект TgStoryTypeList с помощью оператора switch
        return source switch
        {
            // Возвращаем TgStoryTypeList.Main, если source равен StoryList.StoryListMain
            TdApi.StoryList.StoryListMain => TgStoryTypeList.Main,

            // Возвращаем TgStoryTypeList.Archive, если source равен StoryList.StoryListArchive
            TdApi.StoryList.StoryListArchive => TgStoryTypeList.Archive,

            // Выбрасываем исключение, если source не равен ни одному из ожидаемых значений
            _ => throw new ArgumentOutOfRangeException(nameof(source))
        };
    }
}