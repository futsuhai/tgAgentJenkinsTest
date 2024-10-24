using MediatR;
using TgInstanceAgent.Application.Abstractions.Commands.TgContacts;
using TgInstanceAgent.Application.Abstractions.InstancesService;
using TgInstanceAgent.Application.Services.Extensions;

namespace TgInstanceAgent.Application.Services.CommandsHandlers.TgContacts;

/// <summary>
/// Обработчик команды на удаление контакта
/// </summary>
public class RemoveContactCommandHandler(IInstancesService instancesService)
    : IRequestHandler<RemoveContactCommand>
{
    /// <summary>
    /// Обрабатывает команду на удаление контакта.
    /// </summary>
    /// <param name="request">Команда на удаление контакта.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    public async Task Handle(RemoveContactCommand request, CancellationToken cancellationToken)
    {
        // Запуск телеграм клиента инстанса.
        var tgClientService = await instancesService.StartClientAsync(request.InstanceId, cancellationToken);

        // запрос на удаление контакта
        await tgClientService.RemoveContactAsync(request.GetUser(), cancellationToken);
    }
}