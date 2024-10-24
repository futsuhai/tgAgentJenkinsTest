namespace TgInstanceAgent.Application.Abstractions.TelegramClient.Exceptions;

/// <summary>
/// Представляет исключение, которое вызывается, когда не удается найти сообщение.
/// </summary>
public class MessageNotFoundException(int code, string error) : ClientException(code, error);