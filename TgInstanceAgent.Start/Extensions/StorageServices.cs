using Microsoft.EntityFrameworkCore;
using TgInstanceAgent.Application.Abstractions.CommandsStore;
using TgInstanceAgent.Application.Abstractions.FileStorage;
using TgInstanceAgent.Domain.Abstractions.Repositories;
using TgInstanceAgent.Domain.Instances;
using TgInstanceAgent.Domain.Reports;
using TgInstanceAgent.Domain.SystemProxy;
using TgInstanceAgent.Infrastructure.CommandsStore.CommandsStore;
using TgInstanceAgent.Infrastructure.CommandsStore.Context;
using TgInstanceAgent.Infrastructure.Storage.Context;
using TgInstanceAgent.Infrastructure.Storage.Entities.Instances;
using TgInstanceAgent.Infrastructure.Storage.Entities.Reports;
using TgInstanceAgent.Infrastructure.Storage.Entities.SystemProxies;
using TgInstanceAgent.Infrastructure.Storage.Mappers.Abstractions;
using TgInstanceAgent.Infrastructure.Storage.Mappers.AggregateMappers;
using TgInstanceAgent.Infrastructure.Storage.Mappers.ModelMappers;
using TgInstanceAgent.Infrastructure.Storage.Repositories;
using TgInstanceAgent.Infrastructure.Storage.Services;

namespace TgInstanceAgent.Start.Extensions;

///<summary>
/// Статический класс сервисов хранилища.
///</summary>
public static class StorageServices
{
    ///<summary>
    /// Расширяющий метод для добавления сервисов хранилища в коллекцию служб.
    ///</summary>
    ///<param name="services">Коллекция служб.</param>
    ///<param name="configuration">Конфигурация приложения.</param>
    public static void AddStorageServices(this IServiceCollection services, IConfiguration configuration)
    {
        // Получение строки подключения из конфигурации
        var appConnectionString = configuration.GetRequiredValue<string>("ConnectionStrings:ApplicationDb");
        
        // Получение строки подключения из конфигурации
        var commandsConnectionString = configuration.GetRequiredValue<string>("ConnectionStrings:CommandsDb");

        // Получение пути к хранилищу из конфигурации
        var storage = configuration.GetRequiredValue<string>("FileStorage:Path");

        // Добавление контекста базы данных для команд
        services.AddDbContext<CommandsDbContext>(builder => builder.UseNpgsql(commandsConnectionString,
            opt => opt.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery)));

        // Добавление хранилища команд
        services.AddScoped<ICommandsStore, CommandStore>();
        
        // Добавление фабрики контекста базы данных
        services.AddDbContextFactory<ApplicationDbContext>(builder => builder.UseNpgsql(appConnectionString,
            opt => opt.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery)));
        
        // Регистрация синглтона для отображения объекта InstanceAggregate на его модель
        services.AddSingleton<IAggregateMapperUnit<InstanceAggregate, InstanceModel>, InstanceMapper>();

        // Регистрация синглтона для отображения модели InstanceModel на объект InstanceAggregate
        services.AddSingleton<IModelMapperUnit<InstanceModel, InstanceAggregate>, InstanceModelMapper>();
        
        // Регистрация синглтона для отображения объекта ReportAggregate на его модель
        services.AddSingleton<IAggregateMapperUnit<ReportAggregate, ReportModel>, ReportMapper>();

        // Регистрация синглтона для отображения модели ReportModel на объект ReportAggregate
        services.AddSingleton<IModelMapperUnit<ReportModel, ReportAggregate>, ReportModelMapper>();
        
        // Регистрация синглтона для отображения объекта InstanceAggregate на его модель
        services.AddSingleton<IAggregateMapperUnit<SystemProxyAggregate, SystemProxyModel>, SystemProxyMapper>();
        
        // Регистрация синглтона для отображения модели InstanceModel на объект InstanceAggregate
        services.AddSingleton<IModelMapperUnit<SystemProxyModel, SystemProxyAggregate>, SystemProxyModelMapper>();

        // Регистрация синглтона для репозитория экземпляров
        services.AddSingleton<IInstanceRepository, InstanceRepository>();
        
        // Регистрация синглтона для репозитория отчётов
        services.AddSingleton<IReportRepository, ReportRepository>();
        
        // Регистрация синглтона для репозитория системных прокси
        services.AddSingleton<ISystemProxyRepository, SystemProxyRepository>();

        // Регистрация синглтона для файлового хранилища
        services.AddSingleton<IFileStorage>(_ => new FileStorage(storage));
    }
}