using MediatR;
using TgInstanceAgent.Application.Abstractions.Commands.TgAuth;
using TgInstanceAgent.Application.Abstractions.InstancesService;

namespace TgInstanceAgent.Application.Services.CommandsHandlers.TgAuth;

/// <summary>
/// Обработчик команды для установки номера телефона аутентификации.
/// </summary>
/// <param name="instancesService">Сервис, используемый для запуска клиентских инстансов.</param>
public class SetPhoneNumberCommandHandler(IInstancesService instancesService) : IRequestHandler<SetPhoneNumberCommand>
{
    /// <summary>
    /// Обработка команды установки номера телефона аутентификации.
    /// </summary>
    /// <param name="request">Команда, содержащая данные для установки номера телефона.</param>
    /// <param name="cancellationToken">Токен для отмены операции.</param>
    /// <returns>Задачу, представляющую асинхронную операцию.</returns>
    public async Task Handle(SetPhoneNumberCommand request, CancellationToken cancellationToken)
    {
        // Инициализация клиента для соответствующего инстанса.
        var telegramClient = await instancesService.StartClientAsync(request.InstanceId, cancellationToken);
        
        // Установка номера телефона аутентификации для инстанса.
        await telegramClient.SetAuthenticationPhoneNumberAsync(request.PhoneNumber, cancellationToken);
    }
}