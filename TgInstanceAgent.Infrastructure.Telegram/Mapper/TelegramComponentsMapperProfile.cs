using AutoMapper;
using TdLib;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Objects.Components;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Objects.Messages;
using TgInstanceAgent.Infrastructure.Telegram.Extensions;
using TgInstanceAgent.Infrastructure.Telegram.Mapper.Converters;

namespace TgInstanceAgent.Infrastructure.Telegram.Mapper;

/// <summary>
/// Класс для маппинга объектов Telegram
/// </summary>
public class TelegramComponentsMapperProfile : Profile
{
    /// <summary>
    /// Конструктор профиля маппинга 
    /// </summary>
    public TelegramComponentsMapperProfile()
    {
        // Карты для TdApi.MessageSender в TgMessageSender
        // Включает производные типы
        CreateMap<TdApi.MessageSender.MessageSenderChat, TgMessageSenderChat>();
        CreateMap<TdApi.MessageSender.MessageSenderUser, TgMessageSenderUser>();
        CreateMap<TdApi.MessageSender, TgMessageSender>()
            .Include<TdApi.MessageSender.MessageSenderChat, TgMessageSenderChat>()
            .Include<TdApi.MessageSender.MessageSenderUser, TgMessageSenderUser>();


        // Карты для TdApi.ReactionType в TgReactionType
        // Включает производные типы
        CreateMap<TdApi.ReactionType.ReactionTypeEmoji, TgReactionTypeEmoji>();
        CreateMap<TdApi.ReactionType.ReactionTypeCustomEmoji, TgReactionTypeCustomEmoji>();
        CreateMap<TdApi.ReactionType, TgReactionType>()
            .Include<TdApi.ReactionType.ReactionTypeEmoji, TgReactionTypeEmoji>()
            .Include<TdApi.ReactionType.ReactionTypeCustomEmoji, TgReactionTypeCustomEmoji>();

        // Карта для TdApi.ChatPhoto в TgChatPhoto
        CreateMap<TdApi.ChatPhoto, TgChatPhoto>()
            .ForMember(p => p.MiniThumbnail, opt => opt.MapFrom(s => s.Minithumbnail));

        // Карта для TdApi.Video в TgVideo
        CreateMap<TdApi.Video, TgVideo>()
            .ForMember(a => a.File, opt => opt.MapFrom(s => s.Video_))
            .ForMember(v => v.MiniThumbnail, opt => opt.MapFrom(s => s.Minithumbnail));

        // Карта для TdApi.Photo в TgPhoto
        CreateMap<TdApi.Photo, TgPhoto>()
            .ForMember(p => p.MiniThumbnail, opt => opt.MapFrom(s => s.Minithumbnail));

        // Карта для TdApi.Animation в TgAnimation
        CreateMap<TdApi.Animation, TgAnimation>()
            .ForMember(a => a.File, opt => opt.MapFrom(s => s.Animation_));

        // Карта для TdApi.Audio в TgAudio
        CreateMap<TdApi.Audio, TgAudio>()
            .ForMember(a => a.File, opt => opt.MapFrom(s => s.Audio_))
            .ForMember(a => a.AlbumCoverMiniThumbnail, opt => opt.MapFrom(s => s.AlbumCoverMinithumbnail));

        // Карта TdApi.Sticker в TgSticker
        CreateMap<TdApi.Sticker, TgSticker>()
            .ForMember(d => d.File, opt => opt.MapFrom(s => s.Sticker_))
            .ForMember(d => d.Emoji, opt => opt.MapFrom(s => s.Emoji.NullIfEmpty()));

        // Карта для TdApi.VideoNote в TgVideoNote
        CreateMap<TdApi.VideoNote, TgVideoNote>()
            .ForMember(v => v.File, opt => opt.MapFrom(s => s.Video))
            .ForMember(v => v.MiniThumbnail, opt => opt.MapFrom(s => s.Minithumbnail));

        // Карта для TdApi.PhotoSize в TgPhotoSize
        CreateMap<TdApi.PhotoSize, TgPhotoSize>()
            .ForMember(p => p.File, opt => opt.MapFrom(s => s.Photo));

        // Карта для TdApi.StoryVideo в TgVideo
        CreateMap<TdApi.StoryVideo, TgVideo>()
            .ForMember(d => d.File, opt => opt.MapFrom(s => s.Video))
            .ForMember(v => v.MiniThumbnail, opt => opt.MapFrom(s => s.Minithumbnail));

        CreateMap<TdApi.Document, TgDocument>()
            .ForMember(d => d.File, opt => opt.MapFrom(s => s.Document_))
            .ForMember(d => d.MiniThumbnail, opt => opt.MapFrom(s => s.Minithumbnail));

        // Карта для TdApi.VoiceNote в TgVoiceNote
        CreateMap<TdApi.VoiceNote, TgVoiceNote>()
            .ForMember(v => v.File, opt => opt.MapFrom(s => s.Voice));

        // Карта TdApi.Contact в TgContact
        CreateMap<TdApi.Contact, TgContact>()
            .ForMember(c => c.FirstName, opt => opt.MapFrom(s => s.FirstName.NullIfEmpty()))
            .ForMember(c => c.LastName, opt => opt.MapFrom(s => s.LastName.NullIfEmpty()))
            .ForMember(c => c.VCard, opt => opt.MapFrom(s => s.Vcard.NullIfEmpty()));

        // Карта TdApi.ImportedContacts в TgImportedContacts
        CreateMap<TdApi.ImportedContacts, TgImportedContacts>();

        // Карта для TdApi.File в TgFile
        CreateMap<TdApi.File, TgFile>();
        
        // Карта для TdApi.LocalFile в TgLocalFile
        CreateMap<TdApi.LocalFile, TgLocalFile>();
        
        // Карта для TdApi.RemoteFile в TgRemoteFile
        CreateMap<TdApi.RemoteFile, TgRemoteFile>()
            .ForMember(c => c.Id, opt => opt.MapFrom(s => s.Id.NullIfEmpty()))
            .ForMember(c => c.UniqueId, opt => opt.MapFrom(s => s.UniqueId.NullIfEmpty()));

        // Карта для TdApi.FormattedText в TgFormatedText
        CreateMap<TdApi.FormattedText?, TgFormatedText?>().ConvertUsing<FormatedTextConverter>();

        // Карта для TdApi.Location в TgLocation
        CreateMap<TdApi.Location, TgLocation>();

        // Карта для TdApi.Thumbnail в TgThumbnail
        CreateMap<TdApi.Thumbnail, TgThumbnail>();

        // Карта для TdApi.Minithumbnail в TgMiniThumbnail
        CreateMap<TdApi.Minithumbnail, TgMiniThumbnail>();

        // Карта для TdApi.EmojiStatus в TgEmojiStatus
        CreateMap<TdApi.EmojiStatus, TgEmojiStatus>();

        // Карта для TdApi.AnimatedChatPhoto? в TgAnimatedChatPhoto?
        CreateMap<TdApi.AnimatedChatPhoto, TgAnimatedChatPhoto>();

        // Карта для Unix в DateTime?
        CreateMap<int, DateTime?>().ConvertUsing(s => s != 0 ? DateTime.UnixEpoch.AddSeconds(s) : null);

        // Карта для Unix в DateTime
        CreateMap<int, DateTime>().ConvertUsing(s => DateTime.UnixEpoch.AddSeconds(s));

        // Карта для int в int?
        CreateMap<int, int?>().ConvertUsing(s => s == 0 ? new int?() : s);
        
        // Карта для long в long?
        CreateMap<long, long?>().ConvertUsing(s => s == 0 ? new long?() : s);


        // Карты для заливок фона, включая производные типы
        CreateMap<TdApi.BackgroundFill.BackgroundFillFreeformGradient, TgBackgroundFillFreeformGradient>();
        CreateMap<TdApi.BackgroundFill.BackgroundFillGradient, TgBackgroundFillGradient>();
        CreateMap<TdApi.BackgroundFill.BackgroundFillSolid, TgBackgroundFillSolid>();
        CreateMap<TdApi.BackgroundFill, TgBackgroundFill>()
            .Include<TdApi.BackgroundFill.BackgroundFillFreeformGradient, TgBackgroundFillFreeformGradient>()
            .Include<TdApi.BackgroundFill.BackgroundFillGradient, TgBackgroundFillGradient>()
            .Include<TdApi.BackgroundFill.BackgroundFillSolid, TgBackgroundFillSolid>();

        // Карты для типов фона, включая производные типы
        CreateMap<TdApi.BackgroundType.BackgroundTypeChatTheme, TgBackGroundTypeChatTheme>();
        CreateMap<TdApi.BackgroundType.BackgroundTypeFill, TgBackgroundTypeFill>();
        CreateMap<TdApi.BackgroundType.BackgroundTypePattern, TgBackgroundTypePattern>();
        CreateMap<TdApi.BackgroundType.BackgroundTypeWallpaper, TgBackgroundTypeWallpaper>();
        CreateMap<TdApi.BackgroundType, TgBackgroundType>()
            .Include<TdApi.BackgroundType.BackgroundTypeChatTheme, TgBackGroundTypeChatTheme>()
            .Include<TdApi.BackgroundType.BackgroundTypeFill, TgBackgroundTypeFill>()
            .Include<TdApi.BackgroundType.BackgroundTypePattern, TgBackgroundTypePattern>()
            .Include<TdApi.BackgroundType.BackgroundTypeWallpaper, TgBackgroundTypeWallpaper>();
        
        // Карты для типов dice сообщения, включая производные типы
        CreateMap<TdApi.DiceStickers.DiceStickersSlotMachine, TgMessageDiceStickersSlotMachine>();
        CreateMap<TdApi.DiceStickers.DiceStickersRegular, TgMessageDiceStickersRegular>();
        CreateMap<TdApi.DiceStickers, TgMessageDiceStickers>()
            .Include<TdApi.DiceStickers.DiceStickersSlotMachine, TgMessageDiceStickersSlotMachine>()
            .Include<TdApi.DiceStickers.DiceStickersRegular, TgMessageDiceStickersRegular>();

        // Карта для фона
        CreateMap<TdApi.Background, TgBackground>();
    }
}