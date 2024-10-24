using MediatR;
using TgInstanceAgent.Application.Abstractions.Commands.TgChats;
using TgInstanceAgent.Application.Abstractions.InstancesService;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Functions.Chats;

namespace TgInstanceAgent.Application.Services.CommandsHandlers.TgChats;

/// <summary>
/// Обработчик команды изменения заголовка чата.
/// </summary>
/// <param name="instancesService"></param>
public class SetChatTitleCommandHandler(IInstancesService instancesService) : IRequestHandler<SetChatTitleCommand>
{
    /// <summary>
    /// Обрабатывает команду изменения заголовка чата.
    /// </summary>
    /// <param name="request">Команда изменения заголовка чата.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    public async Task Handle(SetChatTitleCommand request, CancellationToken cancellationToken)
    {
        // Запуск телеграм клиента инстанса.
        var client = await instancesService.StartClientAsync(request.InstanceId, cancellationToken);

        // Устанавливаем заголовок чата.
        await client.SetChatTitleAsync(new TgSetChatTitleRequest
        {
            // Идентификатор чата
            ChatId = request.ChatId,
            
            // Заголовок чата
            Title = request.Title
        }, cancellationToken);
    }
}