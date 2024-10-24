namespace TgInstanceAgent.Domain.Instances.Exceptions;

/// <summary>
/// Исключение для истекших инстансов.
/// </summary>
public class InstanceExpiredException() : Exception("The instance has expired");