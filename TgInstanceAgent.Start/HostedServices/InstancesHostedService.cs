using TgInstanceAgent.Application.Abstractions.InstancesService;

namespace TgInstanceAgent.Start.HostedServices;

///<summary>
/// Класс запуска сервиса инстансов.
///</summary>
public class InstancesHostedService(IInstancesService instancesService) : IHostedService
{
    /// <summary>
    /// Метод StartAsync вызывается при запуске приложения.
    /// Вызываем метод StartAsync сервиса инстансов.
    /// </summary>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Завершенное задание.</returns>
    public Task StartAsync(CancellationToken cancellationToken) => instancesService.StartAsync(cancellationToken);

    /// <summary>
    /// Метод StopAsync вызывается при остановке приложения.
    /// Вызываем метод StopAsync сервиса инстансов.
    /// </summary>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Завершенное задание.</returns>
    public Task StopAsync(CancellationToken cancellationToken) => instancesService.StopAsync(cancellationToken);
}