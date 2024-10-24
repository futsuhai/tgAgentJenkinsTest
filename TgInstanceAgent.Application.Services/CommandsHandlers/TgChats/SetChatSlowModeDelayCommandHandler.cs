using MediatR;
using TgInstanceAgent.Application.Abstractions.Commands.TgChats;
using TgInstanceAgent.Application.Abstractions.InstancesService;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Functions.Chats;

namespace TgInstanceAgent.Application.Services.CommandsHandlers.TgChats;

/// <summary>
/// Обработчик команды изменения задержки отправки сообщений в чате.
/// </summary>
/// <param name="instancesService"></param>
public class SetChatSlowModeDelayCommandHandler(IInstancesService instancesService) : IRequestHandler<SetChatSlowModeDelayCommand>
{
    /// <summary>
    /// Обрабатывает команду задержки отправки сообщений в чате.
    /// </summary>
    /// <param name="request">Команда изменения задержки отправки сообщений в чате.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    public async Task Handle(SetChatSlowModeDelayCommand request, CancellationToken cancellationToken)
    {
        // Запуск телеграм клиента инстанса.
        var client = await instancesService.StartClientAsync(request.InstanceId, cancellationToken);

        // Устанавливаем задержку отправки сообщений в чате.
        await client.SetChatSlowModeDelayAsync(new TgSetChatSlowModeDelayRequest
        {
            // Идентификатор чата
            ChatId = request.ChatId,
            
            // Задержка
            SlowModeDelay = request.SlowModeDelay
        }, cancellationToken);
    }
}