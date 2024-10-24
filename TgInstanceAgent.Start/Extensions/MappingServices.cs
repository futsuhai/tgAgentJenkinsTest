using AutoMapper;
using TgInstanceAgent.Infrastructure.Bus.Instances.Mapper;
using TgInstanceAgent.Infrastructure.Proxy.Mapper;
using TgInstanceAgent.Infrastructure.Telegram.Mapper;
using TgInstanceAgent.Infrastructure.Telegram.Mapper.Converters;
using TgInstanceAgent.Infrastructure.Web.Messages.Mapper;

namespace TgInstanceAgent.Start.Extensions;

///<summary>
/// Статический класс сервисов мапинга.
///</summary>
public static class MappingServices
{
    ///<summary>
    /// Расширяющий метод для добавления сервисов мапинга в коллекцию служб.
    ///</summary>
    ///<param name="services">Коллекция служб.</param>
    public static void AddMappingServices(this IServiceCollection services)
    {
        // Добавляем AutoMapper в сервисы
        services.AddAutoMapper(cfg =>
        {
            // Устанавливаем, что когда у исходного объекта коллекция null, то и у конечного объекта должен быть установлен null
            // По умолчанию у исходного объекта был бы пустой массив.
            cfg.AllowNullCollections = true;
            
            // Регистрируем карты для контроллеров
            cfg.AddMaps(typeof(MessageMapperProfile).Assembly);
            
            // Регистрируем карты для событий
            cfg.AddMaps(typeof(InstancesMapperProfile).Assembly);
            
            // Регистрируем карты для клиента Telegram
            cfg.AddMaps(typeof(TelegramMessagesMapperProfile).Assembly);
            
            // Регистрируем карты для клиента Proxy
            cfg.AddMaps(typeof(ProxyMapperProfile).Assembly);
        });
        
        // Добавляем конвертеры маппера в сервисы
        services.AddConverters();
    }

    private static void AddConverters(this IServiceCollection services)
    {
        var assembly = typeof(MessageContentConverter).Assembly;
        
        // Поиск всех типов, которые реализуют ITypeConverter<,>
        var typeConverterInterface = typeof(ITypeConverter<,>);
        
        var typeConverters = assembly.GetTypes()
            .Where(t => t is { IsClass: true, IsAbstract: false })
            .SelectMany(t => t.GetInterfaces(), (type, interfaceType) => new { type, interfaceType })
            .Where(x => x.interfaceType.IsGenericType && x.interfaceType.GetGenericTypeDefinition() == typeConverterInterface)
            .Select(x => x.type)
            .Distinct();

        // Регистрация найденных типов как синглтонов в контейнер DI
        foreach (var typeConverter in typeConverters) services.AddSingleton(typeConverter);
    }
    
}