namespace TgInstanceAgent.Application.Abstractions.TelegramClient.Objects.Chats;

/// <summary>
/// Ссылка приглашение для группового чата
/// </summary>
public class TgChatInviteLink
{
    /// <summary>
    /// Ссылка-приглашение в чат
    /// </summary>
    public required string InviteLink { get; init; }
    
    /// <summary>
    /// Название ссылки
    /// </summary>
    public required string Name { get; init; }
    
    /// <summary>
    /// Идентификатор пользователя-администратора, создавшего ссылку
    /// </summary>
    public required long CreatorUserId { get; init; }
    
    /// <summary>
    /// Время (Unix timestamp), когда была создана ссылка
    /// </summary>
    public required int Date { get; init; }

    /// <summary>
    /// Время (Unix timestamp), когда ссылка была отредактирована в последний раз; 0, если никогда или неизвестно
    /// </summary>
    public required int EditDate { get; init; }

    /// <summary>
    /// Время (Unix timestamp), когда ссылка истечет; 0, если никогда
    /// </summary>
    public required int ExpirationDate { get; init; }
    
    /// <summary>
    /// Максимальное количество участников, которые могут одновременно присоединиться к чату по этой ссылке;
    /// 0, если не ограничено. Всегда 0, если ссылка требует одобрения
    /// </summary>
    public required int MemberLimit { get; init; }

    /// <summary>
    /// Количество участников чата, которые присоединились к чату с использованием этой ссылки
    /// </summary>
    public required int MemberCount { get; init; }

    /// <summary>
    /// Количество ожидающих запросов на присоединение, созданных с использованием этой ссылки
    /// </summary>
    public required int PendingJoinRequestCount { get; init; }

    /// <summary>
    /// Истинно, если ссылка создаёт только запрос на присоединение.
    /// Если true, общее количество присоединившихся участников будет неограниченным
    /// </summary>
    public required bool CreatesJoinRequest { get; init; }
    
    /// <summary>
    /// Истинно, если ссылка является основной.
    /// Основная ссылка-приглашение не может иметь название, дату истечения или ограничение на использование.
    /// </summary>
    public required bool IsPrimary { get; init; }

    /// <summary>
    /// Истинно, если ссылка была отозвана
    /// </summary>
    public required bool IsRevoked { get; init; }
}