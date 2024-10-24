using TgInstanceAgent.Application.Abstractions.TelegramClient.Functions.Enums;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Objects.Enums;

namespace TgInstanceAgent.Application.Abstractions.TelegramClient.Functions.Chats;

/// <summary>
/// Выполняет поиск сообщений во всех чатах, кроме секретных. Возвращает результаты в обратном хронологическом порядке.
/// </summary>
public class TgSearchMessagesRequest
{
    /// <summary>
    /// Строка с ключевыми словами для поиска.
    /// </summary>
    public required string Query { get; init; }
    
    /// <summary>
    /// Тип списка
    /// </summary>
    public TgChatList? List { get; init; }
    
    /// <summary>
    /// Кол-во сообщений в запросе.
    /// </summary>
    public required int Limit { get; init; }
    
    /// <summary>
    /// Смещение
    /// </summary>
    public string? Offset { get; init; }
    
    /// <summary>
    /// Укажите значение true для поиска только сообщений в каналах
    /// </summary>
    public bool OnlyInChannels { get; init; }
    
    /// <summary>
    /// Фильтр сообщений.
    /// </summary>
    public TgMessageFilterType? Filter { get; init; }
    
    /// <summary>
    /// Если значение не равно null, то минимальная дата возврата сообщений
    /// </summary>
    public DateTime? MinDate { get; init; }

    /// <summary>
    /// Если значение не равно null, то указывается максимальная дата возврата сообщений
    /// </summary>
    public DateTime? MaxDate { get; init; }
}