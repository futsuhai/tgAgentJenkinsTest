using TgInstanceAgent.Application.Abstractions.TelegramClient.Functions.Components.Visitors;

namespace TgInstanceAgent.Application.Abstractions.TelegramClient.Functions.Components;

/// <summary>
/// Базовый класс, представляющий тип планирования сообщения
/// </summary>
public abstract class TgInputSchedulingState
{
    /// <summary>
    /// Принять посетителя планирования сообщения.
    /// </summary>
    public abstract void Accept(IMessageSchedulingStateAsyncVisitor visitor);
}

/// <summary>
/// Планирование отправки сообщения, когда получатель будет онлайн
/// </summary>
public class TgInputSchedulingStateWhenOnline : TgInputSchedulingState
{
    /// <summary>
    /// Принять посетителя планирования сообщения.
    /// </summary>
    public override void Accept(IMessageSchedulingStateAsyncVisitor visitor) => visitor.Visit(this);
}

/// <summary>
/// Планирование отправки сообщения по времени
/// </summary>
public class TgInputSchedulingStateAtDate : TgInputSchedulingState
{
    /// <summary>
    /// Дата отправки сообщения в UNIX
    /// </summary>
    public int SendDate { get; init; }
    
    /// <summary>
    /// Принять посетителя планирования сообщения.
    /// </summary>
    public override void Accept(IMessageSchedulingStateAsyncVisitor visitor) => visitor.Visit(this);
}