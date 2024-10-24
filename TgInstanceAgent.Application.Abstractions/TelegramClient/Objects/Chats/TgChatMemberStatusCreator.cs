namespace TgInstanceAgent.Application.Abstractions.TelegramClient.Objects.Chats;

/// <summary>
/// Статус участника чата - создатель
/// </summary>
public class TgChatMemberStatusCreator : TgChatMemberStatus
{
    /// <summary>
    /// Пользовательский титул владельца; от 0 до 16 символов без эмодзи;
    /// Применимо только к супергруппам
    /// </summary>
    public required string CustomTitle { get; init; }

    /// <summary>
    /// Истинно, если создатель не отображается в списке участников чата и отправляет сообщения анонимно;
    /// Применимо только к супергруппам
    /// </summary>
    public required bool IsAnonymous { get; init; }

    /// <summary>
    ///  Истинно, если пользователь является участником чата
    /// </summary>
    public required bool IsMember { get; init; }
}