using TgInstanceAgent.Application.Abstractions.TelegramClient.Objects.Components;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Objects.Enums;

namespace TgInstanceAgent.Application.Abstractions.TelegramClient.Objects.Stories;

/// <summary>
/// Представляет полную информацию об истории в телеграм
/// </summary>
public class TgStory
{
    /// <summary>
    /// Идентификатор истории
    /// </summary>
    public required long Id { get; init; }
    
    /// <summary>
    /// Контект истории
    /// </summary>
    public required TgStoryContent StoryContent { get; init; }
    
    /// <summary>
    /// Идентификатор отправителя истории(пользователя)
    /// </summary>
    public required long SenderChatId { get; init; }
    
    /// <summary>
    /// Настройки приватности истории
    /// </summary>
    public TgStoryPrivacySettings? StoryPrivacySettings { get; init; }
    
    /// <summary>
    /// Время поста истории
    /// </summary>
    public required DateTime Date { get; init; }
    
    /// <summary>
    /// Информация о взаимодействии с историей; может быть нулевой, если история не принадлежит аккаунту или взаимодействий не было.
    /// </summary>
    public TgStoryInteractionInfo? InteractionInfo { get; init; }
    
    /// <summary>
    /// Истинно, если историю отправляет текущий пользователь
    /// </summary>
    public required bool IsBeingSent { get; init; }
    
    /// <summary>
    /// Истинно, если историю редактирует текущий пользователь
    /// </summary>
    public required bool IsBeingEdited { get; init; }
    
    /// <summary>
    /// Истинно, если история была отредактирована
    /// </summary>
    public required bool IsEdited { get; init; }
    
    /// <summary>
    /// Истинно, если история была закреплена и будет доступна после истечения времени 
    /// </summary>
    public required bool IsPinned { get; init; }
    
    /// <summary>
    /// Истинно, если видна только владельцу истории
    /// </summary>
    public required bool IsVisibleOnlyForSelf { get; init; }
    
    /// <summary>
    /// Истинно, если может быть удалена
    /// </summary>
    public required bool CanBeDeleted { get; init; }
    
    /// <summary>
    /// Истинно, если может быть изменена
    /// </summary>
    public required bool CanBeEdited { get; init; }
    
    /// <summary>
    /// Истинно, если может быть переслана
    /// </summary>
    public required bool CanBeForwarded { get; init; }

    /// <summary>
    /// Истинно, если на историю можно ответить
    /// </summary>
    public required bool CanBeReplied { get; init; }
    
    /// <summary>
    /// Истинно если IsPinned можно менять
    /// </summary>
    public required bool CanToggleIsPinned { get; init; }
    
    /// <summary>
    /// Истинно, если можно получить пользователей просмотревщих историю
    /// </summary>
    public required bool CanGetViewers { get; init; }
    
    /// <summary>
    /// Подпись
    /// </summary>
    public TgFormatedText? Caption { get; init; }
}