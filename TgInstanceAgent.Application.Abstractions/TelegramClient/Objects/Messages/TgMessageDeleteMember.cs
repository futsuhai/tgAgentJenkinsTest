namespace TgInstanceAgent.Application.Abstractions.TelegramClient.Objects.Messages;

/// <summary>
/// Специальное сообщение о исключении пользователя
/// </summary>
public class TgMessageDeleteMember : TgMessageContent
{
    /// <summary>
    /// Идентификатор исключённого пользователя
    /// </summary>
    public required long UserId { get; init; }
}