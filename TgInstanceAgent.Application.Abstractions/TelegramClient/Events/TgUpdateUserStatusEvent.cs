using TgInstanceAgent.Application.Abstractions.TelegramClient.Events.Visitors;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Objects.Users;

namespace TgInstanceAgent.Application.Abstractions.TelegramClient.Events;

/// <summary>
/// Событие изменения статуса пользователя (онлайн/оффлайн)
/// </summary>
public class TgUpdateUserStatusEvent : TgEvent
{
    /// <summary>
    /// Идентификатор пользователя
    /// </summary>
    public required long UserId { get; init; }

    /// <summary>
    /// Новый статус пользователя
    /// </summary>
    public required TgUserStatus Status { get; init; }

    /// <summary>
    /// Принять посетителя события.
    /// </summary>
    public override void Accept(ITgEventVisitor eventVisitor) => eventVisitor.Visit(this);
}