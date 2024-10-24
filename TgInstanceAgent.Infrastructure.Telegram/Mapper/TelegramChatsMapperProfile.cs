using AutoMapper;
using TdLib;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Objects.Chats;
using TgInstanceAgent.Infrastructure.Telegram.Extensions;

namespace TgInstanceAgent.Infrastructure.Telegram.Mapper;

/// <summary>
/// Класс для маппинга объектов Telegram
/// </summary>
public class TelegramChatsMapperProfile : Profile
{
    /// <summary>
    /// Конструктор
    /// </summary>
    public TelegramChatsMapperProfile()
    {
        // Карта для TdApi.ChatFolderInfo в TgChatFolderInfo
        CreateMap<TdApi.ChatFolderInfo, TgChatFolderInfo>()
            .ForMember(c => c.IconName, opt => opt.MapFrom(s => s.Icon.Name));
        
        // Карта для TdApi.Chat в TgChat
        CreateMap<TdApi.Chat, TgChat>()
            .ForMember(c => c.Title, opt => opt.MapFrom(s => s.Title.NullIfEmpty()))
            .ForMember(p => p.IsStoriesBlocked,
                opt => opt.MapFrom(s => s.BlockList is TdApi.BlockList.BlockListStories))
            .ForMember(p => p.IsBlocked, opt => opt.MapFrom(s => s.BlockList is TdApi.BlockList.BlockListMain));

        // Карта для TdApi.ChatJoinRequestsInfo в TgChatJoinRequestsInfo
        CreateMap<TdApi.ChatJoinRequestsInfo, TgChatJoinRequestsInfo>();

        // Карта для TdApi.ChatPermissions в TgChatPermissions
        CreateMap<TdApi.ChatPermissions, TgChatPermissions>();

        // Карта для TdApi.ChatPosition в TgChatPosition
        CreateMap<TdApi.ChatPosition, TgChatPosition>()
            .ForMember(p => p.ChatFolderId,
                opt => opt.MapFrom(s =>
                    s.List.GetType() == typeof(TdApi.ChatList.ChatListFolder)
                        ? ((TdApi.ChatList.ChatListFolder)s.List).ChatFolderId
                        : new int?()));
        
        // Карта для TdApi.Update.UpdateUnreadChatCount в TgUnreadChatsCount
        CreateMap<TdApi.Update.UpdateUnreadChatCount, TgUnreadChatsCount>()
            .ForMember(p => p.ChatFolderId,
                opt => opt.MapFrom(s =>
                    s.ChatList.GetType() == typeof(TdApi.ChatList.ChatListFolder)
                        ? ((TdApi.ChatList.ChatListFolder)s.ChatList).ChatFolderId
                        : new int?()));

        // Карта для TdApi.Update.UpdateUnreadMessageCount в TgUnreadMessagesCount
        CreateMap<TdApi.Update.UpdateUnreadMessageCount, TgUnreadMessagesCount>()
            .ForMember(p => p.ChatFolderId,
                opt => opt.MapFrom(s =>
                    s.ChatList.GetType() == typeof(TdApi.ChatList.ChatListFolder)
                        ? ((TdApi.ChatList.ChatListFolder)s.ChatList).ChatFolderId
                        : new int?()));
        
        // Карта для TdApi.ChatPhotoInfo в TgChatPhotoInfo
        CreateMap<TdApi.ChatPhotoInfo, TgChatPhotoInfo>()
            .ForMember(p => p.MiniThumbnail, opt => opt.MapFrom(s => s.Minithumbnail));

        // Карты для TdApi.ChatType в TgChatType
        // Включает производные типы
        CreateMap<TdApi.ChatType.ChatTypePrivate, TgChatTypePrivate>();
        CreateMap<TdApi.ChatType.ChatTypeSecret, TgChatTypeSecret>();
        CreateMap<TdApi.ChatType.ChatTypeSupergroup, TgChatTypeSupergroup>();
        CreateMap<TdApi.ChatType.ChatTypeBasicGroup, TgChatTypeBasicGroup>();
        CreateMap<TdApi.ChatType, TgChatType>()
            .Include<TdApi.ChatType.ChatTypePrivate, TgChatTypePrivate>()
            .Include<TdApi.ChatType.ChatTypeSecret, TgChatTypeSecret>()
            .Include<TdApi.ChatType.ChatTypeSupergroup, TgChatTypeSupergroup>()
            .Include<TdApi.ChatType.ChatTypeBasicGroup, TgChatTypeBasicGroup>();

        // Карты для Api.ChatAvailableReactions в TgChatAvailableReactions
        // Включает производные типы
        CreateMap<TdApi.ChatAvailableReactions.ChatAvailableReactionsAll, TgChatAvailableReactionsAll>();
        CreateMap<TdApi.ChatAvailableReactions.ChatAvailableReactionsSome, TgChatAvailableReactionsSome>();
        CreateMap<TdApi.ChatAvailableReactions, TgChatAvailableReactions>()
            .Include<TdApi.ChatAvailableReactions.ChatAvailableReactionsAll, TgChatAvailableReactionsAll>()
            .Include<TdApi.ChatAvailableReactions.ChatAvailableReactionsSome, TgChatAvailableReactionsSome>();
        
        // Карта для Api.ChatBackground в TgChatBackground
        CreateMap<TdApi.ChatBackground, TgChatBackground>();
        
        // Карты для TdApi.ChatAction в TgChatAction
        // Включает производные типы
        CreateMap<TdApi.ChatAction.ChatActionCancel, TgChatActionCancel>();
        CreateMap<TdApi.ChatAction.ChatActionChoosingContact, TgChatActionChoosingContact>();
        CreateMap<TdApi.ChatAction.ChatActionChoosingLocation, TgChatActionChoosingLocation>();
        CreateMap<TdApi.ChatAction.ChatActionChoosingSticker, TgChatActionChoosingSticker>();
        CreateMap<TdApi.ChatAction.ChatActionRecordingVideo, TgChatActionRecordingVideo>();
        CreateMap<TdApi.ChatAction.ChatActionRecordingVideoNote, TgChatActionRecordingVideoNote>();
        CreateMap<TdApi.ChatAction.ChatActionRecordingVoiceNote, TgChatActionRecordingVoiceNote>();
        CreateMap<TdApi.ChatAction.ChatActionStartPlayingGame, TgChatActionStartPlayingGame>();
        CreateMap<TdApi.ChatAction.ChatActionTyping, TgChatActionTyping>();
        CreateMap<TdApi.ChatAction.ChatActionUploadingDocument, TgChatActionUploadingDocument>();
        CreateMap<TdApi.ChatAction.ChatActionUploadingPhoto, TgChatActionUploadingPhoto>();
        CreateMap<TdApi.ChatAction.ChatActionUploadingVideo, TgChatActionUploadingVideo>();
        CreateMap<TdApi.ChatAction.ChatActionUploadingVideoNote, TgChatActionUploadingVideoNote>();
        CreateMap<TdApi.ChatAction.ChatActionUploadingVoiceNote, TgChatActionUploadingVoiceNote>();
        CreateMap<TdApi.ChatAction.ChatActionWatchingAnimations, TgChatActionWatchingAnimations>();
        CreateMap<TdApi.ChatAction, TgChatAction>()
            .Include<TdApi.ChatAction.ChatActionCancel, TgChatActionCancel>()
            .Include<TdApi.ChatAction.ChatActionChoosingContact, TgChatActionChoosingContact>()
            .Include<TdApi.ChatAction.ChatActionChoosingLocation, TgChatActionChoosingLocation>()
            .Include<TdApi.ChatAction.ChatActionChoosingSticker, TgChatActionChoosingSticker>()
            .Include<TdApi.ChatAction.ChatActionRecordingVideo, TgChatActionRecordingVideo>()
            .Include<TdApi.ChatAction.ChatActionRecordingVideoNote, TgChatActionRecordingVideoNote>()
            .Include<TdApi.ChatAction.ChatActionRecordingVoiceNote, TgChatActionRecordingVoiceNote>()
            .Include<TdApi.ChatAction.ChatActionStartPlayingGame, TgChatActionStartPlayingGame>()
            .Include<TdApi.ChatAction.ChatActionTyping, TgChatActionTyping>()
            .Include<TdApi.ChatAction.ChatActionUploadingDocument, TgChatActionUploadingDocument>()
            .Include<TdApi.ChatAction.ChatActionUploadingPhoto, TgChatActionUploadingPhoto>()
            .Include<TdApi.ChatAction.ChatActionUploadingVideo, TgChatActionUploadingVideo>()
            .Include<TdApi.ChatAction.ChatActionUploadingVideoNote, TgChatActionUploadingVideoNote>()
            .Include<TdApi.ChatAction.ChatActionUploadingVoiceNote, TgChatActionUploadingVoiceNote>()
            .Include<TdApi.ChatAction.ChatActionWatchingAnimations, TgChatActionWatchingAnimations>();
        
        // Карта для TdApi.BasicGroupFullInfo в TgBasicGroupFullInfo
        CreateMap<TdApi.BasicGroupFullInfo, TgBasicGroupFullInfo>();
        
        // Карта для TdApi.ChatMember в TgChatMember
        CreateMap<TdApi.ChatMember, TgChatMember>();
        
        // Карты для TdApi.ChatMemberStatus в TgChatMemberStatus
        // Включает производные типы
        CreateMap<TdApi.ChatMemberStatus.ChatMemberStatusAdministrator, TgChatMemberStatusAdministrator>();
        CreateMap<TdApi.ChatMemberStatus.ChatMemberStatusBanned, TgChatMemberStatusBanned>();
        CreateMap<TdApi.ChatMemberStatus.ChatMemberStatusCreator, TgChatMemberStatusCreator>();
        CreateMap<TdApi.ChatMemberStatus.ChatMemberStatusLeft, TgChatMemberStatusLeft>();
        CreateMap<TdApi.ChatMemberStatus.ChatMemberStatusMember, TgChatMemberStatusMember>();
        CreateMap<TdApi.ChatMemberStatus.ChatMemberStatusRestricted, TgChatMemberStatusRestricted>();
        CreateMap<TdApi.ChatMemberStatus, TgChatMemberStatus>()
            .Include<TdApi.ChatMemberStatus.ChatMemberStatusRestricted, TgChatMemberStatusRestricted>()
            .Include<TdApi.ChatMemberStatus.ChatMemberStatusMember, TgChatMemberStatusMember>()
            .Include<TdApi.ChatMemberStatus.ChatMemberStatusLeft, TgChatMemberStatusLeft>()
            .Include<TdApi.ChatMemberStatus.ChatMemberStatusCreator, TgChatMemberStatusCreator>()
            .Include<TdApi.ChatMemberStatus.ChatMemberStatusBanned, TgChatMemberStatusBanned>()
            .Include<TdApi.ChatMemberStatus.ChatMemberStatusAdministrator, TgChatMemberStatusAdministrator>();
        
        // Карта для TdApi.ChatInviteLink в TgChatInviteLink
        CreateMap<TdApi.ChatInviteLink, TgChatInviteLink>();
        
        // Карта для TdApi.BotCommands в TgBotCommands
        CreateMap<TdApi.BotCommands, TgBotCommands>();
        
        // Карта для TdApi.BotCommand в TgBotCommand
        CreateMap<TdApi.BotCommand, TgBotCommand>();
        
        // Карта для TdApi.ChatNotificationSettings в TgChatNotificationSettings
        CreateMap<TdApi.ChatNotificationSettings, TgChatNotificationSettings>();
    }
}