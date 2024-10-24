namespace TgInstanceAgent.Application.Abstractions.Exceptions;

/// <summary>
/// Представляет исключение, которое вызывается, когда в системе отсутствует доступная системная прокси.
/// </summary>
public class NoAvailableSystemProxyException() : Exception("There is no available system proxy in the system");