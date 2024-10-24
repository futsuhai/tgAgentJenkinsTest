using TgInstanceAgent.Application.Abstractions.TelegramClient.Functions.Components;

namespace TgInstanceAgent.Application.Abstractions.TelegramClient.Functions.Messages;

/// <summary>
/// Запрос на получение списка доступных реакций на сообщение.
/// </summary>
public class TgGetMessageReactionsRequest
{
    /// <summary>
    /// Чат
    /// </summary>
    public required TgInputChat Chat { get; init; }
    
    /// <summary>
    /// Идентификатор сообщения.
    /// </summary>
    public required long MessageId { get; init; }
}