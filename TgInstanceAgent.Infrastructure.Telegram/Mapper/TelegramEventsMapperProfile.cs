using AutoMapper;
using TdLib;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Events;

namespace TgInstanceAgent.Infrastructure.Telegram.Mapper;

/// <summary>
/// Класс для маппинга объектов Telegram в события
/// </summary>
public class TelegramEventsMapperProfile : Profile
{
    /// <summary>
    /// Конструктор
    /// </summary>
    public TelegramEventsMapperProfile()
    {
        // Карта для TdApi.Update.UpdateChatFolders в TgUpdateChatFoldersEvent
        CreateMap<TdApi.Update.UpdateChatFolders, TgUpdateChatFoldersEvent>();

        // Карта для TdApi.Update.UpdateChatMessageAutoDeleteTime в TgUpdateChatMessageAutoDeleteTimeEvent
        CreateMap<TdApi.Update.UpdateChatMessageAutoDeleteTime, TgUpdateChatMessageAutoDeleteTimeEvent>();

        // Карта для TdApi.Update.UpdateDeleteMessages в TgMessagesDeletedEvent
        CreateMap<TdApi.Update.UpdateDeleteMessages, TgMessagesDeletedEvent>();

        // Карта для TdApi.Update.UpdateChatTitle в TgUpdateChatTitleEvent
        CreateMap<TdApi.Update.UpdateChatTitle, TgUpdateChatTitleEvent>();

        // Карта для TdApi.Update.UpdateChatPhoto в TgUpdateChatPhotoEvent
        CreateMap<TdApi.Update.UpdateChatPhoto, TgUpdateChatPhotoEvent>();

        // Карта для TdApi.Update.UpdateChatIsMarkedAsUnread в TgUpdateChatMarkedAsUnreadEvent
        CreateMap<TdApi.Update.UpdateChatIsMarkedAsUnread, TgUpdateChatMarkedAsUnreadEvent>();

        // Карта для TdApi.Update.UpdateMessageContent в TgUpdateMessageContent
        CreateMap<TdApi.Update.UpdateMessageContent, TgUpdateMessageContent>();

        // Карта для TdApi.Update.UpdateMessageLiveLocationViewed в TgMessageLiveLocationViewedEvent
        CreateMap<TdApi.Update.UpdateMessageLiveLocationViewed, TgMessageLiveLocationViewedEvent>();

        // Карта для TdApi.Update.UpdateStoryDeleted в TgStoryDeletedEvent
        CreateMap<TdApi.Update.UpdateStoryDeleted, TgStoryDeletedEvent>();

        // Карта для TdApi.Update.UpdateChatEmojiStatus в TgUpdateChatEmojiStatusEvent
        CreateMap<TdApi.Update.UpdateChatEmojiStatus, TgUpdateChatEmojiStatusEvent>();

        // Карта для TdApi.Update.UpdateChatOnlineMemberCount в TgUpdateChatOnlineMemberCountEvent
        CreateMap<TdApi.Update.UpdateChatOnlineMemberCount, TgUpdateChatOnlineMemberCountEvent>();

        // Карта для TdApi.Update.UpdateChatPermissions в TgUpdateChatPermissionsEvent
        CreateMap<TdApi.Update.UpdateChatPermissions, TgUpdateChatPermissionsEvent>();

        // Карта для TdApi.Update.UpdateChatHasProtectedContent в TgUpdateChatProtectedContentEvent
        CreateMap<TdApi.Update.UpdateChatHasProtectedContent, TgUpdateChatProtectedContentEvent>();

        // Карта для TdApi.Update.UpdateChatRemovedFromList в TgChatRemovedFromListEvent
        CreateMap<TdApi.Update.UpdateChatRemovedFromList, TgChatRemovedFromListEvent>();

        // Карта для TdApi.Update.UpdateChatHasScheduledMessages в TgUpdateChatScheduledMessagesEvent
        CreateMap<TdApi.Update.UpdateChatHasScheduledMessages, TgUpdateChatScheduledMessagesEvent>();

        // Карта для TdApi.Update.UpdateFileDownloads в TgUpdateFileDownloadsEvent
        CreateMap<TdApi.Update.UpdateFileDownloads, TgUpdateFileDownloadsEvent>();

        // Карта для TdApi.Update.UpdateUserStatus в TgUpdateUserStatusEvent
        CreateMap<TdApi.Update.UpdateUserStatus, TgUpdateUserStatusEvent>();

        // Карта для TdApi.Update.UpdateChatReadInbox в TgUpdateChatReadInboxEvent
        CreateMap<TdApi.Update.UpdateChatReadInbox, TgUpdateChatReadInboxEvent>();

        // Карта для TdApi.Update.UpdateChatUnreadMentionCount в TgUpdateChatUnreadMentionEvent
        CreateMap<TdApi.Update.UpdateChatUnreadMentionCount, TgUpdateChatUnreadMentionEvent>();

        // Карта для TdApi.Update.UpdateChatUnreadReactionCount в TgUpdateChatUnreadReactionEvent
        CreateMap<TdApi.Update.UpdateChatUnreadReactionCount, TgUpdateChatUnreadReactionEvent>();

        // Пользовательское маппинг для UpdateChatBlockList в TgUpdateChatBlockListEvent
        CreateMap<TdApi.Update.UpdateChatBlockList, TgUpdateChatBlockListEvent>()
            .ForMember(p => p.IsStoriesBlocked,
                opt => opt.MapFrom(s => s.BlockList is TdApi.BlockList.BlockListStories))
            .ForMember(p => p.IsBlocked, opt => opt.MapFrom(s => s.BlockList is TdApi.BlockList.BlockListMain));

        // Карта для TdApi.Update.UpdateChatBackground в TgUpdateChatBackgroundEvent
        CreateMap<TdApi.Update.UpdateChatBackground, TgUpdateChatBackgroundEvent>();

        // Карта для TdApi.Update.UpdateChatAction в TgChatActionEvent
        CreateMap<TdApi.Update.UpdateChatAction, TgChatActionEvent>();

        // Карта для TdApi.Update.UpdateChatActiveStories в TgUpdateChatActiveStories
        CreateMap<TdApi.Update.UpdateChatActiveStories, TgUpdateChatActiveStories>();

        // Карта для TdApi.Update.UpdateFile в TgUpdateFileEvent
        CreateMap<TdApi.Update.UpdateFile, TgUpdateFileEvent>();
        
        // Карта для TdApi.Update.UpdateMessageIsPinned в TgMessagePinnedEvent
        CreateMap<TdApi.Update.UpdateMessageIsPinned, TgMessagePinnedEvent>();

        // Карта для TdApi.Update.UpdateUnreadChatCount в TgUpdateUnreadChatCount
        CreateMap<TdApi.Update.UpdateUnreadChatCount, TgUpdateUnreadChatCount>()
            .ForMember(p => p.ChatFolderId,
                opt => opt.MapFrom(s =>
                    s.ChatList.GetType() == typeof(TdApi.ChatList.ChatListFolder)
                        ? ((TdApi.ChatList.ChatListFolder)s.ChatList).ChatFolderId
                        : new int?()));

        // Карта для TdApi.Update.UpdateUnreadMessageCount в TgUpdateUnreadMessageCount
        CreateMap<TdApi.Update.UpdateUnreadMessageCount, TgUpdateUnreadMessageCount>()
            .ForMember(p => p.ChatFolderId,
                opt => opt.MapFrom(s =>
                    s.ChatList.GetType() == typeof(TdApi.ChatList.ChatListFolder)
                        ? ((TdApi.ChatList.ChatListFolder)s.ChatList).ChatFolderId
                        : new int?()));
        
        // Карта для TdApi.Update.UpdateBasicGroupFullInfo в TgUpdateBasicGroupFullInfo
        CreateMap<TdApi.Update.UpdateBasicGroupFullInfo, TgUpdateBasicGroupFullInfo>();
        
        // Карта для TdApi.Update.UpdateChatNotificationSettings в TgUpdateChatNotificationSettings
        CreateMap<TdApi.Update.UpdateChatNotificationSettings, TgUpdateChatNotificationSettings>();
        
        // Карта для TdApi.Update.UpdateChatAccentColors в TgUpdateChatAccentColors
        CreateMap<TdApi.Update.UpdateChatAccentColors, TgUpdateChatAccentColors>();
        
        // Карта для TdApi.Update.UpdateChatMessageAutoDeleteTime в TgUpdateChatMessageAutoDeleteTime
        CreateMap<TdApi.Update.UpdateChatMessageAutoDeleteTime, TgUpdateChatMessageAutoDeleteTime>();
        
        // Карта для TdApi.Update.UpdateChatAvailableReactions в TgUpdateChatAvailableReactions
        CreateMap<TdApi.Update.UpdateChatAvailableReactions, TgUpdateChatAvailableReactions>();
        
        // Карта для TdApi.Update.UpdateChatMessageSender в TgUpdateChatMessageSender
        CreateMap<TdApi.Update.UpdateChatMessageSender, TgUpdateChatMessageSender>();

        // Маппинг базового объекта обновления TdLib в базовый класс TgEvent
        // И включение всех специфичных маппингов, определяющих обновления TdLib
        CreateMap<TdApi.Update, TgEvent>()
            .Include<TdApi.Update.UpdateChatFolders, TgUpdateChatFoldersEvent>()
            .Include<TdApi.Update.UpdateChatMessageAutoDeleteTime, TgUpdateChatMessageAutoDeleteTimeEvent>()
            .Include<TdApi.Update.UpdateDeleteMessages, TgMessagesDeletedEvent>()
            .Include<TdApi.Update.UpdateChatTitle, TgUpdateChatTitleEvent>()
            .Include<TdApi.Update.UpdateChatPhoto, TgUpdateChatPhotoEvent>()
            .Include<TdApi.Update.UpdateChatIsMarkedAsUnread, TgUpdateChatMarkedAsUnreadEvent>()
            .Include<TdApi.Update.UpdateMessageContent, TgUpdateMessageContent>()
            .Include<TdApi.Update.UpdateMessageLiveLocationViewed, TgMessageLiveLocationViewedEvent>()
            .Include<TdApi.Update.UpdateStoryDeleted, TgStoryDeletedEvent>()
            .Include<TdApi.Update.UpdateChatEmojiStatus, TgUpdateChatEmojiStatusEvent>()
            .Include<TdApi.Update.UpdateChatOnlineMemberCount, TgUpdateChatOnlineMemberCountEvent>()
            .Include<TdApi.Update.UpdateChatPermissions, TgUpdateChatPermissionsEvent>()
            .Include<TdApi.Update.UpdateChatHasProtectedContent, TgUpdateChatProtectedContentEvent>()
            .Include<TdApi.Update.UpdateChatRemovedFromList, TgChatRemovedFromListEvent>()
            .Include<TdApi.Update.UpdateChatHasScheduledMessages, TgUpdateChatScheduledMessagesEvent>()
            .Include<TdApi.Update.UpdateFileDownloads, TgUpdateFileDownloadsEvent>()
            .Include<TdApi.Update.UpdateUserStatus, TgUpdateUserStatusEvent>()
            .Include<TdApi.Update.UpdateChatAction, TgChatActionEvent>()
            .Include<TdApi.Update.UpdateChatReadInbox, TgUpdateChatReadInboxEvent>()
            .Include<TdApi.Update.UpdateChatUnreadMentionCount, TgUpdateChatUnreadMentionEvent>()
            .Include<TdApi.Update.UpdateChatUnreadReactionCount, TgUpdateChatUnreadReactionEvent>()
            .Include<TdApi.Update.UpdateChatBlockList, TgUpdateChatBlockListEvent>()
            .Include<TdApi.Update.UpdateChatActiveStories, TgUpdateChatActiveStories>()
            .Include<TdApi.Update.UpdateFile, TgUpdateFileEvent>()
            .Include<TdApi.Update.UpdateMessageIsPinned, TgMessagePinnedEvent>()
            .Include<TdApi.Update.UpdateChatBackground, TgUpdateChatBackgroundEvent>()
            .Include<TdApi.Update.UpdateUnreadChatCount, TgUpdateUnreadChatCount>()
            .Include<TdApi.Update.UpdateBasicGroupFullInfo, TgUpdateBasicGroupFullInfo>()
            .Include<TdApi.Update.UpdateChatNotificationSettings, TgUpdateChatNotificationSettings>()
            .Include<TdApi.Update.UpdateChatAccentColors, TgUpdateChatAccentColors>()
            .Include<TdApi.Update.UpdateChatMessageAutoDeleteTime, TgUpdateChatMessageAutoDeleteTime>()
            .Include<TdApi.Update.UpdateChatAvailableReactions, TgUpdateChatAvailableReactions>()
            .Include<TdApi.Update.UpdateChatMessageSender, TgUpdateChatMessageSender>()
            .Include<TdApi.Update.UpdateUnreadMessageCount, TgUpdateUnreadMessageCount>();
    }
}