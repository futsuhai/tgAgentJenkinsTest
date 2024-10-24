namespace TgInstanceAgent.Application.Abstractions.InstancesService.Exceptions;

/// <summary>
/// Исключение "Сервис находится в процессе запуска".
/// </summary>
public class InstanceServiceNotReadyException() : Exception("The service is in the process of launching");