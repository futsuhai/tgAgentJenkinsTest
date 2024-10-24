using TgInstanceAgent.Application.Abstractions.TelegramClient.Events.Visitors;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Objects.Chats;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Objects.Users;

namespace TgInstanceAgent.Application.Abstractions.TelegramClient.Events;

/// <summary>
/// Событие, генерируемое при создании нового чата.
/// </summary>
public class TgNewChatEvent : TgEvent
{
    /// <summary>
    /// Данные информации о групповом чате
    /// </summary>
    public required TgChat Chat { get; init; }
    
    /// <summary>
    /// Пользователи, чьи идентификаторы присутствуют в обновлении, необходимо для отображения чата.
    /// </summary>
    public required IReadOnlyCollection<TgUser> Users { get; init; }
    
    /// <summary>
    /// Принять посетителя события.
    /// </summary>
    public override void Accept(ITgEventVisitor eventVisitor) => eventVisitor.Visit(this);
}