namespace TgInstanceAgent.Application.Abstractions.TelegramClient.Objects.Chats;

/// <summary>
/// Статус участника чата - администратор
/// </summary>
public class TgChatMemberStatusAdministrator : TgChatMemberStatus
{
    /// <summary>
    /// Пользовательский заголовок администратора; от 0 до 16 символов без эмодзи; применимо только к супергруппам
    /// </summary>
    public string? CustomTitle { get; init; }
    
    /// <summary>
    /// Значение true, если текущий пользователь может редактировать права администратора для указанного пользователя
    /// </summary>
    public required bool CanBeEdited { get; init; }
    
    /// <summary>
    /// Права администратора
    /// </summary>
    public required TgChatAdministratorRights Rights { get; init; }
}