using TgInstanceAgent.Application.Abstractions.TelegramClient.Events.Visitors;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Objects.Chats;

namespace TgInstanceAgent.Application.Abstractions.TelegramClient.Events;

/// <summary>
/// Событие обновления информации о базовом групповом чате
/// </summary>
public class TgUpdateBasicGroupFullInfo : TgEvent
{
    /// <summary>
    /// Идентификатор базового группового чата
    /// </summary>
    public required long BasicGroupId { get; init; }
    
    /// <summary>
    /// Обновлённая информация о базовом групповом чате
    /// </summary>
    public required TgBasicGroupFullInfo BasicGroupFullInfo { get; init; }
    
    /// <summary>
    /// Принять посетителя события.
    /// </summary>
    public override void Accept(ITgEventVisitor eventVisitor) => eventVisitor.Visit(this);
}