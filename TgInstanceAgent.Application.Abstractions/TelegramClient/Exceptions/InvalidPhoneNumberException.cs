namespace TgInstanceAgent.Application.Abstractions.TelegramClient.Exceptions;

/// <summary>
/// Представляет исключение, которое вызывается при вводе неверного номера телефона.
/// </summary>
public class InvalidPhoneNumberException(int code, string error) : ClientException(code, error);