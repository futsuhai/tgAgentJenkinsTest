using MediatR;
using TgInstanceAgent.Application.Abstractions.Commands.Instances;
using TgInstanceAgent.Application.Abstractions.CommandsStore;
using TgInstanceAgent.Application.Abstractions.InstancesService;

namespace TgInstanceAgent.Application.Services.CommandsHandlers.Instances;

/// <summary>
/// Обработчик команды для остановки клиентского инстанса.
/// </summary>
/// <param name="instancesService">Сервис, используемый для запуска клиентских инстансов.</param>
/// <param name="commandsStore">Сервис для сохранения логов выполнения команды.</param>
public class StopInstanceCommandHandler(ICommandsStore commandsStore, IInstancesService instancesService) : IRequestHandler<StopInstanceCommand>
{
    /// <summary>
    /// Обработка команды остановки клиентского инстанса.
    /// </summary>
    /// <param name="request">Команда, содержащая идентификатор инстанса для остановки.</param>
    /// <param name="cancellationToken">Токен для отмены операции.</param>
    /// <returns>Задачу, представляющую асинхронную операцию.</returns>
    public async Task Handle(StopInstanceCommand request, CancellationToken cancellationToken)
    {
        // Останавливаем экземпляр инстанса
        await instancesService.StopClientAsync(request.InstanceId, cancellationToken);
        
        // Сохраняем логи о выполнении команды
        await commandsStore.StoreCommand(request, cancellationToken);
    }
}