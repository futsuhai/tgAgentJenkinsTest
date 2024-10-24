namespace TgInstanceAgent.Application.Abstractions.TelegramClient.Objects.Enums;

/// <summary>
/// Перечисление для настроек приватности
/// </summary>
public enum TgStoryPrivacySettings
{
    /// <summary>
    /// Для близких друзей
    /// </summary>
    CloseFriends,
    
    /// <summary>
    /// Для контактов
    /// </summary>
    Contacts,
    
    /// <summary>
    /// Для всех
    /// </summary>
    EveryOne,
    
    /// <summary>
    /// Для выбранных пользователей
    /// </summary>
    SelectedUsers
}