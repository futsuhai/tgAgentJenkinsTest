using MediatR;
using TgInstanceAgent.Application.Abstractions.Commands.Instances;
using TgInstanceAgent.Application.Abstractions.CommandsStore;
using TgInstanceAgent.Application.Abstractions.InstancesService;

namespace TgInstanceAgent.Application.Services.CommandsHandlers.Instances;

/// <summary>
/// Обработчик команды для старта клиентского инстанса.
/// </summary>
/// <param name="instancesService">Сервис, используемый для запуска клиентских инстансов.</param>
/// <param name="commandsStore">Сервис для сохранения логов выполнения команды.</param>
public class StartInstanceCommandHandler(ICommandsStore commandsStore, IInstancesService instancesService)
    : IRequestHandler<StartInstanceCommand>
{
    /// <summary>
    /// Обработка команды старта клиентского инстанса.
    /// </summary>
    /// <param name="request">Команда, содержащая идентификатор инстанса для старта.</param>
    /// <param name="cancellationToken">Токен для отмены операции.</param>
    /// <returns>Задачу, представляющую асинхронную операцию.</returns>
    public async Task Handle(StartInstanceCommand request, CancellationToken cancellationToken)
    {
        // Запускаем экземпляр инстанса
        await instancesService.StartClientAsync(request.InstanceId, cancellationToken);
        
        // Сохраняем логи о выполнении команды
        await commandsStore.StoreCommand(request, cancellationToken);
    }
}