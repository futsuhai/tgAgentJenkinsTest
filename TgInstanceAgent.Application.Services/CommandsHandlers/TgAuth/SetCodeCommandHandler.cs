using MediatR;
using TgInstanceAgent.Application.Abstractions.Commands.TgAuth;
using TgInstanceAgent.Application.Abstractions.InstancesService;

namespace TgInstanceAgent.Application.Services.CommandsHandlers.TgAuth;

/// <summary>
/// Обработчик команды для установки кода аутентификации.
/// </summary>
/// <param name="instancesService">Сервис, используемый для запуска клиентских инстансов.</param>
public class SetCodeCommandHandler(IInstancesService instancesService) : IRequestHandler<SetCodeCommand>
{
    /// <summary>
    /// Обработка команды установки кода аутентификации.
    /// </summary>
    /// <param name="request">Команда, содержащая данные для установки кода.</param>
    /// <param name="cancellationToken">Токен для отмены операции.</param>
    /// <returns>Задачу, представляющую асинхронную операцию.</returns>
    public async Task Handle(SetCodeCommand request, CancellationToken cancellationToken)
    {
        // Инициализация клиента для соответствующего инстанса.
        var telegramClient = await instancesService.StartClientAsync(request.InstanceId, cancellationToken);
        
        // Установка кода аутентификации для инстанса.
        await telegramClient.SetAuthenticationCodeAsync(request.Code, cancellationToken);
    }
}