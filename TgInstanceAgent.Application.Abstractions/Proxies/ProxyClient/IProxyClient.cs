using TgInstanceAgent.Application.Abstractions.Proxies.DataModels;
using TgInstanceAgent.Domain.Instances.Enums;
using TgInstanceAgent.Domain.SystemProxy.Enums;

namespace TgInstanceAgent.Application.Abstractions.Proxies.ProxyClient;

/// <summary>
/// Интерфейс для взаимодействия с Proxy6 API
/// </summary>
public interface IProxyClient
{
    /// <summary>
    /// Асинхронно получает список существующих прокси
    /// </summary>
    /// <param name="token">Токен для отмены операции</param>
    /// <returns>Коллекция данных о прокси</returns>
    Task<IReadOnlyCollection<ProxyData>> GetExistingAsync(CancellationToken token = default);

    /// <summary>
    /// Асинхронно получает список доступных стран прокси
    /// </summary>
    /// <param name="token">Токен для отмены операции</param>
    /// <param name="version">Версия прокси (IPv4, IPv4Shared, IPv6)</param>
    /// <returns>Коллекция строк с названиями стран</returns>
    Task<IReadOnlyCollection<string>> GetAvailableCountriesAsync(IpVersion version, CancellationToken token = default);

    /// <summary>
    /// Асинхронно получает количество доступных прокси в указанной стране
    /// </summary>
    /// <param name="country">Название страны</param>
    /// <param name="token">Токен для отмены операции</param>
    /// <param name="version">Версия прокси (IPv4, IPv4Shared, IPv6)</param>
    /// <returns>Количество доступных прокси</returns>
    Task<int> GetCountAvailableInCountryAsync(string country, IpVersion version, CancellationToken token = default);

    /// <summary>
    /// Асинхронно покупает прокси для указанной страны на заданное количество дней
    /// </summary>
    /// <param name="country">Название страны</param>
    /// <param name="days">Количество дней</param>
    /// <param name="token">Токен для отмены операции</param>
    /// <param name="type">Тип прокси (http или socks)</param>
    /// <param name="version">Версия прокси (IPv4, IPv4Shared, IPv6)</param>
    /// <param name="count">Количество прокси</param>
    /// <returns>Данные о купленном прокси</returns>
    Task<ProxyData[]> BuyProxyForCountryAsync(int count, string country, int days, ProxyType type, IpVersion version,
        CancellationToken token = default);

    /// <summary>
    /// Асинхронно продлевает срок действия прокси
    /// </summary>
    /// <param name="proxyIds"></param>
    /// <param name="countDays">Количество дней</param>
    /// <param name="token">Токен для отмены операции</param>
    Task<IReadOnlyCollection<ExtendResult>> ExtendAsync(IEnumerable<string> proxyIds, int countDays, CancellationToken token = default);
}