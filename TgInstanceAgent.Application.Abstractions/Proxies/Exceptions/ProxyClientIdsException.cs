namespace TgInstanceAgent.Application.Abstractions.Proxies.Exceptions;

/// <summary>
/// Ошибка списка номеров прокси. Номера прокси должны быть указаны через запятую
/// </summary>
public class ProxyClientIdsException(int errorId, string message) : ProxyClientException(errorId, message);