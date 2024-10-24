using System.Threading.Channels;
using MediatR;
using TgInstanceAgent.Application.Abstractions.Interfaces;

namespace TgInstanceAgent.Application.Abstractions.Commands.TgAuth;

/// <summary>
/// Команда для аутентификации с помощью QR кода.
/// </summary>
public class QrCodeAuthCommand : IWithInstanceId, IRequest<Channel<string>>
{
    /// <summary>
    /// Уникальный идентификатор инстанса.
    /// </summary>
    public required Guid InstanceId { get; init; }
}