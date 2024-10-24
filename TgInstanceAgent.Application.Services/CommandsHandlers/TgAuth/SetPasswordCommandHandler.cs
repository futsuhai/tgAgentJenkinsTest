using MediatR;
using TgInstanceAgent.Application.Abstractions.Commands.TgAuth;
using TgInstanceAgent.Application.Abstractions.InstancesService;

namespace TgInstanceAgent.Application.Services.CommandsHandlers.TgAuth;

/// <summary>
/// Обработчик команды для установки пароля аутентификации.
/// </summary>
/// <param name="instancesService">Сервис, используемый для запуска клиентских инстансов.</param>
public class SetPasswordCommandHandler(IInstancesService instancesService) : IRequestHandler<SetPasswordCommand>
{
    /// <summary>
    /// Обработка команды установки пароля аутентификации.
    /// </summary>
    /// <param name="request">Команда, содержащая данные для установки пароля.</param>
    /// <param name="cancellationToken">Токен для отмены операции.</param>
    /// <returns>Задачу, представляющую асинхронную операцию.</returns>
    public async Task Handle(SetPasswordCommand request, CancellationToken cancellationToken)
    {
        // Инициализация клиента для соответствующего инстанса.
        var telegramClient = await instancesService.StartClientAsync(request.InstanceId, cancellationToken);
        
        // Установка пароля аутентификации для инстанса.
        await telegramClient.SetAuthenticationPasswordAsync(request.Password, cancellationToken);
    }
}