using MediatR;
using TgInstanceAgent.Application.Abstractions.Commands.TgChats;
using TgInstanceAgent.Application.Abstractions.InstancesService;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Objects.Chats;
using TgInstanceAgent.Application.Services.Extensions;

namespace TgInstanceAgent.Application.Services.CommandsHandlers.TgChats;

/// <summary>
/// Обработчик команды открытия чата.
/// </summary>
public class OpenChatCommandHandler(IInstancesService instancesService) : IRequestHandler<OpenChatCommand, TgChat>
{
    /// <summary>
    /// Обрабатывает команду открытия чата.
    /// </summary>
    /// <param name="request">Команда открытия чата.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    public async Task<TgChat> Handle(OpenChatCommand request, CancellationToken cancellationToken)
    {
        // Запуск телеграм клиента инстанса.
        var client = await instancesService.StartClientAsync(request.InstanceId, cancellationToken);

        // Открываем чат.
        return await client.OpenChatAsync(request.GetChat(), cancellationToken);
    }
}