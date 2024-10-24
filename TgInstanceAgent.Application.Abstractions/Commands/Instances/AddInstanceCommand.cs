using MediatR;
using TgInstanceAgent.Application.Abstractions.Interfaces;

namespace TgInstanceAgent.Application.Abstractions.Commands.Instances;

/// <summary>
/// Команда для добавления инстанса.
/// </summary>
public class AddInstanceCommand : IWithInstanceId, IWithCommandId, IRequest
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
    /// Уникальный идентификатор пользователя.
    /// </summary>
    public required Guid UserId { get; init; }
    
    /// <summary>
    /// Время истечения действия инстанса в формате UTC.
    /// </summary>
    public required DateTime ExpirationTimeUtc { get; init; }
}