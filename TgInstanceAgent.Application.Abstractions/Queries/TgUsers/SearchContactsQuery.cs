using MediatR;
using TgInstanceAgent.Application.Abstractions.Interfaces;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Objects.Components;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Objects.Users;

namespace TgInstanceAgent.Application.Abstractions.Queries.TgUsers;

/// <summary>
/// Запрос на поиск контактов, который представляет собой запрос к инстансу.
/// Результат запроса - коллекция контактов.
/// </summary>
public class SearchContactsQuery : IWithInstanceId, IRequest<TgCountResult<TgUser>>
{
    /// <summary>
    /// Уникальный идентификатор инстанса.
    /// </summary>
    public required Guid InstanceId { get; init; }
    
    /// <summary>
    /// Число необходимых контактов.
    /// </summary>
    public int Limit { get; init; }
    
    /// <summary>
    /// Строка с ключевыми словами для поиска.
    /// </summary>
    public required string Query { get; init; }
}