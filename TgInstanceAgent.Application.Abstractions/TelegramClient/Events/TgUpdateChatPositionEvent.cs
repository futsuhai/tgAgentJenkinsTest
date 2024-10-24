using TgInstanceAgent.Application.Abstractions.TelegramClient.Events.Visitors;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Objects.Chats;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Objects.Users;

namespace TgInstanceAgent.Application.Abstractions.TelegramClient.Events;

/// <summary>
/// Позиция чата была изменена.
/// </summary>
public class TgUpdateChatPositionEvent : TgEvent
{
    /// <summary>
    /// Обновленные данные чата.
    /// Отправляем чат в этом событии, так как чат может быть не загружен у клиента, но сообщение нужно отобразить.
    /// Чтоб не делать дополнительный запрос, сразу помещаем чат в это событие.
    /// </summary>
    public required TgChat Chat { get; init; }
    
    /// <summary>
    /// Пользователи, чьи идентификаторы присутствуют в обновлении, необходимо для отображения чата.
    /// </summary>
    public required IReadOnlyCollection<TgUser> Users {get; set; }
    
    /// <summary>
    /// Принять посетителя события.
    /// </summary>
    public override void Accept(ITgEventVisitor eventVisitor) => eventVisitor.Visit(this);
}