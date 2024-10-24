using MediatR;
using TgInstanceAgent.Application.Abstractions.Interfaces;

namespace TgInstanceAgent.Application.Abstractions.Commands.TgContacts;

/// <summary>
/// Команда для импорта контактов
/// </summary>
public class ImportContactsCommand : IWithInstanceId, IRequest
{
    /// <summary>
    /// Уникальный идентификатор инстанса.
    /// </summary>
    public required Guid InstanceId { get; init; }
    
    /// <summary>
    /// Список импортируемых контактов
    /// </summary>
    public required IEnumerable<ImportContact> Contacts { get; init; }
}

/// <summary>
/// Класс импортируемого контакта
/// </summary>
public class ImportContact
{
    /// <summary>
    /// Номер телефона
    /// </summary>
    public required string PhoneNumber { get; init; }

    /// <summary>
    /// Имя
    /// </summary>
    public required string FirstName { get; init; }

    /// <summary>
    /// Фамилия
    /// </summary>
    public string? LastName { get; init; }
}