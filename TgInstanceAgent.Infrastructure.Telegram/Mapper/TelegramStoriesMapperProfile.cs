using AutoMapper;
using TdLib;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Objects.Stories;

namespace TgInstanceAgent.Infrastructure.Telegram.Mapper;

/// <summary>
/// Класс для маппинга объектов Telegram в истории
/// </summary>
public class TelegramStoriesMapperProfile : Profile
{
    /// <summary>
    /// Конструктор
    /// </summary>
    public TelegramStoriesMapperProfile()
    {
        // Карта для TdApi.Story в TgStory
        CreateMap<TdApi.Story, TgStory>();

        // Карта для TdApi.Story в TgStory
        CreateMap<TdApi.StoryInteractionInfo, TgStoryInteractionInfo>();
        
        // Карта для TdApi.StoryInfo в TgStoryInfo
        CreateMap<TdApi.ChatActiveStories, TgChatActiveStories>();

        // Карта для TdApi.StoryInfo в TgStoryInfo
        CreateMap<TdApi.StoryInfo, TgStoryInfo>();
        
        // Карты для TdApi.StoryContent в TgStoryContent
        // Включает производные типы
        CreateMap<TdApi.StoryContent.StoryContentVideo, TgStoryVideo>();
        CreateMap<TdApi.StoryContent.StoryContentPhoto, TgStoryPhoto>();
        CreateMap<TdApi.StoryContent.StoryContentUnsupported, TgUnsupportedStory>();
        CreateMap<TdApi.StoryContent, TgStoryContent>()
            .Include<TdApi.StoryContent.StoryContentVideo, TgStoryVideo>()
            .Include<TdApi.StoryContent.StoryContentPhoto, TgStoryPhoto>()
            .Include<TdApi.StoryContent.StoryContentUnsupported, TgUnsupportedStory>();
    }
}