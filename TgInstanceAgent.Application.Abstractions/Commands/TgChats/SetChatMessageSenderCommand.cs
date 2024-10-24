using MediatR;
using TgInstanceAgent.Application.Abstractions.Interfaces;

namespace TgInstanceAgent.Application.Abstractions.Commands.TgChats;

/// <summary>
/// Команда изменения отправителя сообщений в групповом чате
/// </summary>
public class SetChatMessageSenderCommand : IWitMessageSender, IWithInstanceId, IRequest
{
    /// <summary>
    /// Уникальный идентификатор инстанса.
    /// </summary>
    public required Guid InstanceId { get; init; }
    
    /// <summary>
    /// Идентификатор чата.
    /// </summary>
    public required long ChatId { get; init; }
    
    /// <summary>
    /// Идентификатор пользователя.
    /// </summary>
    public long? SenderUserId { get; init; }
    
    /// <summary>
    /// Идентификатор чата.
    /// </summary>
    public long? SenderChatId { get; init; }
}