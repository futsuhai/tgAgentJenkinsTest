using MediatR;
using TgInstanceAgent.Application.Abstractions.Interfaces;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Functions.Enums;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Objects.Components;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Objects.Enums;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Objects.Messages;

namespace TgInstanceAgent.Application.Abstractions.Queries.TgChats;

/// <summary>
/// Запрос на поиск сообщений во всех чатах, кроме секретных
/// </summary>
public class SearchMessagesQuery: IWithInstanceId, IRequest<TgFoundMessages>
{
    /// <summary>
    /// Уникальный идентификатор инстанса.
    /// </summary>
    public required Guid InstanceId { get; init; }
    
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