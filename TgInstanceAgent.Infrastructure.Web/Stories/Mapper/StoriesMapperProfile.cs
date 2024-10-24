using TgInstanceAgent.Application.Abstractions.Commands.TgStories;
using TgInstanceAgent.Application.Abstractions.Interfaces;
using TgInstanceAgent.Application.Abstractions.Queries.TgStories;
using TgInstanceAgent.Infrastructure.Web.Components.Interfaces;
using TgInstanceAgent.Infrastructure.Web.Components.Mapper;
using TgInstanceAgent.Infrastructure.Web.Stories.InputModels;

namespace TgInstanceAgent.Infrastructure.Web.Stories.Mapper;

/// <summary>
/// Класс для маппинга входных моделей историй в команды
/// </summary>
public class StoriesMapperProfile : AutoMapper.Profile
{
    /// <summary>
    /// Маппинг входных моделей в команды
    /// </summary>
    public StoriesMapperProfile()
    {
        // Карта для ProxyTypeHttpInputModel в ProxyTypeHttp
        CreateMap<GetChatActiveStoriesInputModel, GetChatActiveStoriesQuery>().MapInstanceId();
        
        // Карта для ProxyTypeHttpInputModel в ProxyTypeHttp
        CreateMap<GetStoryInputModel, GetStoryQuery>().MapInstanceId();

        // Карта для StoryPrivacySettingsContactsInputModel в StoryPrivacySettingsContactsData
        CreateMap<StoryPrivacySettingsContactsInputModel, StoryPrivacySettingsContactsData>();
        
        // Карта для StoryPrivacySettingsEveryoneInputModel в StoryPrivacySettingsEveryoneData
        CreateMap<StoryPrivacySettingsEveryoneInputModel, StoryPrivacySettingsEveryoneData>();
        
        // Карта для StoryPrivacySettingsSelectedUsersInputModel в StoryPrivacySettingsSelectedUsersData
        CreateMap<StoryPrivacySettingsSelectedUsersInputModel, StoryPrivacySettingsSelectedUsersData>();
        
        // Карта для StoryPrivacySettingsCloseFriendsInputModel в StoryPrivacySettingsCloseFriendsData
        CreateMap<StoryPrivacySettingsCloseFriendsInputModel, StoryPrivacySettingsCloseFriendsData>();
        
        // Карта для PostPhotoStoryInputModel в PostPhotoStoryCommand
        CreateMap<PostPhotoStoryInputModel, PostPhotoStoryCommand>()
            .ForMember(dest => dest.ActivePeriod, opt => opt.MapFrom(src => src.ActivePeriod * 3600))
            
            .MapInstanceId();
        
        // Карта для PostVideoStoryInputModel в PostVideoStoryCommand
        CreateMap<PostVideoStoryInputModel, PostVideoStoryCommand>()
            .ForMember(dest => dest.ActivePeriod, opt => opt.MapFrom(src => src.ActivePeriod * 3600))
            
            .MapInstanceId();
    }
}