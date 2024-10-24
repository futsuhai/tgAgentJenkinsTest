using TgInstanceAgent.Application.Abstractions.TelegramClient.Functions.Components;

namespace TgInstanceAgent.Application.Abstractions.TelegramClient.Functions.Messages;

/// <summary>
/// Запрос на отправку сообщения.
/// </summary>
public class TgSendMessageRequest
{
    /// <summary>
    /// Чат
    /// </summary>
    public required TgInputChat Chat { get; init; }
    
    /// <summary>
    /// Данные сообщения.
    /// </summary>
    public required TgInputMessageContent InputMessageData { get; init; }

    /// <summary>
    /// Параметры отправки сообщения
    /// </summary>
    public TgInputMessageSendOptions? MessageSendOptions { get; init; }
    
    /// <summary>
    /// Флаг, указывающий, нужно ли отправлять активность перед сообщением
    /// </summary>
    public bool NeedShowActivity { get; init; }
}