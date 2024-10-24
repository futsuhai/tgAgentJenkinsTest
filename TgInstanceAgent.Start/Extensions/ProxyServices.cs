using AutoMapper;
using TgInstanceAgent.Application.Abstractions.Proxies.ProxyClient;
using TgInstanceAgent.Application.Abstractions.Proxies.ProxyPurchasingService;
using TgInstanceAgent.Application.Services.ProxyServices;
using TgInstanceAgent.Domain.Abstractions.Repositories;
using TgInstanceAgent.Infrastructure.Proxy.Client;
using TgInstanceAgent.Start.HostedServices;

namespace TgInstanceAgent.Start.Extensions;

///<summary>
/// Статический класс сервисов прокси.
///</summary>
public static class ProxyServices
{
    /// <summary>
    ///  Расширяющий метод для добавления сервисов экземпляров в коллекцию служб.
    /// </summary>
    /// <param name="services">Коллекция служб.</param>
    /// <param name="configuration">Конфигурация приложения.</param>
    /// <param name="environment">Данные окружения.</param>
    public static void AddProxyServices(this IServiceCollection services, IConfiguration configuration,
        IWebHostEnvironment environment)
    {
        // Получение секретный ключ
        var secretKey = configuration.GetRequiredValue<string>("SecretKeys:ProxySecretApiKey");

        // Получаем страну
        var country = configuration.GetRequiredValue<string>("Counties:Russia");

        // Получаем коэффициент
        var instancesCoefficient = configuration.GetRequiredValue<int>("Proxy6Data:InstancesCoefficient");

        // Регистрация синглтона для клиента прокси
        services.AddSingleton<IProxyClient>(provider =>
            new ProxyClient(provider.GetRequiredService<IMapper>(), secretKey));

        // Регистрация синглтона для сервиса покупки и продления прокси
        services.AddSingleton<IProxyPurchasingService>(provider => new ProxyPurchasingService(
            provider.GetRequiredService<IInstanceRepository>(),
            provider.GetRequiredService<ISystemProxyRepository>(),
            provider.GetRequiredService<ILogger<ProxyPurchasingService>>(),
            provider.GetRequiredService<IProxyClient>(), country, instancesCoefficient));

        // Если запуск происходит в продакшн
        if (environment.IsProduction())
        {
            // Регистрация Hosted сервиса для покупки и продления системных прокси
            services.AddHostedService<ProxyPurchasingHostedService>(provider => new ProxyPurchasingHostedService(
                provider.GetRequiredService<IProxyPurchasingService>(),
                provider.GetRequiredService<ILogger<ProxyPurchasingHostedService>>()));
        }
    }
}