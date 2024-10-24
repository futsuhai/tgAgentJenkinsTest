namespace TgInstanceAgent.Application.Abstractions.Proxies.Exceptions;

/// <summary>
/// Ошибка страны, неверно указана страна (страны указываются в формате iso2), либо отсутствует
/// </summary>
public class ProxyClientCountryException(int errorId, string message) : ProxyClientException(errorId, message);