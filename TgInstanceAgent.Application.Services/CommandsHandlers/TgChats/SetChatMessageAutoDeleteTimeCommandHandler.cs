using MediatR;
using TgInstanceAgent.Application.Abstractions.Commands.TgChats;
using TgInstanceAgent.Application.Abstractions.InstancesService;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Functions.Chats;
using TgInstanceAgent.Application.Services.Extensions;

namespace TgInstanceAgent.Application.Services.CommandsHandlers.TgChats;

/// <summary>
/// Обработчик команды установки времени автоудаления сообщений в чате.
/// </summary>
public class SetChatMessageAutoDeleteTimeCommandHandler(IInstancesService instancesService)
    : IRequestHandler<SetChatMessageAutoDeleteTimeCommand>
{
    /// <summary>
    /// Обрабатывает команду установки времени автоудаления сообщений в чате
    /// </summary>
    /// <param name="request">Команда установки времени автоудаления сообщений в чате.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    public async Task Handle(SetChatMessageAutoDeleteTimeCommand request, CancellationToken cancellationToken)
    {
        // Запуск телеграм клиента инстанса.
        var client = await instancesService.StartClientAsync(request.InstanceId, cancellationToken);

        // Устанавливаем время автоудаления сообщений
        await client.SetChatMessageAutoDeleteTimeAsync(new TgSetChatMessageAutoDeleteTimeRequest
        {
            // Чат
            Chat = request.GetChat(),
            
            // Время автоудаления
            MessageAutoDeleteTime = request.MessageAutoDeleteTime
        }, cancellationToken);
    }
}