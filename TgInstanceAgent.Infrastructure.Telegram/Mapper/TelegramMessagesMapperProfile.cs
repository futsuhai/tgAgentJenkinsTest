using AutoMapper;
using TdLib;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Objects.Messages;
using TgInstanceAgent.Infrastructure.Telegram.Extensions;
using TgInstanceAgent.Infrastructure.Telegram.Mapper.Converters;

namespace TgInstanceAgent.Infrastructure.Telegram.Mapper;

/// <summary>
/// Класс для маппинга объектов Telegram в сообщения
/// </summary>
public class TelegramMessagesMapperProfile : Profile
{
    /// <summary>
    /// Конструктор
    /// </summary>
    public TelegramMessagesMapperProfile()
    {
        #region Messages

        // Карта для TdApi.Message в TgMessage
        CreateMap<TdApi.Message, TgMessage>()
            .ForMember(m => m.AuthorSignature, opt => opt.MapFrom(s => s.AuthorSignature.NullIfEmpty()));

        // Карта для TdApi.MessageContent.MessageSchedulingState в TgMessageSchedulingState
        // Включает производные типы
        CreateMap<TdApi.MessageSchedulingState.MessageSchedulingStateSendAtDate, TgMessageSchedulingStateSendAtDate>();
        CreateMap<TdApi.MessageSchedulingState.MessageSchedulingStateSendWhenOnline, TgMessageSchedulingStateSendWhenOnline>();
        CreateMap<TdApi.MessageSchedulingState, TgMessageSchedulingState>()
            .Include<TdApi.MessageSchedulingState.MessageSchedulingStateSendAtDate,
                TgMessageSchedulingStateSendAtDate>()
            .Include<TdApi.MessageSchedulingState.MessageSchedulingStateSendWhenOnline,
                TgMessageSchedulingStateSendWhenOnline>();
        
        // Карта для TdApi.MessageContent в TgMessageContent
        CreateMap<TdApi.MessageContent, TgMessageContent>().ConvertUsing<MessageContentConverter>();

        // Карта для TdApi.MessageContent.MessageStory в TgStoryMessage
        CreateMap<TdApi.MessageContent.MessageStory, TgStoryMessage>();

        // Карта для TdApi.MessageContent.MessageCall в TgCallMessage
        CreateMap<TdApi.MessageContent.MessageCall, TgCallMessage>();

        // Карта для TdApi.MessageContent.MessageAnimation в TgAnimationMessage
        CreateMap<TdApi.MessageContent.MessageAnimation, TgAnimationMessage>();

        // Карта TdApi.MessageContent.MessageAudio в TgAudioMessage
        CreateMap<TdApi.MessageContent.MessageAudio, TgAudioMessage>();

        // Карта TdApi.MessageContent.MessageContact в TgContactMessage>
        CreateMap<TdApi.MessageContent.MessageContact, TgContactMessage>();

        // Карта TdApi.MessageContent.MessageDocument в TgDocumentMessage
        CreateMap<TdApi.MessageContent.MessageDocument, TgDocumentMessage>();

        // Карта для TdApi.MessageContent.MessageSticker в TgStickerMessage
        CreateMap<TdApi.MessageContent.MessageSticker, TgStickerMessage>();

        // Карта для TdApi.MessageContent.MessageLocation в TgLocationMessage
        CreateMap<TdApi.MessageContent.MessageLocation, TgLocationMessage>();

        // Карта для TdApi.MessageContent.MessagePhoto в TgPhotoMessage
        CreateMap<TdApi.MessageContent.MessagePhoto, TgPhotoMessage>();

        // Карта для TdApi.MessageContent.MessageText в TgTextMessage
        CreateMap<TdApi.MessageContent.MessageText, TgTextMessage>();

        // Карта для TdApi.MessageContent.MessageVideo в TgVideoMessage
        CreateMap<TdApi.MessageContent.MessageVideo, TgVideoMessage>();

        // Карта для TdApi.MessageContent.MessageVideoNote в TgVideoNoteMessage
        CreateMap<TdApi.MessageContent.MessageVideoNote, TgVideoNoteMessage>();

        // Карта для TdApi.MessageContent.MessageVoiceNote в TgVoiceNoteMessage
        CreateMap<TdApi.MessageContent.MessageVoiceNote, TgVoiceNoteMessage>();
        
        // Карта для TdApi.MessageContent.MessageChatAddMembers, в TgChatMessageAddMembers
        CreateMap<TdApi.MessageContent.MessageChatAddMembers, TgMessageAddMembers>();
        
        // Карта для TdApi.MessageContent.MessageChatDeleteMember в TgChatMessageDeleteMember
        CreateMap<TdApi.MessageContent.MessageChatDeleteMember, TgMessageDeleteMember>();
        
        // Карта для TdApi.MessageContent.MessageDice в TgMessageDice
        CreateMap<TdApi.MessageContent.MessageDice, TgDiceMessage>();
        
        // Карта для TdApi.MessageInteractionInfo в TgMessageInteractionInfo
        CreateMap<TdApi.MessageInteractionInfo, TgMessageInteractionInfo>();
        
        // Карта для TdApi.MessageReplyInfo в TgMessageReplyInfo
        CreateMap<TdApi.MessageReplyInfo, TgMessageReplyInfo>();
        
        // Карта для TdApi.MessageReactions в TgMessageReactions
        CreateMap<TdApi.MessageReactions, TgMessageReactions>();
        
        // Карта для TdApi.MessageReaction в TgMessageReaction
        CreateMap<TdApi.MessageReaction, TgMessageReaction>();

        // Карта для TdApi.MessageContent.MessageBasicGroupCreate в TgGroupChatCreateMessage
        CreateMap<TdApi.MessageContent.MessageBasicGroupChatCreate, TgGroupChatCreateMessage>();

        // Карта для TdApi.MessageContent.MessageChatSetTheme в TgChatSetThemeMessage
        CreateMap<TdApi.MessageContent.MessageChatSetTheme, TgChatSetThemeMessage>();
        
        // Карта для TdApi.MessageContent.MessageChatSetBackground в TgChatSetBackgroundMessage
        CreateMap<TdApi.MessageContent.MessageChatSetBackground, TgChatSetBackgroundMessage>();
        
        // Карта для TdApi.MessageContent.MessageChatJoinByLink в TgChatJoinByLinkMessage
        CreateMap<TdApi.MessageContent.MessageChatJoinByLink, TgChatJoinByLinkMessage>();
        
        // Карта для TdApi.MessageContent.MessageChatJoinByRequest в TgChatJoinByLinkMessage
        CreateMap<TdApi.MessageContent.MessageChatJoinByRequest, TgChatJoinByRequestMessage>();
        
        // Карта для TdApi.MessageContactRegistered в TgContactRegistredMessage
        CreateMap<TdApi.MessageContent.MessageContactRegistered, TgContactRegistredMessage>();
        
        // Карта для TdApi.MessageChatChangeTitle в TgChatChangeTitleMessage
        CreateMap<TdApi.MessageContent.MessageChatChangeTitle, TgChatChangeTitleMessage>();
        
        // Карта для TdApi.MessageChatChangePhoto в TgChatChangePhotoMessage
        CreateMap<TdApi.MessageContent.MessageChatChangePhoto, TgChatChangePhotoMessage>();
        
        // Карта для TdApi.MessageChatDeletePhoto в TgChatDeletePhotoMessage
        CreateMap<TdApi.MessageContent.MessageChatDeletePhoto, TgChatDeletePhotoMessage>();

        #endregion
    
        #region Reactions

        CreateMap<TdApi.AvailableReactions, TgMessageAvailableReactions>();

        CreateMap<TdApi.AddedReactions, TgMessageAddedReactions>();

        // Карта для TdApi.AddedReaction в TgAddedReaction
        CreateMap<TdApi.AddedReaction, TgAddedReaction>();

        // Карта для TdApi.AvailableReaction в TgAvailableReaction
        CreateMap<TdApi.AvailableReaction, TgAvailableReaction>();

        #endregion

        #region ForwardInfo

        CreateMap<TdApi.MessageForwardInfo, TgForwardInfo>();

        // Карты для TdApi.MessageOrigin в TgMessageOrigin
        // Включает производные типы
        CreateMap<TdApi.MessageOrigin.MessageOriginChannel, TgMessageOriginChannel>()
            .ForMember(f => f.AuthorSignature, opt => opt.MapFrom(s => s.AuthorSignature.NullIfEmpty()));
        CreateMap<TdApi.MessageOrigin.MessageOriginChat, TgMessageOriginChat>()
            .ForMember(f => f.AuthorSignature, opt => opt.MapFrom(s => s.AuthorSignature.NullIfEmpty()));
        CreateMap<TdApi.MessageOrigin.MessageOriginHiddenUser, TgMessageOriginHiddenUser>()
            .ForMember(f => f.SenderName, opt => opt.MapFrom(s => s.SenderName.NullIfEmpty()));
        CreateMap<TdApi.MessageOrigin.MessageOriginUser, TgMessageOriginUser>();
        CreateMap<TdApi.MessageOrigin, TgMessageOrigin>()
            .Include<TdApi.MessageOrigin.MessageOriginChannel, TgMessageOriginChannel>()
            .Include<TdApi.MessageOrigin.MessageOriginChat, TgMessageOriginChat>()
            .Include<TdApi.MessageOrigin.MessageOriginHiddenUser, TgMessageOriginHiddenUser>()
            .Include<TdApi.MessageOrigin.MessageOriginUser, TgMessageOriginUser>();

        // Карты для TdApi.MessageReplyTo в TgMessageReplyTo
        // Включает производные типы
        CreateMap<TdApi.MessageReplyTo.MessageReplyToMessage, TgMessageReplyToMessage>();
        CreateMap<TdApi.MessageReplyTo.MessageReplyToStory, TgMessageReplyToStory>();
        CreateMap<TdApi.MessageReplyTo, TgMessageReplyTo>()
            .Include<TdApi.MessageReplyTo.MessageReplyToMessage, TgMessageReplyToMessage>()
            .Include<TdApi.MessageReplyTo.MessageReplyToStory, TgMessageReplyToStory>();

        #endregion
    }
}