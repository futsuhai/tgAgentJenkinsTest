namespace TgInstanceAgent.Application.Abstractions.TelegramClient.Exceptions;

/// <summary>
/// Представляет исключение, которое вызывается при необходимости указания пароля для входа в систему.
/// </summary>
public class PasswordNeededException(string? hint) : Exception("To log in, you must specify a password")
{
    /// <summary>
    /// Подсказка для ввода пароля.
    /// </summary>
    public string? Hint { get; } = hint;
}