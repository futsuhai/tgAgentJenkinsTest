using MediatR;
using Microsoft.Extensions.Caching.Memory;
using TgInstanceAgent.Application.Abstractions.Commands.Instances;
using TgInstanceAgent.Application.Abstractions.CommandsStore;
using TgInstanceAgent.Application.Abstractions.Exceptions;
using TgInstanceAgent.Application.Services.Extensions;
using TgInstanceAgent.Domain.Abstractions.Repositories;

namespace TgInstanceAgent.Application.Services.CommandsHandlers.Instances;

/// <summary>
/// Обработчик команды для удаления сслыки
/// </summary>
/// <param name="instanceRepository">Репозиторий для работы с инстансами.</param>
/// <param name="cache">Кэш.</param>
/// <param name="commandsStore">Сервис для сохранения логов выполнения команды.</param>
public class RemoveWebhookUrlCommandHandler(ICommandsStore commandsStore, IInstanceRepository instanceRepository, IMemoryCache cache)
    : IRequestHandler<RemoveWebhookUrlCommand>
{
    /// <summary>
    /// Обработка команды для удаления сслыки
    /// </summary>
    /// <param name="request">Модель команды для удаления сслыки.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    public async Task Handle(RemoveWebhookUrlCommand request, CancellationToken cancellationToken)
    {
        // Получаем инстанс из кэша или репозитория
        var instance = await cache.GetInstanceAsync(request.InstanceId, instanceRepository, cancellationToken);

        // Если не удалось удалить URL
        if (!instance.RemoveWebhookUrl(request.Url))
        {
            // Вызываем исключение
            throw new WebhookUrlNotFoundException();
        }

        // Сохраняем изменения в репозитории
        await instanceRepository.UpdateAsync(instance);
        
        // Сохраняем логи о выполнении команды
        await commandsStore.StoreCommand(request, cancellationToken);
    }
}