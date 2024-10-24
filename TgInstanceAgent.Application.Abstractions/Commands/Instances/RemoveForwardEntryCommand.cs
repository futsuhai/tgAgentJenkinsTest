using MediatR;
using TgInstanceAgent.Application.Abstractions.Interfaces;

namespace TgInstanceAgent.Application.Abstractions.Commands.Instances;

/// <summary>
/// Команда для удаления пересылки сообщений
/// </summary>
public class RemoveForwardEntryCommand : IRequest, IWithCommandId, IWithInstanceId
{
    /// <summary>
    /// Уникальный идентификатор команды.
    /// </summary>
    public required Guid CommandId { get; init; }
    
    /// <summary>
    /// Уникальный идентификатор инстанса.
    /// </summary>
    public required Guid InstanceId { get; init; }
    
    /// <summary>
    /// Идентификатор чата, из которого будут пересылка сообщения.
    /// </summary>
    public required long For { get; init; }
    
    /// <summary>
    /// Идентификатор чата, в который будут пересылка сообщения.
    /// </summary>
    public required long To { get; init; }
    
    /// <summary>
    /// Флаг, определяющий переслать сообщение или отправить копию.
    /// </summary>
    public required bool SendCopy { get; set; }
}