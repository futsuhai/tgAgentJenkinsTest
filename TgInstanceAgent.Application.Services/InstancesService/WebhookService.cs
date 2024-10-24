using Microsoft.Extensions.Caching.Memory;
using TgInstanceAgent.Application.Abstractions.InstancesService;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Events;
using TgInstanceAgent.Application.Abstractions.WebhookSender;
using TgInstanceAgent.Application.Services.Extensions;
using TgInstanceAgent.Domain.Abstractions.Repositories;

namespace TgInstanceAgent.Application.Services.InstancesService;

/// <summary>
/// Сервис отправки вебхуков
/// </summary>
/// <param name="memoryCache">Кэш.</param>
/// <param name="instanceRepository">Репозиторий инстансов.</param>
/// <param name="sender">Сервис непосредственной отправки вебхуков</param>
public class WebhookService(IMemoryCache memoryCache, IInstanceRepository instanceRepository, IWebhookSender sender)
    : IWebhookService
{
    /// <inheritdoc/>
    /// <summary>
    /// Публикация веб-хука для указанного экземпляра и события.
    /// </summary>
    public async Task PublishWebhookIfNeededAsync(Guid instanceId, TgEvent @event, CancellationToken token)
    {
        // Получение инстанса из кэша или репозитория
        var instance = await memoryCache.GetInstanceAsync(instanceId, instanceRepository, token);

        // Создание посетителя для посещения события
        var visitor = new EventWebhookVisitor(instance.WebhookSetting);
        
        // Посещаем событие посетителем
        @event.Accept(visitor);
        
        // Если посетитель говорит, что вебхук отправлять не надо (значит не включен данный тип вебхуков у инстансов) - ничего не делаем
        if (!visitor.NeedSend) return;
        
        // Отправка веб-хука на все указанные URL-адреса
        foreach (var url in instance.WebhookUrls)
        {
            // Добавляем вебхук в очередь
            sender.PushToQueue(@event, url, instanceId.ToString());
        }
    }
}