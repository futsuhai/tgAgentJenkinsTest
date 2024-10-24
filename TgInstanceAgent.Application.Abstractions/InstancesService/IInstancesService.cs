using TgInstanceAgent.Application.Abstractions.TelegramClient.Interfaces;

namespace TgInstanceAgent.Application.Abstractions.InstancesService;

/// <summary>
/// Интерфейс сервиса инстансов.
/// </summary>
public interface IInstancesService
{
    /// <summary>
    /// Запуск всех инстансов асинхронно.
    /// </summary>
    /// <param name="token">Токен отмены</param>
    Task StartAsync(CancellationToken token);

    /// <summary>
    /// Остановка всех инстансов асинхронно.
    /// </summary>
    /// <param name="token">Токен отмены</param>
    Task StopAsync(CancellationToken token);
    
    /// <summary>
    /// Запуск клиента асинхронно.
    /// </summary>
    /// <param name="instanceId">Идентификатор инстанса</param>
    /// <param name="token">Токен отмены операции</param>
    /// <returns>Интерфейс WhatsApp клиента</returns>
    Task<ITelegramClient> StartClientAsync(Guid instanceId, CancellationToken token);

    /// <summary>
    /// Остановка клиента асинхронно.
    /// </summary>
    /// <param name="instanceId">Идентификатор инстанса</param>
    /// <param name="token">Токен отмены операции</param>
    Task StopClientAsync(Guid instanceId, CancellationToken token);
}