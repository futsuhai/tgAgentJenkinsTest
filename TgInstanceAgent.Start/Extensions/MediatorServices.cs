using TgInstanceAgent.Application.Services.CommandsHandlers.Instances;

namespace TgInstanceAgent.Start.Extensions;

///<summary>
/// Статический класс сервисов Mediator.
///</summary>
public static class MediatorServices
{
    ///<summary>
    /// Расширяющий метод для добавления сервисов Mediator в коллекцию служб.
    ///</summary>
    ///<param name="services">Коллекция служб.</param>
    public static void AddMediatorServices(this IServiceCollection services)
    {
        // Регистрация сервисов MediatR и обработчиков команд
        services.AddMediatR(configuration =>
        {
            configuration.RegisterServicesFromAssembly(typeof(AddInstanceCommandHandler).Assembly);
        });
    }
}