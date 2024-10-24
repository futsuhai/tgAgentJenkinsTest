using MediatR;
using TgInstanceAgent.Application.Abstractions.Interfaces;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Objects.Chats;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Objects.Enums;

namespace TgInstanceAgent.Application.Abstractions.Queries.TgChats;

/// <summary>
/// Запрос на получение чатов, который представляет собой запрос к инстансу.
/// Результат запроса - коллекция чатов.
/// </summary>
public class GetChatsQuery : IWithInstanceId, IRequest<TelegramClient.Objects.Chats.TgChats>
{
    /// <summary>
    /// Уникальный идентификатор инстанса.
    /// </summary>
    public required Guid InstanceId { get; init; }
    
    /// <summary>
    /// Тип списка
    /// </summary>
    public required TgChatList List { get; init; }
    
    /// <summary>
    /// Идентификатор папки
    /// </summary>
    public int? ChatFolderId { get; init; }
    
    /// <summary>
    /// Кол-во чатов в запросе.
    /// </summary>
    public required int Limit { get; init; }
    
    /// <summary>
    /// Смещение чатов
    /// </summary>
    public int? Offset { get; init; }
    
    /// <summary>
    /// Начиная с какого идентификатора будут возвращаться чаты. 
    /// </summary>
    public long? FromChatId { get; init; }
    
    /// <summary>
    /// Флаг, необходимо ли включить в ответ данные пользователей
    /// </summary>
    public required bool IncludeUsers { get; init; }
}