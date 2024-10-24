using MediatR;
using TgInstanceAgent.Application.Abstractions.DTOs.Files;
using TgInstanceAgent.Application.Abstractions.Interfaces;

namespace TgInstanceAgent.Application.Abstractions.Queries.TgFiles;

/// <summary>
/// Запрос на получение файла по Id файла.
/// Результат запроса - поток с файлом.
/// </summary>
public class GetFileQuery : IWithInstanceId, IRequest<FileDto>, IWithFileRequest
{
    /// <summary>
    /// Уникальный идентификатор инстанса.
    /// </summary>
    public required Guid InstanceId { get; init; }
    
    /// <summary>
    /// Локальный идентификатор файла
    /// </summary>
    public int? LocalId { get; init; }

    /// <summary>
    /// Удаленный идентификатор файла
    /// </summary>
    public string? RemoteId { get; init; }
    
    /// <summary>
    /// Данные сообщения для получения файла
    /// </summary>
    public FileFromMessageData? FileFromMessage { get; init; }
}