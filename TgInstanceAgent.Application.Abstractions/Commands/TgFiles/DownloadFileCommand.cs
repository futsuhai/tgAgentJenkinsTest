using MediatR;
using TgInstanceAgent.Application.Abstractions.Interfaces;

namespace TgInstanceAgent.Application.Abstractions.Commands.TgFiles;

/// <summary>
/// Команда на начало загрузки файла по Id
/// </summary>
public class DownloadFileCommand : IWithInstanceId, IRequest, IWithFileRequest
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