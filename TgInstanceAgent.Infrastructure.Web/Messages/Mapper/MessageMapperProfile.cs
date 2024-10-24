using TgInstanceAgent.Application.Abstractions.Commands.TgMessages;
using TgInstanceAgent.Application.Abstractions.Queries.TgMessages;
using TgInstanceAgent.Infrastructure.Web.Components.Mapper;
using TgInstanceAgent.Infrastructure.Web.Messages.InputModels;

namespace TgInstanceAgent.Infrastructure.Web.Messages.Mapper;

/// <summary>
/// Класс для маппинга входных моделей в команды
/// </summary>
public class MessageMapperProfile : AutoMapper.Profile
{
    /// <summary>
    /// Маппинг входных моделей в команды
    /// </summary>
    public MessageMapperProfile()
    {
        // Карта для GetMessageReactionsInputModel в GetMessageAvailableReactionsQuery
        CreateMap<GetMessageReactionsInputModel, GetMessageAvailableReactionsQuery>().MapInstanceId();
        
        // Карта для GetMessageInputModel в GetMessageQuery
        CreateMap<GetMessageInputModel, GetMessageQuery>().MapInstanceId();
        
        // Карта для GetMessageInputModel в GetMessageQuery
        CreateMap<GetMessageInputModel, GetMessageQuery>().MapInstanceId();
        
        // Карта для GetMessageAddedReactionsInputModel в GetMessageAddedReactionsQuery
        CreateMap<GetMessageAddedReactionsInputModel, GetMessageAddedReactionsQuery>().MapInstanceId();
        
        // Карта для AddMessageReactionInputModel в AddMessageReactionCommand
        CreateMap<AddMessageReactionInputModel, AddMessageReactionCommand>().MapInstanceId();

        // Карта для DeleteMessagesInputModel в DeleteMessagesCommand
        CreateMap<DeleteMessagesInputModel, DeleteMessagesCommand>().MapInstanceId();

        // Карта для ForwardMessagesInputModel в ForwardMessagesCommand
        CreateMap<ForwardMessagesInputModel, ForwardMessagesCommand>().MapInstanceId();

        // Карта для RemoveMessageReactionInputModel в RemoveMessageReactionCommand
        CreateMap<RemoveMessageReactionInputModel, RemoveMessageReactionCommand>().MapInstanceId();

        // Карта для SendAnimationMessageInputModel в SendAnimationMessageCommand
        CreateMap<SendAnimationMessageInputModel, SendAnimationMessageCommand>().MapInstanceId();

        // Карта для SendAudioMessageInputModel в SendAudioMessageCommand
        CreateMap<SendAudioMessageInputModel, SendAudioMessageCommand>()
            
            .MapInstanceId();

        // Карта для SendContactMessageInputModel в SendContactMessageCommand
        CreateMap<SendContactMessageInputModel, SendContactMessageCommand>().MapInstanceId();

        // Карта для SendDocumentMessageInputModel в SendDocumentMessageCommand
        CreateMap<SendDocumentMessageInputModel, SendDocumentMessageCommand>().MapInstanceId();

        // Карта для SendLocationMessageInputModel в SendLocationMessageCommand
        CreateMap<SendLocationMessageInputModel, SendLocationMessageCommand>().MapInstanceId();

        // Карта для SendPhotoMessageInputModel в SendPhotoMessageCommand
        CreateMap<SendPhotoMessageInputModel, SendPhotoMessageCommand>().MapInstanceId();

        // Карта для SendStickerMessageInputModel в SendStickerMessageCommand
        CreateMap<SendStickerMessageInputModel, SendStickerMessageCommand>().MapInstanceId();
        
        // Карта для SendStoryMessageInputModel в SendStoryMessageCommand
        CreateMap<SendStoryMessageInputModel, SendStoryMessageCommand>().MapInstanceId();

        // Карта для SendTextMessageInputModel в SendTextMessageCommand
        CreateMap<SendTextMessageInputModel, SendTextMessageCommand>().MapInstanceId();
        
        // Карта для SendDiceMessageInputModel в SendDiceMessageCommand
        CreateMap<SendDiceMessageInputModel, SendDiceMessageCommand>().MapInstanceId();

        // Карта для SendVideoMessageInputModel в SendVideoMessageCommand
        CreateMap<SendVideoMessageInputModel, SendVideoMessageCommand>().MapInstanceId();

        // Карта для SendVideoNoteMessageInputModel в SendVideoNoteMessageCommand
        CreateMap<SendVideoNoteMessageInputModel, SendVideoNoteMessageCommand>().MapInstanceId();

        // Карта для SendVoiceNoteMessageInputModel в SendVoiceNoteMessageCommand
        CreateMap<SendVoiceNoteMessageInputModel, SendVoiceNoteMessageCommand>().MapInstanceId();
    }
}