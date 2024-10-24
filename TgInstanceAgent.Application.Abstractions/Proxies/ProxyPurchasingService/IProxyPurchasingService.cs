namespace TgInstanceAgent.Application.Abstractions.Proxies.ProxyPurchasingService;

/// <summary>
/// Интерфейс для сервиса покупки прокси
/// </summary>
public interface IProxyPurchasingService
{
    /// <summary>
    /// Продлевает прокси если это требуется
    /// </summary>
    /// <param name="token">Токен для отмены операции</param>
    Task PurchaseProxyIfNeededAsync(CancellationToken token = default);

    /// <summary>
    /// Удаляет истекшие прокси
    /// </summary>
    Task DeleteExpiredProxy(CancellationToken token = default);
}