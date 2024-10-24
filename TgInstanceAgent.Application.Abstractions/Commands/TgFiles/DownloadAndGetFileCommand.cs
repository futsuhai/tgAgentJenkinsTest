using MediatR;
using TgInstanceAgent.Application.Abstractions.DTOs.Files;
using TgInstanceAgent.Application.Abstractions.Interfaces;

namespace TgInstanceAgent.Application.Abstractions.Commands.TgFiles;

/// <summary>
/// Команда на загрузку и получение файла из сообщения
/// </summary>
public class DownloadAndGetFileCommand : IWithInstanceId, IRequest<FileDto>, IWithFileRequest
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