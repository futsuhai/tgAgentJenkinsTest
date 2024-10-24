using MediatR;
using TgInstanceAgent.Application.Abstractions.Interfaces;

namespace TgInstanceAgent.Application.Abstractions.Commands.TgAuth;

/// <summary>
/// Команда для установки кода.
/// </summary>
public class SetCodeCommand : IWithInstanceId, IRequest
{
    /// <summary>
    /// Уникальный идентификатор инстанса.
    /// </summary>
    public required Guid InstanceId { get; init; }
    
    /// <summary>
    /// Код для установки.
    /// </summary>
    public required string Code { get; init; }
}