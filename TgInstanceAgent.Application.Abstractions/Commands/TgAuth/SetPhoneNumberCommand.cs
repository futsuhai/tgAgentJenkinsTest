using MediatR;
using TgInstanceAgent.Application.Abstractions.Interfaces;

namespace TgInstanceAgent.Application.Abstractions.Commands.TgAuth;

/// <summary>
/// Команда для установки номера телефона.
/// </summary>
public class SetPhoneNumberCommand : IWithInstanceId, IRequest
{
    /// <summary>
    /// Уникальный идентификатор инстанса.
    /// </summary>
    public required Guid InstanceId { get; init; }
    
    /// <summary>
    /// Номер телефона для установки.
    /// </summary>
    public required string PhoneNumber { get; init; }
}