namespace TgInstanceAgent.Application.Abstractions.Exceptions;

/// <summary>
/// Представляет исключение, которое вызывается, когда не удается найти инстанс.
/// </summary>
public class InstanceNotFoundException() : Exception("Instance not found");