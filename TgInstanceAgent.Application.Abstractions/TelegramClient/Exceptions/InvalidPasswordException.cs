namespace TgInstanceAgent.Application.Abstractions.TelegramClient.Exceptions;

/// <summary>
/// Представляет исключение, которое вызывается при вводе неверного пароля.
/// </summary>
public class InvalidPasswordException(int code, string error) : ClientException(code, error);