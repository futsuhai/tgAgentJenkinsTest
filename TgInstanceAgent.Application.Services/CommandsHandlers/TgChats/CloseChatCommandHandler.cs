using MediatR;
using TgInstanceAgent.Application.Abstractions.Commands.TgChats;
using TgInstanceAgent.Application.Abstractions.InstancesService;
using TgInstanceAgent.Application.Services.Extensions;

namespace TgInstanceAgent.Application.Services.CommandsHandlers.TgChats;

/// <summary>
/// Обработчик команды закрытия чата.
/// </summary>
public class CloseChatCommandHandler(IInstancesService instancesService) : IRequestHandler<CloseChatCommand>
{
    /// <summary>
    /// Обрабатывает команду закрытия чата.
    /// </summary>
    /// <param name="request">Команда закрытия чата.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    public async Task Handle(CloseChatCommand request, CancellationToken cancellationToken)
    {
        // Запуск телеграм клиента инстанса.
        var client = await instancesService.StartClientAsync(request.InstanceId, cancellationToken);

        // Закрываем чат.
        await client.CloseChatAsync(request.GetChat(), cancellationToken); 
    }
}