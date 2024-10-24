namespace TgInstanceAgent.Application.Abstractions.Proxies.Exceptions;

/// <summary>
/// Ошибка кол-ва прокси, неверно указано кол-во, либо отсутствует
/// </summary>
public class ProxyClientCountException(int errorId, string message) : ProxyClientException(errorId, message);