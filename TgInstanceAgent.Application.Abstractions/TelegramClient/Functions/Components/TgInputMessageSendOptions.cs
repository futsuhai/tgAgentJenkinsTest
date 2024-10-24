namespace TgInstanceAgent.Application.Abstractions.TelegramClient.Functions.Components;

/// <summary>
/// Параметры отправки сообщения
/// </summary>
public class TgInputMessageSendOptions
{
    /// <summary>
    /// Флаг - выключить оповещение получателя
    /// </summary>
    public bool DisableNotification { get; init; }
    
    /// <summary>
    /// Флаг - можно ли пересылать, сохранять отправленное сообщение
    /// </summary>
    public bool ProtectContent { get; init; }
    
    /// <summary>
    /// Тип планирования сообщения
    /// </summary>
    public TgInputSchedulingState? SchedulingState { get; set; }
}