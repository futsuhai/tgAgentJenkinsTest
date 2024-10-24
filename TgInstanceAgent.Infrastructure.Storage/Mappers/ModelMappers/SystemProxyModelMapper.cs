using Microsoft.EntityFrameworkCore;
using TgInstanceAgent.Domain.SystemProxy;
using TgInstanceAgent.Infrastructure.Storage.Context;
using TgInstanceAgent.Infrastructure.Storage.Entities.SystemProxies;
using TgInstanceAgent.Infrastructure.Storage.Mappers.Abstractions;

namespace TgInstanceAgent.Infrastructure.Storage.Mappers.ModelMappers;

/// <summary>
/// Класс для преобразования SystemProxyAggregate в SystemProxyModel
/// </summary>
public class SystemProxyModelMapper: IModelMapperUnit<SystemProxyModel, SystemProxyAggregate>
{
    /// <summary>
    /// Преобразуем InstanceAggregate в InstanceModel
    /// </summary>
    /// <param name="aggregate">Аггрегат InstanceAggregate</param>
    /// <param name="context">Контекст базы данных</param>
    /// <returns>Модель инстанса</returns>
    public async Task<SystemProxyModel> MapAsync(SystemProxyAggregate aggregate, ApplicationDbContext context)
    {
        // Получаем модель инстанса из базы данных
        var model = await context.SystemProxies
            .FirstOrDefaultAsync(x => x.Id == aggregate.Id);

        // Если модели null, то создаем новую модель, так как это новое системное прокси
        model ??= new SystemProxyModel { Id = aggregate.Id };

        // Идентификатор прокси во внешней системе
        model.IdInProviderSystem = aggregate.IdInProviderSystem;

        // Ip
        model.Ip = aggregate.Ip;
        
        // Устанавливаем хост прокси.
        model.Host = aggregate.Host;

        // Устанавливаем порт прокси.
        model.Port = aggregate.Port;

        // Устанавливаем логин для аутентификации на прокси.
        model.Login = aggregate.Login;

        // Устанавливаем пароль для аутентификации на прокси.
        model.Password = aggregate.Password;

        // Устанавливаем время истечения срока действия в формате UTC.
        model.ExpirationTimeUtc = aggregate.ExpirationTimeUtc;
        
        // Устанавливаем тип системного прокси
        model.Type = aggregate.Type;
        
        // Устанавливаем версию системного прокси
        model.IpVersion = aggregate.IpVersion;

        // Возвращаем модель.
        return model;
    }
}