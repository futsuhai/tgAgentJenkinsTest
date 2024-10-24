using MediatR;
using TgInstanceAgent.Application.Abstractions.Commands.TgProfile;
using TgInstanceAgent.Application.Abstractions.InstancesService;

namespace TgInstanceAgent.Application.Services.CommandsHandlers.TgProfile;

/// <summary>
/// Обработчик команды на установку био текущему пользователю.
/// </summary>
public class SetUserBioCommandHandler(IInstancesService instancesService):
    IRequestHandler<SetUserBioCommand>
{
    /// <summary>
    /// Обрабатывает команду на установку био текущему пользователю асинхронно.
    /// </summary>
    /// <param name="request">Запрос на установку био текущему пользователю асинхронно.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns></returns>
    public async Task Handle(SetUserBioCommand request, CancellationToken cancellationToken)
    {
        // Запуск клиента для обработки команды.
        var service = await instancesService.StartClientAsync(request.InstanceId, cancellationToken);

        // Установка био текущего пользователя через сервис инстансов.
        await service.SetUserBioAsync(request.Bio, cancellationToken);
    }
}