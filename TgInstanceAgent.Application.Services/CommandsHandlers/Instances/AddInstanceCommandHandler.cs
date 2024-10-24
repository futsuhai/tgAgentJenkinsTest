using MediatR;
using TgInstanceAgent.Application.Abstractions.Commands.Instances;
using TgInstanceAgent.Application.Abstractions.CommandsStore;
using TgInstanceAgent.Domain.Abstractions.Repositories;
using TgInstanceAgent.Domain.Instances;

namespace TgInstanceAgent.Application.Services.CommandsHandlers.Instances;

/// <summary>
/// Обработчик команды для создания нового инстанса.
/// </summary>
/// <param name="repository">Репозиторий, предоставляющий методы для работы с инстансами.</param>
/// <param name="commandsStore">Сервис для сохранения логов выполнения команды.</param>
public class AddInstanceCommandHandler(ICommandsStore commandsStore, IInstanceRepository repository)
    : IRequestHandler<AddInstanceCommand>
{
    /// <summary>
    /// Обрабатывает команду создания нового инстанса.
    /// </summary>
    /// <param name="request">Команда с параметрами для создания инстанса.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Задача, представляющая асинхронную операцию добавления инстанса.</returns>
    public async Task Handle(AddInstanceCommand request, CancellationToken cancellationToken)
    {
        // Создаем новый инстанс с параметрами из запроса.
        var instance = new InstanceAggregate(request.InstanceId, request.ExpirationTimeUtc, request.UserId);

        // Добавляем созданный инстанс в репозиторий асинхронно.
        await repository.AddAsync(instance, cancellationToken);
        
        // Сохраняем логи о выполнении команды
        await commandsStore.StoreCommand(request, cancellationToken);
    }
}