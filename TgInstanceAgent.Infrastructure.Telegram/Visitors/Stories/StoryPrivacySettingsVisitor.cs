using TdLib;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Functions.Components;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Functions.Components.Visitors;

namespace TgInstanceAgent.Infrastructure.Telegram.Visitors.Stories;

/// <summary>
/// Посетитель настроек приватности истории
/// </summary>
public class StoryPrivacySettingsVisitor : IStoryPrivacySettingsVisitor
{
    /// <summary>
    /// Настройки приватности истории
    /// </summary>
    public TdApi.StoryPrivacySettings? StoryPrivacySettings { get; private set; }

    /// <summary>
    /// Метод посещения настроек приватности истории
    /// </summary>
    /// <param name="tgInputStoryPrivacySettingsSelectedUsers">Настройки приватности, позволяющие увидеть историю выбранным пользователям</param>
    public void Visit(TgInputStoryPrivacySettingsSelectedUsers tgInputStoryPrivacySettingsSelectedUsers)
    {
        // Устанавливаем настройки приватности
        StoryPrivacySettings = new TdApi.StoryPrivacySettings.StoryPrivacySettingsSelectedUsers
        {
            // Передаем массив идентификатор пользователей, которым предназначена история
            UserIds = tgInputStoryPrivacySettingsSelectedUsers.UserIds
        };
    }

    /// <summary>
    /// Метод посещения настроек приватности истории
    /// </summary>
    /// <param name="tgInputStoryPrivacySettingsCloseFriends">Настройки приватности, позволяющие увидеть историю пользователям, находящимся в списке близких друзей</param>
    public void Visit(TgInputStoryPrivacySettingsCloseFriends tgInputStoryPrivacySettingsCloseFriends)
    {
        // Устанавливаем настройки приватности
        StoryPrivacySettings = new TdApi.StoryPrivacySettings.StoryPrivacySettingsCloseFriends();
    }

    /// <summary>
    /// Метод посещения настроек приватности истории
    /// </summary>
    /// <param name="tgInputStoryPrivacySettingsEveryone">Настройки приватности, позволяющие увидеть историю всем</param>
    public void Visit(TgInputStoryPrivacySettingsEveryone tgInputStoryPrivacySettingsEveryone)
    {
        // Устанавливаем настройки приватности
        StoryPrivacySettings = new TdApi.StoryPrivacySettings.StoryPrivacySettingsEveryone
        {
            // Передаем масиив идентификатор пользователей, которые не увидят историю
            ExceptUserIds = tgInputStoryPrivacySettingsEveryone.ExceptUserIds
        };
    }

    /// <summary>
    /// Метод посещения настроек приватности истории
    /// </summary>
    /// <param name="tgInputStoryPrivacySettingsContacts"></param>
    public void Visit(TgInputStoryPrivacySettingsContacts tgInputStoryPrivacySettingsContacts)
    {
        // Устанавливаем настройки приватности
        StoryPrivacySettings = new TdApi.StoryPrivacySettings.StoryPrivacySettingsContacts
        {
            // Передаем масиив идентификатор пользователей, которые не увидят историю
            ExceptUserIds = tgInputStoryPrivacySettingsContacts.ExceptUserIds
        };
    }
}