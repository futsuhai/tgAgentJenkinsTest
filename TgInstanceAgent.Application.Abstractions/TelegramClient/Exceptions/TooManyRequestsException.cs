namespace TgInstanceAgent.Application.Abstractions.TelegramClient.Exceptions;

/// <summary>
/// Представляет исключение, которое вызывается при временной блокировке аккаунта телеграм
/// </summary>
public class TooManyRequestsException(int code, string error, int? seconds) : ClientException(code, error)
{
    /// <summary>
    /// Время в секундах до скончания блокировки
    /// </summary>
    public int? Seconds { get; } = seconds;
}