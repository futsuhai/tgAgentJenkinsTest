using MediatR;
using Microsoft.Extensions.Caching.Memory;
using TgInstanceAgent.Application.Abstractions.Commands.Instances;
using TgInstanceAgent.Application.Abstractions.CommandsStore;
using TgInstanceAgent.Application.Services.Extensions;
using TgInstanceAgent.Domain.Abstractions.Repositories;
using TgInstanceAgent.Domain.Instances.ValueObjects;

namespace TgInstanceAgent.Application.Services.CommandsHandlers.Instances;

/// <summary>
/// Обработчик команды для добавления новой пересылки сообщений.
/// </summary>
/// <param name="commandsStore">Сервис для сохранения логов выполнения команды.</param>
/// <param name="repository">Репозиторий для работы с инстансами.</param>
/// <param name="cache">Кэш.</param>
public class AddForwardEntryCommandHandler(ICommandsStore commandsStore, IInstanceRepository repository, IMemoryCache cache)
    : IRequestHandler<AddForwardEntryCommand>
{
    /// <summary>
    /// Обработка команды для добавления новой пересылки сообщений.
    /// </summary>
    /// <param name="request">Модель команды для добавления новой пересылки сообщений.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    public async Task Handle(AddForwardEntryCommand request, CancellationToken cancellationToken)
    {
        // Получаем инстанс из кэша/репозитория по указанному идентификатору асинхронно
        var instance = await cache.GetInstanceAsync(request.InstanceId, repository, cancellationToken);

        // Добавляем пересылку сообщений в инстанс
        instance.AddForwardEntry(new ForwardEntry(request.For, request.To, request.SendCopy));

        // Сохраняем изменения в репозитории
        await repository.UpdateAsync(instance);
        
        // Сохраняем логи о выполнении команды
        await commandsStore.StoreCommand(request, cancellationToken);
    }
}