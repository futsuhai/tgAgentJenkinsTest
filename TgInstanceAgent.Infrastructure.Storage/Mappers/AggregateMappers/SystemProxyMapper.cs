using Microsoft.EntityFrameworkCore;
using TgInstanceAgent.Domain.SystemProxy;
using TgInstanceAgent.Infrastructure.Storage.Context;
using TgInstanceAgent.Infrastructure.Storage.Entities.SystemProxies;
using TgInstanceAgent.Infrastructure.Storage.Mappers.Abstractions;

namespace TgInstanceAgent.Infrastructure.Storage.Mappers.AggregateMappers;

/// <summary>
/// Класс для преобразования системных прокси
/// </summary>
public class SystemProxyMapper : IAggregateMapperUnit<SystemProxyAggregate, SystemProxyModel>
{
    /// <summary>
    /// Проецирует SystemProxyModel в SystemProxyAggregate
    /// </summary>
    /// <param name="model">Модель инстанса</param>
    /// <param name="context">Контекст EF</param>
    /// <returns>Аггрегат</returns>
    public async Task<SystemProxyAggregate> MapAsync(SystemProxyModel model, ApplicationDbContext context)
    {
        // Маппим SystemProxyModel в SystemProxyAggregate и возвращаем
        return new SystemProxyAggregate(model.Id)
        {
            // Идентификатор прокси во внешней системе
            IdInProviderSystem = model.IdInProviderSystem,
            
            // Ip
            Ip = model.Ip,
            
            // Устанавливаем хост прокси.
            Host = model.Host,
            
            // Устанавливаем порт прокси.
            Port = model.Port,
            
            // Число инстансов
            InstancesCount = await context.Instances.CountAsync(i=> i.SystemProxyId == model.Id),
            
            // Устанавливаем время истечения срока действия в формате UTC.
            ExpirationTimeUtc = model.ExpirationTimeUtc,
            
            // Устанавливаем логин для аутентификации на прокси.
            Login = model.Login,
            
            // Устанавливаем пароль для аутентификации на прокси.
            Password = model.Password,
            
            // Устанавливаем версию системного прокси
            IpVersion = model.IpVersion,
            
            // Устанавливаем тип системного прокси
            Type = model.Type,
        };
    }
}