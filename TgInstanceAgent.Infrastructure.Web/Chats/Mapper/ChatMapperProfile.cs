using TgInstanceAgent.Application.Abstractions.Commands.TgChats;
using TgInstanceAgent.Application.Abstractions.Queries.TgChats;
using TgInstanceAgent.Infrastructure.Web.Chats.InputModels;
using TgInstanceAgent.Infrastructure.Web.Components.Mapper;

namespace TgInstanceAgent.Infrastructure.Web.Chats.Mapper;

/// <summary>
/// Класс для маппинга входных моделей для работы с чатами в команды
/// </summary>
public class ChatMapperProfile : AutoMapper.Profile
{
    /// <summary>
    /// Маппинг входных моделей в команды
    /// </summary>
    public ChatMapperProfile()
    {
        // Карта для CreateChatInputModel в CreateChatCommand
        CreateMap<CreateChatInputModel, CreateChatCommand>().MapInstanceId();

        // Карта для ChatInputModel в OpenChatCommand
        CreateMap<ChatInputModel, OpenChatCommand>().MapInstanceId();

        // Карта для ChatInputModel в CloseChatCommand
        CreateMap<ChatInputModel, CloseChatCommand>().MapInstanceId();
        
        // Карта для GetChatInputModel в GetChatQuery
        CreateMap<ChatInputModel, GetChatQuery>().MapInstanceId();
        
        // Карта для GetChatsInputModel в GetChatsQuery
        CreateMap<GetChatsInputModel, GetChatsQuery>().MapInstanceId();
        
        // Карта для GetChatHistoryInputModel в GetChatHistoryQuery
        CreateMap<GetChatHistoryInputModel, GetChatHistoryQuery>().MapInstanceId();
        
        // Карта для SearchPublicChatsInputModel в SearchPublicChatsQuery
        CreateMap<SearchPublicChatsInputModel, SearchPublicChatsQuery>().MapInstanceId();
        
        // Карта для SearchChatsInputModel в SearchChatsQuery
        CreateMap<SearchChatsInputModel, SearchChatsQuery>().MapInstanceId();
        
        // Карта для SearchChatMessagesInputModel в SearchChatMessagesQuery
        CreateMap<SearchChatMessagesInputModel, SearchChatMessagesQuery>().MapInstanceId();
        
        // Карта для SearchMessagesInputModel в SearchMessagesQuery
        CreateMap<SearchMessagesInputModel, SearchMessagesQuery>().MapInstanceId();
        
        // Карта для SetChatTitleInputModel в SetChatTitleCommand
        CreateMap<SetChatTitleInputModel, SetChatTitleCommand>().MapInstanceId();
        
        // Карта для SetChatDescriptionInputModel в SetChatDescriptionCommand
        CreateMap<SetChatDescriptionInputModel, SetChatDescriptionCommand>().MapInstanceId();
        
        // Карта для SetChatNotificationSettingsInputModel в SetChatNotificationSettingsCommand
        CreateMap<SetChatNotificationSettingsInputModel, SetChatNotificationSettingsCommand>().MapInstanceId();
        
        // Карта для SetChatAccentColorInputModel в SetChatAccentColorCommand
        CreateMap<SetChatAccentColorInputModel, SetChatAccentColorCommand>().MapInstanceId();
        
        // Карта для SetChatPermissionsInputModel в SetChatPermissionsCommand
        CreateMap<SetChatPermissionsInputModel, SetChatPermissionsCommand>().MapInstanceId();
        
        // Карта для SetChatSlowModeDelayInputModel в SetChatSlowModeDelayCommand
        CreateMap<SetChatSlowModeDelayInputModel, SetChatSlowModeDelayCommand>().MapInstanceId();
        
        // Карта для SetChatMessageAutoDeleteTimeInputModel в SetChatMessageAutoDeleteTimeCommand
        CreateMap<SetChatMessageAutoDeleteTimeInputModel, SetChatMessageAutoDeleteTimeCommand>().MapInstanceId();
        
        // Карта для SetChatAvailableReactionsInputModel в SetChatAvailableReactionsCommand
        CreateMap<SetChatAvailableReactionsInputModel, SetChatAvailableReactionsCommand>().MapInstanceId();
        
        // Карта для SetChatMessageSenderInputModel в SetChatMessageSenderCommand
        CreateMap<SetChatMessageSenderInputModel, SetChatMessageSenderCommand>().MapInstanceId();
        
        // Карта для SetChatEmojiStatusInputModel в SetChatEmojiStatusCommand
        CreateMap<SetChatEmojiStatusInputModel, SetChatEmojiStatusCommand>().MapInstanceId();
        
        // Карта для SetChatProfileAccentColorInputModel в SetChatProfileAccentColorCommand
        CreateMap<SetChatProfileAccentColorInputModel, SetChatProfileAccentColorCommand>().MapInstanceId();
        
        // Карта для SetChatDiscussionGroupInputModel в SetChatDiscussionGroupCommand
        CreateMap<SetChatDiscussionGroupInputModel, SetChatDiscussionGroupCommand>().MapInstanceId();
        
        // Карта для SetChatProfilePhotoInputModel в SetChatProfilePhotoCommand
        CreateMap<SetChatProfilePhotoInputModel, SetChatProfilePhotoCommand>().MapInstanceId();
        
        // Карта для SetChatEmojiProfilePhotoInputModel в SetChatEmojiProfilePhotoCommand
        CreateMap<SetChatEmojiProfilePhotoInputModel, SetChatEmojiProfilePhotoCommand>().MapInstanceId();
    }
}