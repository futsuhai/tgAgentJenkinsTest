namespace TgInstanceAgent.Infrastructure.Web.Components.Interfaces;

/// <summary>
/// Интерфейс входных данных, содеращий в себе свойства на выбор
/// Для настройки приватности истории
/// </summary>
public interface IWithInputStoryPrivacySettings
{
    /// <summary>
    /// Настройки приватности, позволяющие увидеть историю контактам пользователя
    /// </summary>
    public StoryPrivacySettingsContactsInputModel? StoryPrivacySettingsContacts { get; init; }
    
    /// <summary>
    /// Настройки приватности, позволяющие увидеть историю всем пользователям
    /// </summary>
    public StoryPrivacySettingsEveryoneInputModel? StoryPrivacySettingsEveryone { get; init; }
    
    /// <summary>
    /// Настройки приватности, позволяющие увидеть историю выбранным пользователям
    /// </summary>
    public StoryPrivacySettingsSelectedUsersInputModel? StoryPrivacySettingsSelectedUsers { get; init; }
    
    /// <summary>
    /// Настройки приватности, позволяющие увидеть историю пользователям, находящимся в списке близких друзей
    /// </summary>
    public StoryPrivacySettingsCloseFriendsInputModel? StoryPrivacySettingsCloseFriends { get; init; }
}

/// <summary>
/// Настройки приватности, позволяющие увидеть историю всем пользователям
/// </summary>
public class StoryPrivacySettingsEveryoneInputModel
{
    /// <summary>
    /// Массив идентификаторов пользователей, который не должны увидеть историю
    /// </summary>
    public long[]? ExceptUserIds { get; set; }
}

/// <summary>
/// Настройки приватности, позволяющие увидеть историю выбранным пользователям
/// </summary>
public class StoryPrivacySettingsSelectedUsersInputModel
{
    /// <summary>
    /// Массив идентификаторов пользователей, который увидят историю
    /// </summary>
    public long[]? UserIds { get; init; }
}

/// <summary>
/// Настройки приватности, позволяющие увидеть историю контактам пользователя
/// </summary>
public class StoryPrivacySettingsContactsInputModel
{
    /// <summary>
    /// Массив идентификаторов пользователей, который не должны увидеть историю
    /// </summary>
    public long[]? ExceptUserIds { get; set; }
}

/// <summary>
/// Настройки приватности, позволяющие увидеть историю пользователям, находящимся в списке близких друзей
/// </summary>
public class StoryPrivacySettingsCloseFriendsInputModel;