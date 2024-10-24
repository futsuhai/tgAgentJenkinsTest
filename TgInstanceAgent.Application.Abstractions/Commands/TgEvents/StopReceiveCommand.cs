using MediatR;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Events;

namespace TgInstanceAgent.Application.Abstractions.Commands.TgEvents;

/// <summary>
/// Команда для прекращения отправки событий Телеграм
/// </summary>
public record StopReceiveCommand : IRequest
{
    /// <summary>
    /// Уникальный идентификатор инстанса.
    /// </summary>
    public required Guid InstanceId { get; init; }
    
    /// <summary>
    ///  Отписываемый обработчик событий мессенджера
    /// </summary>
    public required NewEvent Handler { get; init; }
}