namespace TgInstanceAgent.Application.Abstractions.TelegramClient.Exceptions;

/// <summary>
/// Представляет исключение, которое вызывается при неожиданном типе файла
/// </summary>
public class UnexpectedFileTypeException(int code, string error) : ClientException(code, error)
{
    /// <summary>
    /// Полученный тип файла
    /// </summary>
    public required string FileType { get; init; }
    
    /// <summary>
    /// Ожидаемый тип файла
    /// </summary>
    public required string ExpectedType { get; init; }
}