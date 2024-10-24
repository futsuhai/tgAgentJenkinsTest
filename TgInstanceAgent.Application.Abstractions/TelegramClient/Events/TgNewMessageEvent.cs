using TgInstanceAgent.Application.Abstractions.TelegramClient.Events.Visitors;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Objects.Messages;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Objects.Users;

namespace TgInstanceAgent.Application.Abstractions.TelegramClient.Events;

/// <summary>
/// Событие, генерируемое при получении нового сообщения в чате.
/// </summary>
public class TgNewMessageEvent : TgEvent
{
    /// <summary>
    /// Сообщение.
    /// </summary>
    public required TgMessage Message { get; init; }
    
    /// <summary>
    /// Пользователи, чьи идентификаторы присутствуют в обновлении, необходимо для отображения чата.
    /// </summary>
    public required IReadOnlyCollection<TgUser> Users { get; init; }
    
    /// <summary>
    /// Принять посетителя события.
    /// </summary>
    public override void Accept(ITgEventVisitor eventVisitor) => eventVisitor.Visit(this);
}