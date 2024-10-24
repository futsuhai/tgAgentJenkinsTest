using MediatR;
using TgInstanceAgent.Application.Abstractions.Commands.TgEvents;
using TgInstanceAgent.Application.Abstractions.InstancesService;

namespace TgInstanceAgent.Application.Services.CommandsHandlers.TgEvents;

/// <summary>
/// Обработчик для команды прекращения транслирования событий мессенджера
/// </summary>
/// <param name="instancesService">Сервис, используемый для запуска клиентских инстансов.</param>
public class StopReceiveCommandHandler(IInstancesService instancesService)
    : IRequestHandler<StopReceiveCommand>
{
    /// <summary>
    /// Обработчик для команды прекращения транслирования событий мессенджера
    /// </summary>
    /// <param name="request">Команда прекращения отправки событий</param>
    /// <param name="cancellationToken">Токен отмены</param>
    public async Task Handle(StopReceiveCommand request, CancellationToken cancellationToken)
    {
        // Запускаем клиентский сервис для пользователя с указанным в запросе идентификатором.
        var telegramClient = await instancesService.StartClientAsync(request.InstanceId, cancellationToken);

        // Отписываем обработчик отправки ивентов клиенту
        telegramClient.NewEvent -= request.Handler;
    }
}