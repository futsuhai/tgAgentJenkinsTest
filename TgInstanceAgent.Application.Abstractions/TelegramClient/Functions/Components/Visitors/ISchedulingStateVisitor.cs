namespace TgInstanceAgent.Application.Abstractions.TelegramClient.Functions.Components.Visitors;

/// <summary>
/// Интерфейс посетителя типа планирования отправки сообщения
/// </summary>
public interface IMessageSchedulingStateAsyncVisitor
{
    /// <summary>
    /// Посетить планирование сообщения по дате
    /// </summary>
    /// <param name="tgInputSchedulingStateAtDate">Планирование сообщения по дате</param>
    void Visit(TgInputSchedulingStateAtDate tgInputSchedulingStateAtDate);

    /// <summary>
    /// Посетить планирование сообщения, когда получатель будет онлайн
    /// </summary>
    /// <param name="tgInputSchedulingStateWhenOnline">Планирование сообщения, когда пользователь будет онлайн</param>
    void Visit(TgInputSchedulingStateWhenOnline tgInputSchedulingStateWhenOnline);
}