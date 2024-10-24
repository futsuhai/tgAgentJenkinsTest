using MediatR;
using TgInstanceAgent.Application.Abstractions.Interfaces;

namespace TgInstanceAgent.Application.Abstractions.Commands.Instances;

/// <summary>
/// Команда для проверки владения инстансом.
/// </summary>
public class CheckInstanceOwnershipCommand : IWithInstanceId, IRequest
{
    /// <summary>
    /// Уникальный идентификатор инстанса.
    /// </summary>
    public required Guid InstanceId { get; init; }
    
    /// <summary>
    /// Уникальный идентификатор пользователя.
    /// </summary>
    public required Guid UserId { get; init; }
}