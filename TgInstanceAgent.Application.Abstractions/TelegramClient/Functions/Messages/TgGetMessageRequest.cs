using TgInstanceAgent.Application.Abstractions.TelegramClient.Functions.Components;

namespace TgInstanceAgent.Application.Abstractions.TelegramClient.Functions.Messages;

/// <summary>
/// Запрос на получение данных сообщения.
/// </summary>
public class TgGetMessageRequest
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