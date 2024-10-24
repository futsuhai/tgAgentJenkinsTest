using MediatR;
using TgInstanceAgent.Application.Abstractions.Commands.TgChats;
using TgInstanceAgent.Application.Abstractions.InstancesService;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Functions.Chats;

namespace TgInstanceAgent.Application.Services.CommandsHandlers.TgChats;

/// <summary>
/// Обработчик команды изменения разрешений в чате.
/// </summary>
/// <param name="instancesService"></param>
public class SetChatPermissionsCommandHandler(IInstancesService instancesService) : IRequestHandler<SetChatPermissionsCommand>
{
    /// <summary>
    /// Обрабатывает команду изменения разрешений в чате.
    /// </summary>
    /// <param name="request">Команда изменения разрешений в чате.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    public async Task Handle(SetChatPermissionsCommand request, CancellationToken cancellationToken)
    {
        // Запуск телеграм клиента инстанса.
        var client = await instancesService.StartClientAsync(request.InstanceId, cancellationToken);

        // Устанавливаем разрешения в чате.
        await client.SetChatPermissionsAsync(new TgSetChatPermissionsRequest
        {
            ChatId = request.ChatId,
            // Устанавливаем разрешения
            CanSendBasicMessages = request.CanSendBasicMessages,
            CanSendAudios = request.CanSendAudios,
            CanSendDocuments = request.CanSendDocuments,
            CanSendPhotos = request.CanSendPhotos,
            CanSendVideos = request.CanSendVideos,
            CanSendVideoNotes = request.CanSendVideoNotes,
            CanSendVoiceNotes = request.CanSendVoiceNotes,
            CanSendPolls = request.CanSendPolls,
            CanSendOtherMessages = request.CanSendOtherMessages,
            CanAddWebPagePreviews = request.CanAddWebPagePreviews,
            CanChangeInfo = request.CanChangeInfo,
            CanInviteUsers = request.CanInviteUsers,
            CanPinMessages = request.CanPinMessages,
            CanCreateTopics = request.CanCreateTopics,
        }, cancellationToken);
    }
}