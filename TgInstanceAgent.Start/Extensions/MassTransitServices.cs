using MassTransit;
using TgInstanceAgent.Infrastructure.Bus.Instances.Consumers;

namespace TgInstanceAgent.Start.Extensions;

/// <summary>
/// Статический класс для регистрации сервиса MassTransit в контейнере DI 
/// </summary>
public static class MassTransitServices
{
    /// <summary>
    /// Метод регистрирует сервис MassTransit в контейнере DI 
    /// </summary>
    /// <param name="services">Абстракция, которая представляет коллекцию сервисов (зависимостей),
    /// используемых в приложении.</param>
    /// <param name="configuration">Интерфейс, предоставляющий доступ к конфигурации приложения.</param>
    public static void AddMassTransitServices(this IServiceCollection services, IConfiguration configuration)
    {
        // Получаем строку подключения к RabbitMq
        var rmq = configuration.GetRequiredValue<string>("ConnectionStrings:RabbitMq");

        // Получаем идентификатор текущего сервера
        var serverId = configuration.GetRequiredValue<string>("Server:Id");

        // Конфигурируем MassTransit
        services.AddMassTransit(x =>
        {
            x.AddConsumer<InstanceCreatedConsumer>();

            x.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host(rmq);

                // Конфигурируем эндпоинты для событий комнат с фильмом
                cfg.AddInstanceReceiveEndpoints(context, serverId);
            });
        });
    }
}