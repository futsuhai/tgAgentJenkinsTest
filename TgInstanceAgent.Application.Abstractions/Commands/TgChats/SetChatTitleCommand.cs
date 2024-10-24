using MediatR;
using TgInstanceAgent.Application.Abstractions.Interfaces;

namespace TgInstanceAgent.Application.Abstractions.Commands.TgChats;

/// <summary>
/// Команда изменения заголовка чата.
/// </summary>
public class SetChatTitleCommand : IWithInstanceId, IRequest
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
    /// Заголовок.
    /// </summary>
    public required string Title { get; init; }
}