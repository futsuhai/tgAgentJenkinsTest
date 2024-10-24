using MediatR;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Objects.Chats;

namespace TgInstanceAgent.Application.Abstractions.Queries.TgChats;

/// <summary>
/// Запрос на получение количества непрочитанных чатов в списках чатов.
/// </summary>
public class GetUnreadChatsCountQuery : IRequest<IReadOnlyCollection<TgUnreadChatsCount>>
{
    /// <summary>
    /// Уникальный идентификатор инстанса.
    /// </summary>
    public required Guid InstanceId { get; init; }
}