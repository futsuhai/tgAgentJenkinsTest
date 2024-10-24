using AutoMapper;
using Microsoft.Extensions.Caching.Memory;
using TgInstanceAgent.Application.Abstractions.InstancesService;
using TgInstanceAgent.Application.Abstractions.ReportService;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Interfaces;
using TgInstanceAgent.Application.Services.InstancesService;
using TgInstanceAgent.Domain.Abstractions.Repositories;
using TgInstanceAgent.Infrastructure.Telegram.Client;
using TgInstanceAgent.Infrastructure.Telegram.Structs;
using TgInstanceAgent.Start.HostedServices;

namespace TgInstanceAgent.Start.Extensions;

///<summary>
/// Статический класс сервисов экземпляров.
///</summary>
public static class InstancesServices
{
    /// <summary>
    ///  Расширяющий метод для добавления сервисов экземпляров в коллекцию служб.
    /// </summary>
    /// <param name="services">Коллекция служб.</param>
    /// <param name="configuration">Конфигурация приложения.</param>
    /// <param name="environment">Данные окружения.</param>
    public static void AddInstancesServices(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment environment)
    {
        // Создание экземпляра TelegramApp с полученными значениями из конфигурации
        var telegramApp = new TelegramApp
        {
            AppId = configuration.GetRequiredValue<long>("App:Id"),
            AppHash = configuration.GetRequiredValue<string>("App:Hash"),
            ApplicationVersion = "1.0.0",
            FilesLocation = configuration.GetRequiredValue<string>("App:PatchToFiles")
        };

        // Регистрация синглтона для сервиса работы с экземплярами
        services.AddSingleton<IInstancesService>(provider => new InstancesService(
            provider.GetRequiredService<IInstanceRepository>(),
            provider.GetRequiredService<ISystemProxyRepository>(),
            provider.GetRequiredService<IMemoryCache>(),
            provider.GetRequiredService<ITelegramClientFactory>(),
            provider.GetRequiredService<IWebhookService>(),
            provider.GetRequiredService<IAutoForwardService>(),
            provider.GetRequiredService<IReportService>(),
            provider.GetRequiredService<ILogger<InstancesService>>(),
            environment.IsProduction()));

        // Регистрация синглтона для сервиса автоматической переотправки сообщений
        services.AddSingleton<IAutoForwardService, AutoForwardService>();

        // Регистрация синглтона для сервиса отправки вебхуков
        services.AddSingleton<IWebhookService, WebhookService>();

        // Регистрация синглтона для фабрики клиента Telegram с использованием полученных зависимостей
        services.AddSingleton<ITelegramClientFactory>(sp =>
            new TelegramClientFactory(telegramApp, sp.GetRequiredService<IMapper>()));

        // Регистрация Hosted сервиса запуска инстансов.
        services.AddHostedService<InstancesHostedService>();
    }
}