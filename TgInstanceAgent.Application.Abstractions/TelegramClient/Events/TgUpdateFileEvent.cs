using TgInstanceAgent.Application.Abstractions.TelegramClient.Events.Visitors;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Objects.Components;

namespace TgInstanceAgent.Application.Abstractions.TelegramClient.Events;

/// <summary>
/// Событие изменения состояния загрузки файла
/// </summary>
public class TgUpdateFileEvent : TgEvent
{
    /// <summary>
    /// Файл.
    /// </summary>
    public required TgFile File { get; init; }
    
    /// <summary>
    /// Принять посетителя события.
    /// </summary>
    public override void Accept(ITgEventVisitor eventVisitor) => eventVisitor.Visit(this);
}