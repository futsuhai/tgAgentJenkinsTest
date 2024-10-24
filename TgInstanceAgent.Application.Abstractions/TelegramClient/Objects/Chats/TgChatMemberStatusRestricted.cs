namespace TgInstanceAgent.Application.Abstractions.TelegramClient.Objects.Chats;

/// <summary>
/// Статус участника чата - на участника наложены ограничения
/// </summary>
public class TgChatMemberStatusRestricted : TgChatMemberStatus
{
    /// <summary>
    /// Истинно, если пользователь является участником чата
    /// </summary>
    public required bool IsMember { get; init; }

    /// <summary>
    /// Время (в формате Unix timestamp), когда ограничения будут сняты с пользователя; 0, если никогда.
    /// Если пользователь ограничен более чем на 366 дней или менее чем на 30 секунд от текущего времени, пользователь считается ограниченным навсегда
    /// </summary>
    public required int RestrictedUntilDate { get; init; }

    /// <summary>
    /// Права пользователя в чате
    /// </summary>
    public required TgChatPermissions Permissions { get; init; }
}