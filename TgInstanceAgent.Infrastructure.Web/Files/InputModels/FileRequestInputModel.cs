namespace TgInstanceAgent.Infrastructure.Web.Files.InputModels;

/// <summary>
/// Модель входных данных для запроса с идентификатором файла.
/// </summary>
public class FileRequestInputModel
{
    /// <summary>
    /// Локальный идентификатор файла
    /// </summary>
    public int? LocalId { get; init; }
    
    /// <summary>
    /// Удаленный идентификатор файла
    /// </summary>
    public string? RemoteId { get; init; }
    
    /// <summary>
    /// Входная модель, описывающая получение файла из сообщения
    /// </summary>
    public FileFromMessageInputModel? FileFromMessage { get; init; }
}