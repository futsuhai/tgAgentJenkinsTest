using MediatR;
using TgInstanceAgent.Application.Abstractions.Commands.TgChats;
using TgInstanceAgent.Application.Abstractions.InstancesService;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Functions.Chats;
using TgInstanceAgent.Application.Services.Extensions;

namespace TgInstanceAgent.Application.Services.CommandsHandlers.TgChats;

/// <summary>
/// Обработчик команды изменения отправителя сообщений в групповом чате.
/// </summary>
/// <param name="instancesService"></param>
public class SetChatMessageSenderCommandHandler(IInstancesService instancesService) : IRequestHandler<SetChatMessageSenderCommand>
{
    /// <summary>
    /// Обрабатывает команду изменения отправителя сообщений в групповом чате.
    /// </summary>
    /// <param name="request">Команда изменения отправителя сообщений в групповом чате.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    public async Task Handle(SetChatMessageSenderCommand request, CancellationToken cancellationToken)
    {
        // Запуск телеграм клиента инстанса.
        var client = await instancesService.StartClientAsync(request.InstanceId, cancellationToken);

        // Устанавливаем отправителя сообщений в групповом чате.
        await client.SetChatMessageSenderAsync(new TgSetChatMessageSenderRequest
        {
            // Идентификатор чата
            ChatId = request.ChatId,
            
            // Отправитель
            MessageSender = request.GetMessageSender()
            
        }, cancellationToken);
    }
}