using TgInstanceAgent.Application.Abstractions.TelegramClient.Functions.Components;

namespace TgInstanceAgent.Application.Abstractions.TelegramClient.Functions.Messages;

/// <summary>
/// Запрос на удаление сообщений в чате
/// </summary>
public class TgDeleteMessagesRequest
{
    /// <summary>
    /// Чат
    /// </summary>
    public required TgInputChat Chat { get; init; }
    
    /// <summary>
    /// Массив идентификаторо сообщений для пересылки
    /// </summary>
    public required long[] MessageIds { get; init; }
    
    /// <summary>
    /// Истинно, если требуется удалить для всех участников чата
    /// </summary>
    public bool Revoke { get; init; }
    
}