using TgInstanceAgent.Application.Abstractions.TelegramClient.Events.Visitors;

namespace TgInstanceAgent.Application.Abstractions.TelegramClient.Events;

/// <summary>
/// Событие удаления истории
/// </summary>
public class TgStoryDeletedEvent : TgEvent
{
    /// <summary>
    /// Идентификатор чата, который опубликовал историю
    /// </summary>
    public required long StorySenderChatId { get; init; }

    /// <summary>
    /// Идентификатор истории
    /// </summary>
    public required int StoryId { get; init; }

    /// <summary>
    /// Принять посетителя события.
    /// </summary>
    public override void Accept(ITgEventVisitor eventVisitor) => eventVisitor.Visit(this);
}