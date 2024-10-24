using TgInstanceAgent.Infrastructure.Web.Components.Interfaces;

namespace TgInstanceAgent.Infrastructure.Web.Stories.InputModels;

/// <summary>
/// Модель входных данных для отправки истории с видео
/// </summary>
public class PostVideoStoryInputModel : IWithInputChat, IWithInputFile, IWithInputCaption, IWithInputStoryPrivacySettings
{
    /// <summary>
    /// Идентификатор чата.
    /// При отправке запроса по идентификатору, необходимо быть уверенным, что приложению известен данный чат.
    /// </summary>
    public long? ChatId { get; init; }

    /// <summary>
    /// Имя пользователя чата. Если установлено, Chat может быть не задан.
    /// </summary>
    public string? Username { get; init; }

    /// <summary>
    /// Номер телефона пользователя. Если установлено, Chat и Username могут быть не заданы.
    /// </summary>
    public string? PhoneNumber { get; init; }
    
    /// <summary>
    /// Файл
    /// </summary>
    public IFormFile? File { get; init; }
    
    /// <summary>
    /// Локальный идентификатор файла
    /// </summary>
    public int? LocalId { get; init; }

    /// <summary>
    /// Удаленный идентификатор файла
    /// </summary>
    public string? RemoteId { get; init; }

    /// <summary>
    /// Подпись к файловому сообщению.
    /// </summary>
    public string? Caption { get; init; }
    
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

    /// <summary>
    /// Период, в который будет доступна история (в часах)
    /// </summary>
    public int ActivePeriod { get; init; }
    
    /// <summary>
    /// Истинно, если история защищена от скриншотов и пересылок
    /// </summary>
    public bool ProtectContent { get; init; }
    
    /// <summary>
    /// Истинно, если видео не имеет звука
    /// </summary>
    public bool IsAnimation { get; init; }
}
    
