using MediatR;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Events;

namespace TgInstanceAgent.Application.Abstractions.Commands.TgEvents;

/// <summary>
/// Команда запуска обработки событий Телеграм
/// </summary>
public record StartReceiveCommand : IRequest
{
    /// <summary>
    /// Уникальный идентификатор инстанса.
    /// </summary>
    public required Guid InstanceId { get; init; }
    
    /// <summary>
    /// Подписываемый обработчик событий мессенджера
    /// </summary>
    public required NewEvent Handler { get; init; }
}