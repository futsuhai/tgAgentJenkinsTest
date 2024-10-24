using MediatR;
using TgInstanceAgent.Application.Abstractions.Commands.TgEvents;
using TgInstanceAgent.Application.Abstractions.InstancesService;

namespace TgInstanceAgent.Application.Services.CommandsHandlers.TgEvents;

/// <summary>
/// Обработчик для команды транслирования событий мессенджера
/// </summary>
/// <param name="instancesService">Сервис, используемый для запуска клиентских инстансов.</param>
public class StartReceiveCommandHandler(IInstancesService instancesService)
    : IRequestHandler<StartReceiveCommand>
{
    /// <summary>
    /// Обработчик для команды транслирования событий мессенджера
    /// </summary>
    /// <param name="request">Команда отправки событий</param>
    /// <param name="cancellationToken">Токен отмены</param>
    public async Task Handle(StartReceiveCommand request, CancellationToken cancellationToken)
    {
        // Запускаем клиентский сервис для пользователя с указанным в запросе идентификатором.
        var telegramClient = await instancesService.StartClientAsync(request.InstanceId, cancellationToken);

        // Подписываем обработчик отправки ивентов клиенту
        telegramClient.NewEvent += request.Handler;
    }
}