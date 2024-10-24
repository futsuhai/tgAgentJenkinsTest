using MediatR;
using TgInstanceAgent.Application.Abstractions.Interfaces;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Objects.Chats;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Objects.Components;

namespace TgInstanceAgent.Application.Abstractions.Queries.TgChats;

/// <summary>
/// Запрос на поиск чатов, который представляет собой запрос к инстансу.
/// Результат запроса - коллекция публичных чатов.
/// </summary>
public class SearchChatsQuery : IWithInstanceId, IRequest<TgCountResult<TgChat>>
{
    /// <summary>
    /// Уникальный идентификатор инстанса.
    /// </summary>
    public required Guid InstanceId { get; init; }
 
    /// <summary>
    /// Число необходимых чатов.
    /// </summary>
    public int Limit { get; init; }
    
    /// <summary>
    /// Строка с ключевыми словами для поиска.
    /// </summary>
    public required string Query { get; init; }
}