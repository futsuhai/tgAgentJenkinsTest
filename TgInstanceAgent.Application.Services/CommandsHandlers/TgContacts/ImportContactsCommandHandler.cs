using MediatR;
using TgInstanceAgent.Application.Abstractions.Commands.TgContacts;
using TgInstanceAgent.Application.Abstractions.InstancesService;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Functions.Users;

namespace TgInstanceAgent.Application.Services.CommandsHandlers.TgContacts;

/// <summary>
/// Обработчик команды на импорт контактов
/// </summary>
public class ImportContactsCommandHandler(IInstancesService instancesService)
    : IRequestHandler<ImportContactsCommand>
{
    /// <summary>
    /// Обрабатывает команду на импорт контактов.
    /// </summary>
    /// <param name="request">Команда на импорт контактов.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    public async Task Handle(ImportContactsCommand request, CancellationToken cancellationToken)
    {
        // Запуск телеграм клиента инстанса.
        var telegramClient = await instancesService.StartClientAsync(request.InstanceId, cancellationToken);

        // Формируем запрос на добавление контактов
        var contacts = request.Contacts.Select(c => new TgImportContactRequest
        {
            // Номер телефона
            PhoneNumber = c.PhoneNumber,

            // Имя
            FirstName = c.FirstName,

            // Фамилия
            LastName = c.LastName
        });

        // Запрос на добавление контакта
        await telegramClient.ImportContactsAsync(contacts, cancellationToken);
    }
}