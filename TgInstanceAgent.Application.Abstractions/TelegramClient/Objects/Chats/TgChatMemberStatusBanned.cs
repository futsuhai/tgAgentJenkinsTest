namespace TgInstanceAgent.Application.Abstractions.TelegramClient.Objects.Chats;

/// <summary>
/// Статус участника чата - заблокирован
/// </summary>
public class TgChatMemberStatusBanned : TgChatMemberStatus
{
    /// <summary>
    /// Время (Unix timestamp), когда пользователь будет разблокирован; 0, если никогда. 
    /// Если пользователь заблокирован более чем на 366 дней или менее чем на 30 секунд с текущего момента, 
    /// он считается заблокированным навсегда. Всегда 0 в базовых группах
    /// </summary>
    public required int BannedUntilDate { get; init; }
}