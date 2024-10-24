using MediatR;
using TgInstanceAgent.Application.Abstractions.Interfaces;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Objects.Components;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Objects.Users;

namespace TgInstanceAgent.Application.Abstractions.Queries.TgUsers;

/// <summary>
/// Запрос на получение контактов, который представляет собой запрос к инстансу.
/// Результат запроса - коллекция контактов.
/// </summary>
public class GetContactsQuery : IWithInstanceId, IRequest<TgCountResult<TgUser>>
{
    /// <summary>
    /// Уникальный идентификатор инстанса.
    /// </summary>
    public required Guid InstanceId { get; init; }
    
    /// <summary>
    /// Смещение (Пример - У пользователя 3 контакта, смещение 2 -> получаем 1 контакт).
    /// </summary>
    public int Offset { get; init; }
    
    /// <summary>
    /// Число необходимых контактов.
    /// </summary>
    public int Limit { get; init; }
    
    /// <summary>
    /// Начиная с какого идентификатора будут возвращаться сообщения в запросе. 
    /// </summary>
    public long? FromContactId { get; init; }
}