using MediatR;
using TgInstanceAgent.Application.Abstractions.Commands.TgChats;
using TgInstanceAgent.Application.Abstractions.InstancesService;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Functions.Chats;
using TgInstanceAgent.Application.Services.Extensions;

namespace TgInstanceAgent.Application.Services.CommandsHandlers.TgChats;

/// <summary>
/// Обработчик команды изменения настроек уведомлений чата.
/// </summary>
/// <param name="instancesService"></param>
public class SetChatNotificationSettingsCommandHandler(IInstancesService instancesService) : IRequestHandler<SetChatNotificationSettingsCommand>
{
    /// <summary>
    /// Обрабатывает команду изменения настроек уведомлений чата.
    /// </summary>
    /// <param name="request">Команда изменения настроек уведомлений чата.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    public async Task Handle(SetChatNotificationSettingsCommand request, CancellationToken cancellationToken)
    {
        // Запуск телеграм клиента инстанса.
        var client = await instancesService.StartClientAsync(request.InstanceId, cancellationToken);

        // Изменяем настройки уведомлений чата.
        await client.SetChatNotificationSettingsAsync(new TgSetChatNotificationSettingsRequest
        {
            Chat = request.GetChat(),
            // Устанавливаем значения настроек уведомлений
            UseDefaultMuteFor = request.UseDefaultMuteFor,
            MuteFor = request.MuteFor,
            UseDefaultSound = request.UseDefaultSound,
            SoundId = request.SoundId,
            UseDefaultShowPreview = request.UseDefaultShowPreview,
            ShowPreview = request.ShowPreview,
            UseDefaultMuteStories = request.UseDefaultMuteStories,
            MuteStories = request.MuteStories,
            UseDefaultStorySound = request.UseDefaultStorySound,
            StorySoundId = request.StorySoundId,
            UseDefaultShowStorySender = request.UseDefaultShowStorySender,
            ShowStorySender = request.ShowStorySender,
            UseDefaultDisablePinnedMessageNotifications = request.UseDefaultDisablePinnedMessageNotifications,
            DisablePinnedMessageNotifications = request.DisablePinnedMessageNotifications,
            UseDefaultDisableMentionNotifications = request.UseDefaultDisableMentionNotifications,
            DisableMentionNotifications = request.DisableMentionNotifications
        }, cancellationToken);
    }
}