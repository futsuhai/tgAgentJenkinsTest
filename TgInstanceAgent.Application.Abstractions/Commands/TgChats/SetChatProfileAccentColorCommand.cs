using MediatR;
using TgInstanceAgent.Application.Abstractions.Interfaces;

namespace TgInstanceAgent.Application.Abstractions.Commands.TgChats;

/// <summary>
/// Команда изменения цвета профиля группового чата
/// </summary>
public class SetChatProfileAccentColorCommand : IWithInstanceId, IRequest
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
    /// Идентификатор цвета
    /// </summary>
    public required int ProfileAccentColorId { get; init; }

    /// <summary>
    /// Идентификатор премиум емозди
    /// </summary>
    public required long ProfileBackgroundCustomEmojiId { get; init; }
}