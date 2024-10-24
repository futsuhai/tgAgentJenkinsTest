using MediatR;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Objects.Chats;

namespace TgInstanceAgent.Application.Abstractions.Queries.TgChats;

/// <summary>
/// Запрос на получение количества непрочитанных сообщений в списках чатов.
/// </summary>
public class GetUnreadMessagesCountQuery : IRequest<IReadOnlyCollection<TgUnreadMessagesCount>>
{
    /// <summary>
    /// Уникальный идентификатор инстанса.
    /// </summary>
    public required Guid InstanceId { get; init; }
}