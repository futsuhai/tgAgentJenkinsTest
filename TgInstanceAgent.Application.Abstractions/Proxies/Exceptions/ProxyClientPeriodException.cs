namespace TgInstanceAgent.Application.Abstractions.Proxies.Exceptions;

/// <summary>
/// Ошибка периода, неверно указано кол-во (дней), либо отсутствует
/// </summary>
public class ProxyClientPeriodException(int errorId, string message) : ProxyClientException(errorId, message);