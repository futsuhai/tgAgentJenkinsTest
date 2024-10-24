namespace TgInstanceAgent.Domain.Instances.Exceptions;

/// <summary>
/// Исключение для совпадающих пересылок сообщений.
/// </summary>
public class DuplicateForwardEntryExсeption() : Exception("The ForwardEntry already exists.");