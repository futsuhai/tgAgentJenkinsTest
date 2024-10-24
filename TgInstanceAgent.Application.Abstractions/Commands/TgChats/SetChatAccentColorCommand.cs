using MediatR;
using TgInstanceAgent.Application.Abstractions.Interfaces;

namespace TgInstanceAgent.Application.Abstractions.Commands.TgChats;

/// <summary>
/// Команда изменения акцентного цвета чата.
/// </summary>
public class SetChatAccentColorCommand : IWithInstanceId, IRequest
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
    /// Идентификатор акцентного цвета, который нужно использовать.
    /// </summary>
    public int? AccentColorId { get; init; }

    /// <summary>
    /// Идентификатор пользовательского эмодзи, который будет показан в заголовке ответа и фоне предварительного просмотра ссылки.
    /// </summary>
    public long? BackgroundCustomEmojiId { get; init; }
}