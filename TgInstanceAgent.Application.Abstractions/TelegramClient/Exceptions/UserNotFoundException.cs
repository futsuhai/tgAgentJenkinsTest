namespace TgInstanceAgent.Application.Abstractions.TelegramClient.Exceptions;

/// <summary>
/// Представляет исключение, которое вызывается, когда не удается найти чат.
/// </summary>
public class UserNotFoundException() : Exception("The user could not be found by the input parameters");