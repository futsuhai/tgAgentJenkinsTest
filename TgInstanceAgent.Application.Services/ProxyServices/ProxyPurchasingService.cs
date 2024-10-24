using Microsoft.Extensions.Logging;
using TgInstanceAgent.Application.Abstractions.Proxies.ProxyClient;
using TgInstanceAgent.Application.Abstractions.Proxies.ProxyPurchasingService;
using TgInstanceAgent.Domain.Abstractions.Repositories;
using TgInstanceAgent.Domain.Instances;
using TgInstanceAgent.Domain.Instances.Enums;
using TgInstanceAgent.Domain.Instances.Specifications;
using TgInstanceAgent.Domain.Instances.Specifications.Visitor;
using TgInstanceAgent.Domain.Specifications;
using TgInstanceAgent.Domain.SystemProxy;
using TgInstanceAgent.Domain.SystemProxy.Enums;
using TgInstanceAgent.Domain.SystemProxy.Specifications;

namespace TgInstanceAgent.Application.Services.ProxyServices;

/// <summary>
/// Сервис для работы с покупкой и продлением прокси
/// </summary>
public class ProxyPurchasingService(
    IInstanceRepository instanceRepository,
    ISystemProxyRepository systemProxyRepository,
    ILogger<IProxyPurchasingService> logger,
    IProxyClient proxyClient,
    string country,
    int instancesCoefficient) : IProxyPurchasingService
{
    /// <inheritdoc/>
    /// <summary>
    /// Продлевает прокси если это требуется
    /// </summary>
    public async Task PurchaseProxyIfNeededAsync(CancellationToken token = default)
    {
        var neededInstancesCount = await GetCountInstancesAsync();

        // Получаем количество системных прокси, которые будут живы ещё минимум 1 сутки
        var availableSystemProxiesCount = 
            await systemProxyRepository.CountAsync(new SystemProxyAvailableSpecification());
        
        // Рассчитываем количество прокси для покупки
        var needToBuy = await CalculateNeedToBuyAsync(neededInstancesCount, availableSystemProxiesCount);

        // Получаем прокси, время жизни которых менее 1 суток.
        var expiringSystemProxies =
            await systemProxyRepository.FindAsync(new ExpiringSystemProxySpecification(), null, 0, needToBuy);

        if (expiringSystemProxies.Count != 0)
        {
            // Идентификаторы истекающих прокси
            var ids = expiringSystemProxies.Select(p => p.IdInProviderSystem);

            // Продлеваем прокси
            var prolongResult = await proxyClient.ExtendAsync(ids, 3, token);

            // Обновляем продлённые прокси в репозитории
            foreach (var result in prolongResult)
            {
                // Находим агрегат продленной прокси
                var prolongedProxy =
                    expiringSystemProxies.First(p => p.IdInProviderSystem == result.Id);

                // Обновляем время истечения у агрегата
                prolongedProxy.ExpirationTimeUtc = result.ExpirationTime;

                // Обновляем агрегат в репозитории
                await systemProxyRepository.UpdateAsync(prolongedProxy);
            }

            // Считаем сколько осталось купить прокси
            needToBuy -= expiringSystemProxies.Count;
        }

        // Если не нужно докупать возвращаемся
        if (needToBuy == 0) return;

        // Получаем количество доступных прокси для покупки в заданной стране
        var availableCount = await proxyClient.GetCountAvailableInCountryAsync(country, IpVersion.V6, token);
        
        // Формируем количество прокси для докупки, учитывая сколько необходимо и сколько доступно у провайдера прокси
        var toBuy = Math.Min(needToBuy, availableCount);

        // Докупаем прокси
        var boughtProxies =
            await proxyClient.BuyProxyForCountryAsync(toBuy, country, 3, ProxyType.Https, IpVersion.V6, token);

        // Добавляем купленные прокси в репозиторий
        foreach (var proxy in boughtProxies)
        {
            // Сохраняем прокси
            await systemProxyRepository.AddAsync(new SystemProxyAggregate(Guid.NewGuid())
            {
                IdInProviderSystem = proxy.Id,
                Ip = proxy.Ip,
                Host = proxy.Host,
                Port = proxy.Port,
                Login = proxy.Login,
                Password = proxy.Password,
                Type = proxy.Type,
                IpVersion = IpVersion.V6,
                ExpirationTimeUtc = proxy.ExpirationTime,
                InstancesCount = 0
            }, token);
        }

        // Логгируем сколько прокси не смогли докупить
        if (needToBuy > toBuy) logger.LogWarning("Не удалось докупить {c} прокси", availableCount - needToBuy);
    }

    /// <summary>
    /// Удаляет истекшие прокси
    /// </summary>
    public async Task DeleteExpiredProxy(CancellationToken token = default)
    {
        // Спецификация истёккших прокси
        var expiredSpec = new ExpiredSystemProxiesSpecification();

        // Находим истёкшие прокси
        var expiredProxies = await systemProxyRepository.FindAsync(expiredSpec);

        // Удаляем истёкшие прокси
        foreach (var proxy in expiredProxies)
        {
            await systemProxyRepository.DeleteAsync(proxy.Id);
        }
    }

    /// <summary>
    /// Возвращает не истёкшие инстансы у которых не установлено прокси
    /// </summary>
    private Task<int> GetCountInstancesAsync()
    {
        var notExpiredSpec =
            new NotSpecification<InstanceAggregate, IInstanceSpecificationVisitor>(new ExpiredInstancesSpecification());
        var notWithProxySpec =
            new NotSpecification<InstanceAggregate, IInstanceSpecificationVisitor>(
                new InstancesWithOwnProxySpecification());
        return instanceRepository.CountAsync(
            new AndSpecification<InstanceAggregate, IInstanceSpecificationVisitor>(notExpiredSpec, notWithProxySpec));
    }

    /// <summary>
    /// Рассчитывает количество прокси для покупки
    /// </summary>
    private async Task<int> CalculateNeedToBuyAsync(int instancesCount, int availableProxiesCount)
    {
        // Получаем все прокси, время жизни которых менее 1 суток.
        var expiringSystemProxies = await systemProxyRepository.CountAsync(new ExpiringSystemProxySpecification());
        
        // Если количество инстансов и количество прокси в системе равны нулю, то нужно купить 1 прокси
        if (instancesCount == 0 && availableProxiesCount + expiringSystemProxies == 0) return 1;

        // Рассчитываем количество прокси для покупки
        var needToBuy = (int)Math.Round((double)instancesCount / instancesCoefficient,
            MidpointRounding.AwayFromZero) - availableProxiesCount;
        
        // Возвращаем needToBuy, если он больше 0, и 0, если он меньше 0
        return needToBuy > 0 ? needToBuy : 0;
    }
}