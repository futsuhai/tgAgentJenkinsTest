using MediatR;
using TgInstanceAgent.Application.Abstractions.Interfaces;

namespace TgInstanceAgent.Application.Abstractions.Commands.TgProfile;

/// <summary>
/// Команда установки био текущему пользователю
/// </summary>
public class SetUserBioCommand : IWithInstanceId, IRequest
{
    /// <summary>
    /// Уникальный идентификатор инстанса.
    /// </summary>
    public required Guid InstanceId { get; init; }
    
    /// <summary>
    /// Строка био
    /// </summary>
    public required string? Bio { get; init; }
}