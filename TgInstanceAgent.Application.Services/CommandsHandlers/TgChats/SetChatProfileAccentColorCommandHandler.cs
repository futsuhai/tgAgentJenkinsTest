using MediatR;
using TgInstanceAgent.Application.Abstractions.Commands.TgChats;
using TgInstanceAgent.Application.Abstractions.InstancesService;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Functions.Chats;

namespace TgInstanceAgent.Application.Services.CommandsHandlers.TgChats;

/// <summary>
/// Обработчик команды изменения цвета профиля группового чата.
/// </summary>
/// <param name="instancesService"></param>
public class SetChatProfileAccentColorCommandHandler(IInstancesService instancesService) : IRequestHandler<SetChatProfileAccentColorCommand>
{
    /// <summary>
    /// Обрабатывает команду изменения цвета профиля группового чата.
    /// </summary>
    /// <param name="request">Команда изменения цвета профиля группового чата.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    public async Task Handle(SetChatProfileAccentColorCommand request, CancellationToken cancellationToken)
    {
        // Запуск телеграм клиента инстанса.
        var client = await instancesService.StartClientAsync(request.InstanceId, cancellationToken);

        // Устанавливаем цвет профиля группового чата.
        await client.SetChatProfileAccentColorAsync(new TgSetChatProfileAccentColorRequest
        {
            ChatId = request.ChatId,
            ProfileAccentColorId = request.ProfileAccentColorId,
            ProfileBackgroundCustomEmojiId = request.ProfileBackgroundCustomEmojiId
        }, cancellationToken);
    }
}