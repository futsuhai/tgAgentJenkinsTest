using IntegrationEvents.Instances;
using MassTransit;
using RabbitMQ.Client;
using TgInstanceAgent.Infrastructure.Bus.Instances.Consumers;

namespace TgInstanceAgent.Start.Extensions;

/// <summary>
/// Класс для настройки RabbitMQ эндпоинтов для обработки событий инстансов.
/// </summary>
public static class InstanceReceiveEndpoints
{
    /// <summary>
    /// Добавляет RabbitMQ эндпоинты для получения событий инстансов.
    /// </summary>
    /// <param name="cfg">Конфигуратор RabbitMQ для настроек шины сообщений.</param>
    /// <param name="context">Контекст регистрации обработчиков MassTransit.</param>
    /// <param name="serverId">Идентификатор сервера, используется для роутинга сообщений.</param>
    public static void AddInstanceReceiveEndpoints(this IRabbitMqBusFactoryConfigurator cfg,
        IBusRegistrationContext context, string serverId)
    {
        // Конфигурируем эндпоинты для событий комнат с фильмом
        cfg.ReceiveEndpoint($"InstanceCreated_{serverId}", re =>
        {
            // Отключаем конфигурацию по умолчанию, так как мы конфигурируем обменник вручную ниже в методе Bind
            re.ConfigureConsumeTopology = false;
            
            // Настраиваем потребителя для обработки событий InstanceCreated
            re.ConfigureConsumer<InstanceCreatedConsumer>(context);
            
            // Настраиваем привязку к обменнику с типом Direct для InstanceCreatedIntegrationEvent
            re.Bind<InstanceCreatedIntegrationEvent>(configurator =>
            {
                // Указываем тип обменника
                configurator.ExchangeType = ExchangeType.Direct;
                
                // Используем идентификатор сервера в качестве ключа маршрутизации
                configurator.RoutingKey = serverId;
            });
        });
    }
}