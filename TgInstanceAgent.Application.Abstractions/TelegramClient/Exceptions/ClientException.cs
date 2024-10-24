namespace TgInstanceAgent.Application.Abstractions.TelegramClient.Exceptions;

/// <summary>
/// Представляет исключение, которое возникает при ошибке выполнения запроса.
/// </summary>
public class ClientException(int code, string error) : Exception($"Request execution error: {error}")
{
    /// <summary>
    /// Код ошибки.
    /// </summary>
    public int Code { get; } = code;

    /// <summary>
    /// Текстовое описание ошибки.
    /// </summary>
    public string Error { get; } = error;
}
