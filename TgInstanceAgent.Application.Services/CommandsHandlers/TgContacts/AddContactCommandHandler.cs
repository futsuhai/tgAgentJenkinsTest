using MediatR;
using TgInstanceAgent.Application.Abstractions.Commands.TgContacts;
using TgInstanceAgent.Application.Abstractions.InstancesService;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Functions.Users;
using TgInstanceAgent.Application.Services.Extensions;

namespace TgInstanceAgent.Application.Services.CommandsHandlers.TgContacts;

/// <summary>
/// Обработчик команды на добавление контакта
/// </summary>
public class AddContactCommandHandler(IInstancesService instancesService)
    : IRequestHandler<AddContactCommand>
{
    /// <summary>
    /// Обрабатывает команду добавления контакта.
    /// </summary>
    /// <param name="request">Команда на добавление контакта.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    public async Task Handle(AddContactCommand request, CancellationToken cancellationToken)
    {
        // Запуск телеграм клиента инстанса.
        var client = await instancesService.StartClientAsync(request.InstanceId, cancellationToken);
        
        // Запрос на добавление контакта
        await client.AddContactAsync(new TgAddContactRequest
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            User = request.GetUser()
        }, cancellationToken);
    }
}