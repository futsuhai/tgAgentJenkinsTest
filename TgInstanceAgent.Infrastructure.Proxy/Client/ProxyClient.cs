using AutoMapper;
using Newtonsoft.Json;
using TgInstanceAgent.Application.Abstractions.Proxies.DataModels;
using TgInstanceAgent.Application.Abstractions.Proxies.Exceptions;
using TgInstanceAgent.Application.Abstractions.Proxies.ProxyClient;
using TgInstanceAgent.Domain.SystemProxy.Enums;
using TgInstanceAgent.Infrastructure.Proxy.Converter;
using TgInstanceAgent.Infrastructure.Proxy.Objects;

namespace TgInstanceAgent.Infrastructure.Proxy.Client;

/// <summary>
/// Реализация интерфейса API proxy6
/// </summary>
public class ProxyClient(IMapper mapper, string apiKey) : IProxyClient
{
    /// <summary>
    /// Базовый URL API proxy6.net
    /// </summary>
    private const string BaseApiUrl = "https://proxy6.net/api";

    /// <summary>
    /// HttpClient для выполнения HTTP запросов
    /// </summary>
    private static readonly HttpClient HttpClient = new();

    /// <summary>
    /// Настройки конвертера
    /// </summary>
    private readonly JsonSerializerSettings _settings = new()
    {
        Converters = { new ProxyConverter() }
    };

    /// <inheritdoc/>
    /// <summary>
    /// Метод получает существующие прокси
    /// </summary>
    public async Task<IReadOnlyCollection<ProxyData>> GetExistingAsync(CancellationToken token = default)
    {
        // Формируем URI запроса для получения существующих прокси
        var requestUri = $"{BaseApiUrl}/{apiKey}/getproxy?nokey";
        var request = new HttpRequestMessage(HttpMethod.Get, new Uri(requestUri));

        // Отправляем запрос к API
        var response = await HttpClient.SendAsync(request, token);

        // Проверяем код состояния ответа для успешного выполнения запроса
        response.EnsureSuccessStatusCode();

        // Получаем содержимое ответа в виде строки
        var content = await response.Content.ReadAsStringAsync(token);

        // Десериализуем JSON-ответ в объект
        var deserializedObject = JsonConvert.DeserializeObject<GetProxyResponse>(content, _settings);

        // todo
        if (deserializedObject == null) throw new ProxyClientException(500, "Response is null");

        // Возвращаем прокси
        return mapper.Map<ProxyData[]>(deserializedObject.List);
    }

    /// <inheritdoc/>
    /// <summary>
    /// Метод получает список доступных стран прокси
    /// </summary>
    public async Task<IReadOnlyCollection<string>> GetAvailableCountriesAsync(IpVersion version,
        CancellationToken token = default)
    {
        // Создаем сообщение запроса
        var requestUri = $"{BaseApiUrl}/{apiKey}/getcountry?version={version}&nokey";
        var request = new HttpRequestMessage(HttpMethod.Get, new Uri(requestUri));

        // Отправляем запрос
        var response = await HttpClient.SendAsync(request, token);

        // Проверяем код состояния ответа
        response.EnsureSuccessStatusCode();

        // Получаем ответ
        var content = await response.Content.ReadAsStringAsync(token);

        // Десериализуем JSON-ответ в объект
        var deserializedObject = JsonConvert.DeserializeObject<AvailableCountriesResponse>(content, _settings);

        // todo
        if (deserializedObject == null) throw new ProxyClientException(500, "Response is null");

        // Возвращаем страны
        return deserializedObject.Countries;
    }

    /// <inheritdoc/>
    /// <summary>
    /// Метод получает количество доступных прокси к покупке в заданной стране
    /// </summary>
    public async Task<int> GetCountAvailableInCountryAsync(string country, IpVersion version,
        CancellationToken token = default)
    {
        // Создаем сообщение запроса
        var requestUri = $"{BaseApiUrl}/{apiKey}/getcount?country={country}&version={version}&nokey";
        var request = new HttpRequestMessage(HttpMethod.Get, new Uri(requestUri));

        // Отправляем запрос
        var response = await HttpClient.SendAsync(request, token);

        // Проверяем код состояния ответа
        response.EnsureSuccessStatusCode();

        // Получаем ответ
        var content = await response.Content.ReadAsStringAsync(token);

        // Десериализуем JSON-ответ в объект
        var deserializedObject = JsonConvert.DeserializeObject<GetCountResponse>(content, _settings);

        // todo
        if (deserializedObject == null) throw new ProxyClientException(500, "Response is null");

        // Возвращаем количество доступных прокси
        return deserializedObject.Count;
    }

    /// <inheritdoc/>
    /// <summary>
    /// Метод покупает прокси для указанной страны на заданный период
    /// </summary>
    public async Task<ProxyData[]> BuyProxyForCountryAsync(int count, string country, int days,
        ProxyType type, IpVersion version, CancellationToken token = default)
    {
        // Преобразуем перечисления в строковые и числовые значения
        var typeString = type.ToString().ToLower();
        if(type == ProxyType.Https) typeString = ProxyType.Http.ToString().ToLower();
        var versionNumber = (int)version;

        // Создаем сообщение запроса
        var requestUri =
            $"{BaseApiUrl}/{apiKey}/buy?count={count}&period={days}&country={country}&version={versionNumber}&type={typeString}&nokey";
        var request = new HttpRequestMessage(HttpMethod.Get, new Uri(requestUri));

        // Отправляем запрос
        var response = await HttpClient.SendAsync(request, token);

        // Проверяем код состояния ответа
        response.EnsureSuccessStatusCode();

        // Получаем ответ
        var content = await response.Content.ReadAsStringAsync(token);

        // Десериализуем JSON-ответ в объект
        var deserializedObject = JsonConvert.DeserializeObject<BuyProxyResponse>(content, _settings);

        // todo
        if (deserializedObject == null) throw new ProxyClientException(500, "Response is null");

        // Маппим
        var boughtProxies = mapper.Map<ProxyData[]>(deserializedObject.List);

        // Устанавливаем поле страна для обьектов купленных прокси
        foreach (var boughtProxy in boughtProxies)
        {
            boughtProxy.Country = deserializedObject.County;
        }
        
        // Возвращаем купленные прокси
        return boughtProxies;
    }

    /// <inheritdoc/>
    /// <summary>
    /// Метод продливает прокси
    /// </summary>
    public async Task<IReadOnlyCollection<ExtendResult>> ExtendAsync(IEnumerable<string> proxyIds, int countDays,
        CancellationToken token = default)
    {
        var requestUri = $"{BaseApiUrl}/{apiKey}/prolong?period={countDays}&ids={string.Join(',', proxyIds)}&nokey";
        var request = new HttpRequestMessage(HttpMethod.Get, new Uri(requestUri));

        // Отправляем запрос на продление прокси
        var response = await HttpClient.SendAsync(request, token);

        // Проверяем код состояния ответа
        response.EnsureSuccessStatusCode();

        // Получаем ответ
        var content = await response.Content.ReadAsStringAsync(token);

        // Десериализуем JSON-ответ в объект
        var deserializedObject = JsonConvert.DeserializeObject<ProlongProxyResponse>(content, _settings);

        // todo
        if (deserializedObject == null) throw new ProxyClientException(500, "Response is null");

        // Возвращаем продленные прокси
        return mapper.Map<ExtendResult[]>(deserializedObject.List);
    }
}