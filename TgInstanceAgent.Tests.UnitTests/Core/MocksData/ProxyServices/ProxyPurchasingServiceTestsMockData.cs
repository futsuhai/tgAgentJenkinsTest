using TgInstanceAgent.Application.Abstractions.Proxies.DataModels;
using TgInstanceAgent.Domain.SystemProxy;
using TgInstanceAgent.Domain.SystemProxy.Enums;

namespace TgInstanceAgent.Tests.UnitTests.Core.MocksData.ProxyServices;

/// <summary>
/// Класс, содержащий моковые данные для тестирования сервиса покупки и продления прокси.
/// </summary>
public static class ProxyPurchasingServiceTestsMockData
{
    /// <summary>
    /// Мок для страны
    /// </summary>
    public const string CountryMock = "ru";

    /// <summary>
    /// Коэффициент количества инстансов
    /// </summary>
    public const int InstancesCoefficientMock = 3;

    /// <summary>
    /// Мок данных для истекающей прокси
    /// </summary>
    public static readonly SystemProxyAggregate ExpiringProxyMock = new(Guid.NewGuid())
    {
        IdInProviderSystem = "1",
        Ip = "127.0.0.1",
        Host = "example.com",
        Port = 8080,
        Login = "username",
        Password = "password",
        ExpirationTimeUtc = DateTime.UtcNow.AddDays(1),
        InstancesCount = 0,
        Type = ProxyType.Https,
        IpVersion = IpVersion.V6,
    };
    
    /// <summary>
    /// Мок данных для купленной прокси
    /// </summary>
    public static readonly ProxyData BoughtProxyMock = new()
    {
        Id = "1",
        Ip = "127.0.0.1",
        Host = "example.com",
        Port = 8080,
        Login = "username",
        Password = "password",
        Type = ProxyType.Https,
        Country = "ru",
        PurchaseTime = DateTime.UtcNow,
        ExpirationTime = DateTime.UtcNow.AddDays(3),
    };
    
    /// <summary>
    /// Мок данных для продленной прокси
    /// </summary>
    public static readonly ExtendResult ProlongedProxyMock = new()
    {
        Id = ExpiringProxyMock.IdInProviderSystem,
        ExpirationTime = DateTime.UtcNow.AddDays(4)
    };
}