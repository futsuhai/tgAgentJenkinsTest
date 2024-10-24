namespace TgInstanceAgent.Application.Abstractions.TelegramClient.Functions.Chats;

/// <summary>
/// Запрос на изменение задержки отправки сообщений в чате
/// </summary>
public class TgSetChatSlowModeDelayRequest
{
    /// <summary>
    /// Идентификатор чата.
    /// </summary>
    public required long ChatId { get; init; }
    
    /// <summary>
    /// Задержка
    /// </summary>
    public required int SlowModeDelay { get; init; }
}