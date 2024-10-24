using TgInstanceAgent.Application.Abstractions.TelegramClient.Events.Visitors;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Objects.Chats;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Objects.Users;

namespace TgInstanceAgent.Application.Abstractions.TelegramClient.Events;


/// <summary>
/// Последнее сообщение чата было изменено.
/// Также событие будет содержать информацию о количестве непрочитанных сообщений в чате
/// </summary>
public class TgUpdateChatLastMessageEvent : TgEvent
{
    /// <summary>
    /// Обновленные данные чата.
    /// Отправляем чат в этом событии, так как чат может быть не загружен у клиента, но сообщение нужно отобразить.
    /// Чтоб не делать дополнительный запрос, сразу помещаем чат в это событие.
    /// </summary>
    public required TgChat Chat { get; init; }
    
    /// <summary>
    /// Пользователи, которые затронуты в обновлении
    /// </summary>
    public required IReadOnlyCollection<TgUser> Users { get; init; }
    
    /// <summary>
    /// Принять посетителя события.
    /// </summary>
    public override void Accept(ITgEventVisitor eventVisitor) => eventVisitor.Visit(this);
}