using MediatR;
using TgInstanceAgent.Application.Abstractions.Interfaces;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Objects.Stories;

namespace TgInstanceAgent.Application.Abstractions.Commands.TgStories;

/// <summary>
/// Команда для установки новой истории в виде фотографии
/// </summary>
public class PostPhotoStoryCommand : IWithChat, IWithFile, IWithStoryPrivacySettings, IWithInstanceId, IRequest<TgStory>
{
    /// <summary>
    /// Уникальный идентификатор инстанса.
    /// </summary>
    public required Guid InstanceId { get; init; }
    
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
    /// Файл.
    /// </summary>
    public FileData? File { get; init; }

    /// <summary>
    /// Локальный идентификатор файла
    /// </summary>
    public int? LocalId { get; init; }

    /// <summary>
    /// Удаленный идентификатор файла
    /// </summary>
    public string? RemoteId { get; init; }
    
    /// <summary>
    /// Подпись к изображению.
    /// </summary>
    public string? Caption { get; init; }

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
    
    /// <summary>
    /// Период, в который будет доступна история (в часах)
    /// </summary>
    public int ActivePeriod { get; init; }
    
    /// <summary>
    /// Истинно, если история защищена от скриншотов и пересылок
    /// </summary>
    public bool ProtectContent { get; init; }
}