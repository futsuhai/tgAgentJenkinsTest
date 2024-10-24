using TgInstanceAgent.Application.Abstractions.TelegramClient.Functions.Components;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Functions.Enums;

namespace TgInstanceAgent.Application.Abstractions.TelegramClient.Functions.Chats;

/// <summary>
/// Запрос на поиск сообщений в чате.
/// </summary>
public class TgSearchChatMessagesRequest
{
    /// <summary>
    /// Чат
    /// </summary>
    public required TgInputChat Chat { get; init; }
    
    /// <summary>
    /// Отправитель
    /// </summary>
    public TgInputMessageSender? MessageSender { get; init; }
    
    /// <summary>
    /// Строка с ключевыми словами для поиска.
    /// </summary>
    public string? Query { get; init; }
    
    /// <summary>
    /// Кол-во сообщений в запросе.
    /// </summary>
    public required int Limit { get; init; }
    
    /// <summary>
    /// Смещение
    /// </summary>
    public int? Offset { get; init; }
    
    /// <summary>
    /// Идентификатор сообщения начала истории чата.
    /// </summary>
    public long? FromMessageId { get; init; }
    
    /// <summary>
    /// Идентификатор потока сообщений (thread) в чате.
    /// Если указан, поиск будет выполнен только внутри этого потока.
    /// </summary>
    public long? MessageThreadId { get; init; }
    
    /// <summary>
    /// Идентификатор темы сохраненных сообщений.
    /// Используется для поиска сообщений внутри определенной темы сохраненных сообщений.
    /// </summary>
    public long? SavedMessagesTopicId { get; init; }
    
    /// <summary>
    /// Фильтр сообщений.
    /// </summary>
    public TgMessageFilterType? Filter { get; init; }
}