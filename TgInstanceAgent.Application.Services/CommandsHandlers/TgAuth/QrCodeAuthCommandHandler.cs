using System.Threading.Channels;
using MediatR;
using TgInstanceAgent.Application.Abstractions.Commands.TgAuth;
using TgInstanceAgent.Application.Abstractions.InstancesService;

namespace TgInstanceAgent.Application.Services.CommandsHandlers.TgAuth;

/// <summary>
/// Обработчик команды для аутентификации с использованием QR-кода.
/// </summary>
/// <param name="instancesService">Сервис, используемый для запуска клиентских инстансов.</param>
public class QrCodeAuthCommandHandler(IInstancesService instancesService)
    : IRequestHandler<QrCodeAuthCommand, Channel<string>>
{
    /// <summary>
    /// Обработка команды аутентификации с помощью QR-кода.
    /// </summary>
    /// <param name="request">Команда запроса на аутентификацию.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    public async Task<Channel<string>> Handle(QrCodeAuthCommand request, CancellationToken cancellationToken)
    {
        // Запускаем клиентский сервис для пользователя с указанным в запросе идентификатором.
        var telegramClient = await instancesService.StartClientAsync(request.InstanceId, cancellationToken);

        // Проводим аутентификацию через QR-код.
        return await telegramClient.QrCodeAuthenticateAsync(cancellationToken);
    }
}