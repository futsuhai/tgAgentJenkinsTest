using MediatR;
using TgInstanceAgent.Application.Abstractions.Interfaces;

namespace TgInstanceAgent.Application.Abstractions.Commands.TgAuth;

/// <summary>
/// Команда для установки пароля.
/// </summary>
public class SetPasswordCommand : IWithInstanceId, IRequest
{
    /// <summary>
    /// Уникальный идентификатор инстанса.
    /// </summary>
    public required Guid InstanceId { get; init; }
    
    /// <summary>
    /// Пароль для установки.
    /// </summary>
    public required string Password { get; init; }
}