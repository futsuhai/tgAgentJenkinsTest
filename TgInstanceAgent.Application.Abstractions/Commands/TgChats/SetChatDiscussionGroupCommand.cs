using MediatR;
using TgInstanceAgent.Application.Abstractions.Interfaces;

namespace TgInstanceAgent.Application.Abstractions.Commands.TgChats;

/// <summary>
/// Команда для установки группы обсуждения
/// </summary>
public class SetChatDiscussionGroupCommand : IWithInstanceId, IRequest
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
    /// Идентификатор обсуждаемой группы
    /// </summary>
    public required long DiscussionChatId { get; init; }
}