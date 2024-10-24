using AutoMapper;
using TdLib;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Functions.Enums;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Objects.Enums;
using TgInstanceAgent.Infrastructure.Telegram.Mapper.Converters;

namespace TgInstanceAgent.Infrastructure.Telegram.Mapper;


/// <summary>
/// Класс для маппинга объектов Telegram в enum
/// </summary>
public class TelegramEnumsMapperProfile : Profile
{
    /// <summary>
    /// Конструктор
    /// </summary>
    public TelegramEnumsMapperProfile()
    {
        // Карта для TgMessageFilterType в TdApi.SearchMessagesFilter
        CreateMap<TgMessageFilterType, TdApi.SearchMessagesFilter>().ConvertUsing<SearchMessagesFilterConverter>();
        
        // Карта для TdApi.ChatList в TgChatList
        CreateMap<TdApi.ChatList, TgChatList>().ConvertUsing<ChatListConverter>();
        
        // Карта для TdApi.CallDiscardReason в TgCallDiscardReason
        CreateMap<TdApi.CallDiscardReason, TgCallDiscardReason?>().ConvertUsing<DiscardReasonConverter>();
        
        // Карта для TdApi.StoryPrivacySettings в TgStoryPrivacySettings
        CreateMap<TdApi.StoryPrivacySettings, TgStoryPrivacySettings?>().ConvertUsing<StoryPrivacySettingsConverter>();
        
        // Карта для TdApi.StoryList в TgStoryTypeList
        CreateMap<TdApi.StoryList, TgStoryTypeList?>().ConvertUsing<StoryTypeListConverter>();
    }
}