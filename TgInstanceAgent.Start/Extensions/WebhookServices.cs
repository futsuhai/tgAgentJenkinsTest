using TgInstanceAgent.Application.Abstractions.WebhookSender;
using TgInstanceAgent.Infrastructure.Webhook.Service;
using TgInstanceAgent.Start.HostedServices;

namespace TgInstanceAgent.Start.Extensions;

///<summary>
/// Статический класс сервисов веб-хуков.
///</summary>
public static class WebhookServices
{
    ///<summary>
    /// Расширяющий метод для добавления сервиса отправки веб-хуков в коллекцию служб.
    ///</summary>
    ///<param name="services">Коллекция служб.</param>
    ///<param name="configuration">Конфигурация приложения.</param>
    public static void AddWebhookServices(this IServiceCollection services, IConfiguration configuration)
    {
        // Получение строки подключения rabbitMQ
        var rabbitConnectionString = configuration.GetRequiredValue<string>("ConnectionStrings:WebhookRabbitMq");
        
        // Регистрация синглтона для сервиса для отправки веб-хуков.
        services.AddSingleton<IWebhookSender>(_ => new WebhookSender(rabbitConnectionString));
        
        // Регистрация Hosted сервиса запуска отправки веб-хуков.
        services.AddHostedService<WebhookHostedService>();
    }
}