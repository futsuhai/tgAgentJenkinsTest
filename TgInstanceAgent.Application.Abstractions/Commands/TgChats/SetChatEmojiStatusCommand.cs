using MediatR;
using TgInstanceAgent.Application.Abstractions.Interfaces;

namespace TgInstanceAgent.Application.Abstractions.Commands.TgChats;

/// <summary>
/// Команда для изменения статуса емозди чата
/// </summary>
public class SetChatEmojiStatusCommand : IWithInstanceId, IRequest
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
    ///  Идентификатор пользовательского эмодзи, который будет установлен в качестве статуса.
    /// </summary>
    public long? CustomEmojiId { get; init; }
    
    /// <summary>
    /// Дата истечения срока действия статуса эмодзи, заданная в виде UNIX-времени.
    /// </summary>
    public int? ExpirationDate { get; init; }
}