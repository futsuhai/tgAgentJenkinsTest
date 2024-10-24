using TgInstanceAgent.Application.Abstractions.TelegramClient.Events.Visitors;

namespace TgInstanceAgent.Application.Abstractions.TelegramClient.Events;

/// <summary>
/// Абстрактный класс события Telegram
/// </summary>
public abstract class TgEvent
{
    /// <summary>
    /// Принять посетителя события.
    /// </summary>
    public abstract void Accept(ITgEventVisitor eventVisitor);
}

/// <summary>
/// Делегат, представляющий функцию обработки события Telegram
/// </summary>
public delegate void NewEvent(TgEvent @event);