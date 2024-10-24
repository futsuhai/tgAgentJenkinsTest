namespace TgInstanceAgent.Application.Abstractions.Exceptions;

/// <summary>
/// Представляет исключение, которое вызывается, когда инстанс не пренадлежит пользователю.
/// </summary>
public class InstanceNotBelongToUserException() : Exception("The instance does not belong to the user");