using MediatR;
using Microsoft.Extensions.Caching.Memory;
using TgInstanceAgent.Application.Abstractions.Commands.Instances;
using TgInstanceAgent.Application.Abstractions.CommandsStore;
using TgInstanceAgent.Application.Services.Extensions;
using TgInstanceAgent.Domain.Abstractions.Repositories;

namespace TgInstanceAgent.Application.Services.CommandsHandlers.Instances;

/// <summary>
/// Обработчик команды для добавления новой ссылки
/// </summary>
/// <param name="instanceRepository">Репозиторий для работы с инстансами.</param>
/// <param name="commandsStore">Сервис для сохранения логов выполнения команды.</param>
/// <param name="cache">Кэш.</param>
public class AddWebhookUrlCommandHandler(ICommandsStore commandsStore, IInstanceRepository instanceRepository, IMemoryCache cache)
    : IRequestHandler<AddWebhookUrlCommand>
{
    /// <summary>
    /// Обработка команды для добавления новой ссылки
    /// </summary>
    /// <param name="request">Модель команды для добавления новой ссылки.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    public async Task Handle(AddWebhookUrlCommand request, CancellationToken cancellationToken)
    {
        // Получаем инстанс из кэша или репозитория
        var instance = await cache.GetInstanceAsync(request.InstanceId, instanceRepository, cancellationToken);

        // Добавляем новую ссылку
        instance.AddWebhookUrl(request.Url);

        // Сохраняем изменения в репозитории
        await instanceRepository.UpdateAsync(instance);
        
        // Сохраняем логи о выполнении команды
        await commandsStore.StoreCommand(request, cancellationToken);
    }
}