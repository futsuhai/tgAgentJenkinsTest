using TgInstanceAgent.Application.Abstractions.Proxies.ProxyPurchasingService;

namespace TgInstanceAgent.Start.HostedServices;

/// <summary>
/// Класс для запуска сервиса продления и покупки системных прокси
/// </summary>
public class ProxyPurchasingHostedService(
    IProxyPurchasingService proxyPurchasingService,
    ILogger<ProxyPurchasingHostedService> logger)
    : IHostedService, IDisposable
{
    /// <summary>
    /// Таймер для вызова метода продления и покупки прокси
    /// </summary>
    private Timer? _timer;

    /// <summary>
    /// Метод, который запускается при старте приложения
    /// </summary>
    /// <param name="cancellationToken">Токен отмены</param>
    public Task StartAsync(CancellationToken cancellationToken)
    {
        // Создаем таймер, который вызывает метод PurchaseOrProlongProxy каждые 20 минут
        _timer = new Timer(PurchaseOrProlongProxy, null, TimeSpan.Zero, TimeSpan.FromMinutes(20));

        // Возвращаем завершенный Task
        return Task.CompletedTask;
    }

    /// <summary>
    /// Метод, который вызывается при остановке приложения
    /// </summary>
    /// <param name="cancellationToken">Токен отмены</param>
    public Task StopAsync(CancellationToken cancellationToken)
    {
        // Останавливаем таймер
        _timer?.Change(Timeout.Infinite, 0);

        // Возвращаем завершенный Task
        return Task.CompletedTask;
    }

    /// <summary>
    /// Метод, который вызывает метод продления и покупки прокси
    /// </summary>
    /// <param name="state"></param>
    private async void PurchaseOrProlongProxy(object? state)
    {
        try
        {
            // Вызываем метод продления и покупки прокси
            await proxyPurchasingService.PurchaseProxyIfNeededAsync();
        }
        catch (Exception ex)
        {
            //todo: указывать сообщение в зависимости от исключения
            // Логгируем исключение
            logger.LogError(ex, "Не удалось продлить прокси");
        }
    }

    /// <summary>
    /// Метод, который вызывается при освобождении ресурсов
    /// </summary>
    public void Dispose()
    {
        // Освобождаем ресурсы
        GC.SuppressFinalize(this);
    }
}