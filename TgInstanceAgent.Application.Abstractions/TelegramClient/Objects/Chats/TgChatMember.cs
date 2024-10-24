using TgInstanceAgent.Application.Abstractions.TelegramClient.Objects.Components;

namespace TgInstanceAgent.Application.Abstractions.TelegramClient.Objects.Chats;

/// <summary>
/// Участник группового чата
/// </summary>
public class TgChatMember
{
    /// <summary>
    /// Идентификатор участника чата
    /// </summary>
    public required TgMessageSender MemberId { get; init; }
    
    /// <summary>
    /// Идентификатор пользователя пригласившего участника в чат
    /// </summary>
    public required long InviterUserId { get; init; }
    
    /// <summary>
    /// Время (Unix timestamp), когда участник присоединился
    /// </summary>
    public required int JoinedChatDate { get; init; }
    
    /// <summary>
    /// Статус участника
    /// </summary>
    public required TgChatMemberStatus Status { get; init; }
}