using MediatR;
using TgInstanceAgent.Application.Abstractions.Interfaces;

namespace TgInstanceAgent.Application.Abstractions.Commands.TgContacts;

/// <summary>
/// Команда добавления контакта
/// </summary>
public class AddContactsCommand : IWithInstanceId, IRequest
{
    /// <summary>
    /// Уникальный идентификатор инстанса.
    /// </summary>
    public required Guid InstanceId { get; init; }
    
    /// <summary>
    /// Импортируемый контакт
    /// </summary>
    public class TgImportContact
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
    
    /// <summary>
    /// Импортированные контакты
    /// </summary>
    public required IEnumerable<TgImportContact> Contacts { get; init; }
}