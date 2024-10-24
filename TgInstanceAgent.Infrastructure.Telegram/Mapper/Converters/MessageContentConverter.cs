using AutoMapper;
using TdLib;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Objects.Messages;

namespace TgInstanceAgent.Infrastructure.Telegram.Mapper.Converters;

/// <summary>
/// Преобразует объект TdApi.MessageContent в TgMessageContent.
/// </summary>
public class MessageContentConverter: ITypeConverter<TdApi.MessageContent, TgMessageContent>
{
    /// <summary>
    /// Преобразует объект TdApi.MessageContent в объект TgMessageContent.
    /// </summary>
    /// <param name="source">Объект TdApi.MessageContent для преобразования.</param>
    /// <param name="destination">Целевой объект TgMessageContent.</param>
    /// <param name="context">Контекст преобразования.</param>
    /// <returns>Преобразованный объект TgMessageContent.</returns>
    public TgMessageContent Convert(TdApi.MessageContent source, TgMessageContent destination, ResolutionContext context)
    {
       // Возвращаем данные в зависимости от типа контента сообщения
        return source switch
        {
            // Сообщение с историей
            TdApi.MessageContent.MessageStory messageStory => context.Mapper.Map<TgStoryMessage>(messageStory),
            
            // Сообщение с анимацией
            TdApi.MessageContent.MessageAnimation messageAnimation => context.Mapper.Map<TgAnimationMessage>(messageAnimation),
            
            // Аудио сообщение
            TdApi.MessageContent.MessageAudio messageAudio => context.Mapper.Map<TgAudioMessage>(messageAudio),

            // Сообщение со звонком(аудио или видео)
            TdApi.MessageContent.MessageCall messageCall => context.Mapper.Map<TgCallMessage>(messageCall),

            // Обработка сообщения с игральной костью
            TdApi.MessageContent.MessageDice messageDice => context.Mapper.Map<TgDiceMessage>(messageDice),

            // Контакт
            TdApi.MessageContent.MessageContact messageContact => context.Mapper.Map<TgContactMessage>(messageContact),

            // Сообщение с документом
            TdApi.MessageContent.MessageDocument messageDocument => context.Mapper.Map<TgDocumentMessage>(messageDocument),

            // Сообщение со стикером
            TdApi.MessageContent.MessageSticker messageSticker => context.Mapper.Map<TgStickerMessage>(messageSticker),

            // Сообщение с геолокацией
            TdApi.MessageContent.MessageLocation messageLocation => context.Mapper.Map<TgLocationMessage>(messageLocation),

            // Сообщение с фото
            TdApi.MessageContent.MessagePhoto messagePhoto => context.Mapper.Map<TgPhotoMessage>(messagePhoto),

            // Текстовое сообщение
            TdApi.MessageContent.MessageText messageText => context.Mapper.Map<TgTextMessage>(messageText),

            // Сообщение с видео
            TdApi.MessageContent.MessageVideo messageVideo => context.Mapper.Map<TgVideoMessage>(messageVideo),

            // Видеосообщение
            TdApi.MessageContent.MessageVideoNote messageVideoNote => context.Mapper.Map<TgVideoNoteMessage>(messageVideoNote),

            // Голосовое сообщение
            TdApi.MessageContent.MessageVoiceNote messageVoiceNote => context.Mapper.Map<TgVoiceNoteMessage>(messageVoiceNote),
            
            // Сообщение о создании группового чата
            TdApi.MessageContent.MessageBasicGroupChatCreate messageBasicGroup => context.Mapper.Map<TgGroupChatCreateMessage>(messageBasicGroup),
            
            // Сообщение об изменении темы чата
            TdApi.MessageContent.MessageChatSetTheme messageChatSetTheme => context.Mapper.Map<TgChatSetThemeMessage>(messageChatSetTheme),
            
            // Сообщение об установке фона чата
            TdApi.MessageContent.MessageChatSetBackground messageChatSetBackground => context.Mapper.Map<TgChatSetBackgroundMessage>(messageChatSetBackground),
            
            // Сообщение о присоединении к чату по ссылке
            TdApi.MessageContent.MessageChatJoinByLink messageChatJoin => context.Mapper.Map<TgChatJoinByLinkMessage>(messageChatJoin),
            
            // Сообщение о присоединении к чату по приглашению
            TdApi.MessageContent.MessageChatJoinByRequest messageChatJoinByRequest => context.Mapper.Map<TgChatJoinByRequestMessage>(messageChatJoinByRequest),
            
            // Сообщение о регистрации контакта
            TdApi.MessageContent.MessageContactRegistered messageContactRegistered => context.Mapper.Map<TgContactRegistredMessage>(messageContactRegistered),
            
            // Сообщение о смене названия чата
            TdApi.MessageContent.MessageChatChangeTitle messageChatChangeTitle => context.Mapper.Map<TgChatChangeTitleMessage>(messageChatChangeTitle),
            
            // Сообщение о смене фото чата
            TdApi.MessageContent.MessageChatChangePhoto messageChatChangePhoto => context.Mapper.Map<TgChatChangePhotoMessage>(messageChatChangePhoto),
            
            // Сообщение об удалении фото чата
            TdApi.MessageContent.MessageChatDeletePhoto messageChatDeletePhoto => context.Mapper.Map<TgChatDeletePhotoMessage>(messageChatDeletePhoto),

            // Обработка общего случая, если тип сообщения неизвестен или не поддерживается
            _ => new TgUnsupportedMessage()
        };
    }
}