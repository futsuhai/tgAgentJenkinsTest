using MediatR;
using TgInstanceAgent.Application.Abstractions.Commands.TgChats;
using TgInstanceAgent.Application.Abstractions.InstancesService;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Functions.Chats;

namespace TgInstanceAgent.Application.Services.CommandsHandlers.TgChats;

/// <summary>
/// Обработчик команды изменения описания чата.
/// </summary>
/// <param name="instancesService"></param>
public class SetChatDescriptionCommandHandler(IInstancesService instancesService) : IRequestHandler<SetChatDescriptionCommand>
{
    /// <summary>
    /// Обрабатывает команду изменения описания чата.
    /// </summary>
    /// <param name="request">Команда изменения описания чата.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    public async Task Handle(SetChatDescriptionCommand request, CancellationToken cancellationToken)
    {
        // Запуск телеграм клиента инстанса.
        var client = await instancesService.StartClientAsync(request.InstanceId, cancellationToken);

        // Устанавливаем описание чата.
        await client.SetChatDescriptionAsync(new TgSetChatDescriptionRequest
        {
            // Идентификатор чата
            ChatId = request.ChatId,
            
            // Описание чата
            Description = request.Description
        }, cancellationToken);
    }
}