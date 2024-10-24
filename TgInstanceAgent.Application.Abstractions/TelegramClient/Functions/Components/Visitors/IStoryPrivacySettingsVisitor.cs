namespace TgInstanceAgent.Application.Abstractions.TelegramClient.Functions.Components.Visitors;

/// <summary>
/// Интерфейс посетителя настроек приватности истории
/// </summary>
public interface IStoryPrivacySettingsVisitor
{
    /// <summary>
    /// Посетить настройки приватности, позволяющие увидеть историю выбранным пользователям.
    /// </summary>
    /// <param name="tgInputStoryPrivacySettingsSelectedUsers">Настройки приватности, позволяющие увидеть историю выбранным пользователям.</param>
    void Visit(TgInputStoryPrivacySettingsSelectedUsers tgInputStoryPrivacySettingsSelectedUsers);

    /// <summary>
    /// Посетить настройки приватности, позволяющие увидеть историю близким друзьям.
    /// </summary>
    /// <param name="tgInputStoryPrivacySettingsCloseFriends">Настройки приватности, позволяющие увидеть историю близким друзьям.</param>
    void Visit(TgInputStoryPrivacySettingsCloseFriends tgInputStoryPrivacySettingsCloseFriends);

    /// <summary>
    /// Посетить настройки приватности, позволяющие увидеть историю всем пользователям.
    /// </summary>
    /// <param name="tgInputStoryPrivacySettingsEveryone">Настройки приватности, позволяющие увидеть историю всем пользователям.</param>
    void Visit(TgInputStoryPrivacySettingsEveryone tgInputStoryPrivacySettingsEveryone);

    /// <summary>
    /// Посетить настройки приватности, позволяющие увидеть историю контактам пользователя.
    /// </summary>
    /// <param name="tgInputStoryPrivacySettingsContacts">Настройки приватности, позволяющие увидеть историю контактам пользователя.</param>
    void Visit(TgInputStoryPrivacySettingsContacts tgInputStoryPrivacySettingsContacts);
}