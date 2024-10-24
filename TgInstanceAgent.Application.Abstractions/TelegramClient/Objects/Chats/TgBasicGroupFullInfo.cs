using TgInstanceAgent.Application.Abstractions.TelegramClient.Objects.Components;

namespace TgInstanceAgent.Application.Abstractions.TelegramClient.Objects.Chats;

/// <summary>
/// Содержит информацию о базовом групповом чате
/// </summary>
public class TgBasicGroupFullInfo
{
    /// <summary>
    /// Фото чата
    /// </summary>
    public TgChatPhoto? Photo { get; init; }
    
    /// <summary>
    /// Описание чата
    /// </summary>
    public string? Description { get; init; }
    
    /// <summary>
    /// Идентификатор создателя чата, 0 если создатель неизвестен
    /// </summary>
    public required long CreatorUserId { get; init; }
    
    /// <summary>
    /// Участники чата
    /// </summary>
    public required TgChatMember[] Members { get; init; }
    
    /// <summary>
    /// Истина, если неадминистраторы и неботы могут быть скрыты в ответах на запросы getSupergroupMembers и searchChatMembers
    /// для неадминистраторов после обновления обычной группы до супергруппы
    /// </summary>
    public required bool CanHideMembers { get; init; }
    
    /// <summary>
    /// Истина, если агрессивные проверки на спам могут быть включены или отключены в супергруппе после обновления обычной группы до супергруппы
    /// </summary>
    public required bool CanToggleAggressiveAntiSpam { get; init; }
    
    /// <summary>
    /// Основная ссылка-приглашение для этой группы;
    /// </summary>
    public required TgChatInviteLink InviteLink { get; init; }
    
    /// <summary>
    /// Список команд ботов в группе
    /// </summary>
    public required TgBotCommands[] BotCommands { get; init; }
}