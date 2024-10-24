using MediatR;
using TgInstanceAgent.Application.Abstractions.Interfaces;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Objects.Chats;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Objects.Components;

namespace TgInstanceAgent.Application.Abstractions.Queries.TgChats;

/// <summary>
/// Запрос на поиск публичных чатов, который представляет собой запрос к инстансу.
/// Результат запроса - коллекция публичных чатов.
/// </summary>
public class SearchPublicChatsQuery : IWithInstanceId, IRequest<TgCountResult<TgChat>>
{
    /// <summary>
    /// Уникальный идентификатор инстанса.
    /// </summary>
    public required Guid InstanceId { get; init; }
    
    /// <summary>
    /// Строка с ключевыми словами для поиска.
    /// </summary>
    public required string Query { get; init; }
}