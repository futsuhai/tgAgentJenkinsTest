namespace TgInstanceAgent.Application.Abstractions.TelegramClient.Objects.Messages;

/// <summary>
/// Специальное сообщение о добавлении пользователей
/// </summary>
public class TgMessageAddMembers : TgMessageContent
{
    /// <summary>
    /// Идентификаторы добавленных пользователей
    /// </summary>
    public required long[] MemberUserIds { get; init; }
}