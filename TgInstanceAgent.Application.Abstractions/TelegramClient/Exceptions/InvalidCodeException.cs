namespace TgInstanceAgent.Application.Abstractions.TelegramClient.Exceptions;

/// <summary>
/// Представляет исключение, которое вызывается при вводе неверного кода подтверждения.
/// </summary>
public class InvalidCodeException(int code, string error) : ClientException(code, error);