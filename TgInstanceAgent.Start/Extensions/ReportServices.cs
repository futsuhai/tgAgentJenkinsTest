using TgInstanceAgent.Application.Abstractions.ReportService;
using TgInstanceAgent.Application.Services.ReportService;

namespace TgInstanceAgent.Start.Extensions;

///<summary>
/// Статический класс сервисов отчётов по сообщениям.
///</summary>
public static class ReportServices
{
    ///<summary>
    /// Расширяющий метод для добавления сервисов экземпляров в коллекцию служб.
    ///</summary>
    ///<param name="services">Коллекция служб.</param>
    ///<param name="configuration">Конфигурация приложения.</param>
    public static void AddReportServices(this IServiceCollection services, IConfiguration configuration)
    {
        // Регистрация синглтона для сервиса отчетов по сообщениям
        services.AddSingleton<IReportService, ReportService>();
    }
}