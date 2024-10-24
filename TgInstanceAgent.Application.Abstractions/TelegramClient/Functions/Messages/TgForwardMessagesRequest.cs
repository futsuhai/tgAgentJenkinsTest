using TgInstanceAgent.Application.Abstractions.TelegramClient.Functions.Components;

namespace TgInstanceAgent.Application.Abstractions.TelegramClient.Functions.Messages;

/// <summary>
/// Запрос на пересылку сообщений.
/// </summary>
public class TgForwardMessagesRequest
{
    /// <summary>
    /// Чат
    /// </summary>
    public required TgInputChat Chat { get; init; }
    
    /// <summary>
    /// Идентификатор чата с которого идет пересылка сообщений
    /// </summary>
    public required long FromChatId { get; init; }
    
    /// <summary>
    /// Массив идентификаторо сообщений для пересылки
    /// </summary>
    public required long[] MessageIds { get; init; }
    
    /// <summary>
    /// Истинно, если требуется убрать подпись в медиа файлах
    /// </summary>
    public bool RemoveCaption { get; init; }

    /// <summary>
    /// Истинно, если требуется переслать сообщения без ссылок на пользователей этих сообщений
    /// </summary>
    public bool SendCopy { get; init; }
    
    /// <summary>
    /// Параметры отправки сообщения
    /// </summary>
    public TgInputMessageSendOptions? MessageSendOptions { get; init; }
}