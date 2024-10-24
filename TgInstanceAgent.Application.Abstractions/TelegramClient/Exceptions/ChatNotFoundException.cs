namespace TgInstanceAgent.Application.Abstractions.TelegramClient.Exceptions;

/// <summary>
/// Представляет исключение, которое вызывается, когда не удается найти чат.
/// </summary>
public class ChatNotFoundException() : Exception("The chat could not be found by the input parameters");