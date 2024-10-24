namespace TgInstanceAgent.Application.Abstractions.Proxies.Exceptions;

/// <summary>
/// Базовый класс исключений для прокси
/// </summary>
public class ProxyClientException(int errorId, string message) : Exception(message)
{
    /// <summary>
    /// Идентификатор ошибки
    /// </summary>
    public int ErrorId { get; } = errorId;
}