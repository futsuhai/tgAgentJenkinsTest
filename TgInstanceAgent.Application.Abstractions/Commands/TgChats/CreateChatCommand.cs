using MediatR;
using TgInstanceAgent.Application.Abstractions.Interfaces;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Functions.Enums;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Objects.Chats;

namespace TgInstanceAgent.Application.Abstractions.Commands.TgChats;

/// <summary>
/// Команда создания чата.
/// </summary>
public class CreateChatCommand : IWithInstanceId, IRequest, IRequest<TgChat>
{
    /// <summary>
    /// Уникальный идентификатор инстанса.
    /// </summary>
    public required Guid InstanceId { get; init; }

    /// <summary>
    /// Идентификатор объекта, для которого нужно создать чат (пользователь, базовая группа, супергруппа, секретный чат).
    /// </summary>
    public required long Id { get; init; }

    /// <summary>
    /// Тип чата.
    /// Нужен для того, чтобы создать или загрузить чат с серверов Telegram.
    /// </summary>
    public required TgInputChatType ChatType { get; init; }
}