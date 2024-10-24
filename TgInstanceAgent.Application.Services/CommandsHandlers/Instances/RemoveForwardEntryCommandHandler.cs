using MediatR;
using Microsoft.Extensions.Caching.Memory;
using TgInstanceAgent.Application.Abstractions.Commands.Instances;
using TgInstanceAgent.Application.Abstractions.Exceptions;
using TgInstanceAgent.Application.Abstractions.CommandsStore;
using TgInstanceAgent.Application.Services.Extensions;
using TgInstanceAgent.Domain.Abstractions.Repositories;
using TgInstanceAgent.Domain.Instances.ValueObjects;

namespace TgInstanceAgent.Application.Services.CommandsHandlers.Instances;

/// <summary>
/// Обработчик команды для удаления новой пересылки сообщений.
/// </summary>
/// <param name="repository">Репозиторий для работы с инстансами.</param>
/// <param name="commandsStore">Сервис для сохранения логов выполнения команды.</param>
/// <param name="cache">Кэш.</param>
public class RemoveForwardEntryCommandHandler(ICommandsStore commandsStore, IInstanceRepository repository, IMemoryCache cache)
    : IRequestHandler<RemoveForwardEntryCommand>
{
    /// <summary>
    /// Обработка команды для удаления новой пересылки сообщений.
    /// </summary>
    /// <param name="request">Модель команды для удаления новой пересылки сообщений.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    public async Task Handle(RemoveForwardEntryCommand request, CancellationToken cancellationToken)
    {
        // Получаем инстанс из кэша/репозитория по указанному идентификатору асинхронно
        var instance = await cache.GetInstanceAsync(request.InstanceId, repository, cancellationToken);

        // Если не удалось удалить пересылку
        if (!instance.RemoveForwardEntry(new ForwardEntry(request.For, request.To, false)))
        {
            // Вызываем исключение
            throw new ForwardEntryNotFoundException();
        }
        
        // Сохраняем изменения в репозитории
        await repository.UpdateAsync(instance);
        
        // Сохраняем логи о выполнении команды
        await commandsStore.StoreCommand(request, cancellationToken);
    }
}