using MediatR;
using Microsoft.Extensions.Caching.Memory;
using TgInstanceAgent.Application.Abstractions.Commands.Instances;
using TgInstanceAgent.Application.Abstractions.CommandsStore;
using TgInstanceAgent.Application.Services.Extensions;
using TgInstanceAgent.Domain.Abstractions.Repositories;
using TgInstanceAgent.Domain.Instances.ValueObjects;

namespace TgInstanceAgent.Application.Services.CommandsHandlers.Instances;

/// <summary>
/// Обработчик команды для 
/// </summary>
/// <param name="instanceRepository">Репозиторий для работы с инстансами.</param>
/// <param name="cache">Кэш.</param>
/// <param name="commandsStore">Сервис для сохранения логов выполнения команды.</param>
public class UpdateWebhookSettingCommandHandler(ICommandsStore commandsStore, IInstanceRepository instanceRepository, IMemoryCache cache)
    : IRequestHandler<UpdateWebhookSettingCommand>
{
    /// <summary>
    /// Обработка команды для 
    /// </summary>
    /// <param name="request">Модель команды .</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    public async Task Handle(UpdateWebhookSettingCommand request, CancellationToken cancellationToken)
    {
        // Получаем инстанс из кэша или репозитория
        var instance = await cache.GetInstanceAsync(request.InstanceId, instanceRepository, cancellationToken);

        // Обновляем настройки вебхука 
        instance.WebhookSetting = new WebhookSetting
        {
            Messages = request.Messages,
            Chats = request.Chats,
            Users = request.Users,
            Files = request.Files,
            Stories = request.Stories,
            Other = request.Other
        };

        // Сохраняем изменения в репозитории
        await instanceRepository.UpdateAsync(instance);
        
        // Сохраняем логи о выполнении команды
        await commandsStore.StoreCommand(request, cancellationToken);
    }
}