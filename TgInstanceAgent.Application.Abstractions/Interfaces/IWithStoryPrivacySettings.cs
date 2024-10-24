namespace TgInstanceAgent.Application.Abstractions.Interfaces;

/// <summary>
/// Интерфейс, определяющий настройки приватностии истории.
/// </summary>
public interface IWithStoryPrivacySettings
{
    /// <summary>
    /// Настройки приватности, позволяющие увидеть историю контактам пользователя
    /// </summary>
    public StoryPrivacySettingsContactsData? StoryPrivacySettingsContacts { get; init; }
    
    /// <summary>
    /// Настройки приватности, позволяющие увидеть историю всем пользователям
    /// </summary>
    public StoryPrivacySettingsEveryoneData? StoryPrivacySettingsEveryone { get; init; }
    
    /// <summary>
    /// Настройки приватности, позволяющие увидеть историю выбранным пользователям
    /// </summary>
    public StoryPrivacySettingsSelectedUsersData? StoryPrivacySettingsSelectedUsers { get; init; }
    
    /// <summary>
    /// Настройки приватности, позволяющие увидеть историю пользователям, находящимся в списке близких друзей
    /// </summary>
    public StoryPrivacySettingsCloseFriendsData? StoryPrivacySettingsCloseFriends { get; init; }
}

/// <summary>
/// Настройки приватности, позволяющие увидеть историю контактам пользователя
/// </summary>
public class StoryPrivacySettingsContactsData
{
    /// <summary>
    /// Массив идентификаторов пользователей, которые не увидят историю
    /// </summary>
    public long[]? ExceptUserIds { get; init; }
}
    
/// <summary>
/// Настройки приватности, позволяющие увидеть историю всем пользователям
/// </summary>
public class StoryPrivacySettingsEveryoneData
{
    /// <summary>
    /// Массив идентификаторов пользователей, которые не увидят историю
    /// </summary>
    public long[]? ExceptUserIds { get; init; }
}
    
/// <summary>
/// Настройки приватности, позволяющие увидеть историю выбранным пользователям
/// </summary>
public class StoryPrivacySettingsSelectedUsersData
{
    /// <summary>
    /// Массив идентификаторов пользователей, для которых предназначена эта история
    /// </summary>
    public long[]? UserIds { get; init; }
}

/// <summary>
/// Настройки приватности, позволяющие увидеть историю пользователям, находящимся в списке близких друзей
/// </summary>
public class StoryPrivacySettingsCloseFriendsData;


