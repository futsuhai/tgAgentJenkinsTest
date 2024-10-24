using TgInstanceAgent.Application.Abstractions.TelegramClient.Interfaces;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Objects.Messages;

namespace TgInstanceAgent.Application.Abstractions.InstancesService;

/// <summary>
/// Интерфейс сервиса автоматической пересылки сообщений.
/// </summary>
public interface IAutoForwardService
{
    /// <summary>
    /// Метод для автоматической пересылки сообщений.
    /// </summary>
    /// <param name="client">Ватсап клиент</param>
    /// <param name="instanceId">Идентификатор инстанса</param>
    /// <param name="message">Модель сообщения</param>
    /// <param name="token">Токен отмены</param>
    /// <returns>Новое сообщение, если прошлое было переслано</returns>
    Task<TgMessage?> ForwardIfNeededAsync(ITelegramClient client, Guid instanceId, TgMessage message, CancellationToken token = default);
}