using TgInstanceAgent.Application.Abstractions.WebhookSender;

namespace TgInstanceAgent.Start.HostedServices;

///<summary>
/// Класс запуска сервиса отправки веб-хуков.
///</summary>
public class WebhookHostedService(IWebhookSender webhookSender) : IHostedService
{
    /// <summary>
    /// Метод StartAsync вызывается при запуске приложения.
    /// </summary>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Завершенное задание.</returns>
    public Task StartAsync(CancellationToken cancellationToken)
    {
        // Вызываем метод Start сервиса отправки веб-хуков.
        webhookSender.Start();
        return Task.CompletedTask;
    }

    /// <summary>
    /// Метод StopAsync вызывается при остановке приложения.
    /// </summary>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Завершенное задание.</returns>
    public Task StopAsync(CancellationToken cancellationToken)
    {
        // Вызываем метод Stop сервиса отправки веб-хуков.
        webhookSender.Stop();
        return Task.CompletedTask;
    }
}