namespace TgInstanceAgent.Application.Abstractions.DTOs.Files;

/// <summary>
/// Данные о загруженном файле
/// </summary>
public class FileDto
{
    /// <summary>
    /// Имя файла
    /// </summary>
    public required string Name { get; init; }
    
    /// <summary>
    /// Поток с файлом
    /// </summary>
    public required Stream FileStream { get; init; }
    
    ///
}