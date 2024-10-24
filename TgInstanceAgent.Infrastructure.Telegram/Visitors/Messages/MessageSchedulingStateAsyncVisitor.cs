using TdLib;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Functions.Components;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Functions.Components.Visitors;

namespace TgInstanceAgent.Infrastructure.Telegram.Visitors.Messages;

/// <summary>
/// Посетитель типа планирования отправки сообщения
/// </summary>
public class MessageSchedulingStateAsyncVisitor : IMessageSchedulingStateAsyncVisitor
{
    /// <summary>
    /// Тип планирования отправки сообщения
    /// </summary>
    public TdApi.MessageSchedulingState? SchedulingState { get; private set; }
    
    /// <inheritdoc/>
    /// <summary>
    /// Метод для посещения типа планирования отправки сообщения
    /// </summary>
    public void Visit(TgInputSchedulingStateAtDate tgInputSchedulingStateAtDate)
    {
        // Устанавливаем тип планирования отправки сообщения по дате
        SchedulingState = new TdApi.MessageSchedulingState.MessageSchedulingStateSendAtDate
        {
            // Дата отправки сообщения
            SendDate = tgInputSchedulingStateAtDate.SendDate
        };
    }

    /// <inheritdoc/>
    /// <summary>
    /// Метод для посещения типа планирования отправки сообщения
    /// </summary>
    public void Visit(TgInputSchedulingStateWhenOnline tgInputSchedulingStateWhenOnline)
    {
        // Устанавливаем тип планирования отправки сообщения, когда получатель будет онлайн
        SchedulingState = new TdApi.MessageSchedulingState.MessageSchedulingStateSendWhenOnline();
    }
}