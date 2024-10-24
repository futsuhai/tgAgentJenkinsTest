using TgInstanceAgent.Application.Abstractions.TelegramClient.Events;

namespace TgInstanceAgent.Application.Abstractions.InstancesService;

/// <summary>
/// Интерфейс сервиса для публикации веб-хуков.
/// </summary>
public interface IWebhookService
{
    /// <summary>
    /// Публикация веб-хука для указанного инстанса и события если это необходимо.
    /// </summary>
    /// <param name="instanceId">Идентификатор инстанса.</param>
    /// <param name="event">Событие.</param>
    /// <param name="token">Токен отмены операции (по умолчанию null).</param>
    /// <returns>Задача, представляющая асинхронную операцию.</returns>
    Task PublishWebhookIfNeededAsync(Guid instanceId, TgEvent @event, CancellationToken token = default);
}