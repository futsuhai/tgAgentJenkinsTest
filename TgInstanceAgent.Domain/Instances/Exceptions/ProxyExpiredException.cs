namespace TgInstanceAgent.Domain.Instances.Exceptions;

/// <summary>
/// Исключение для истекших прокси-серверов.
/// </summary>
public class ProxyExpiredException() : Exception("The proxy has expired");