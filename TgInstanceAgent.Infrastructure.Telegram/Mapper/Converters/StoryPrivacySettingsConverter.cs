using AutoMapper;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Objects.Enums;
using static TdLib.TdApi;

namespace TgInstanceAgent.Infrastructure.Telegram.Mapper.Converters;

/// <summary>
/// Преобразует объект StoryPrivacySettings в TgStoryPrivacySettings.
/// </summary>
public class StoryPrivacySettingsConverter : ITypeConverter<StoryPrivacySettings, TgStoryPrivacySettings?>
{
    /// <summary>
    /// Преобразует объект StoryPrivacySettings в TgStoryPrivacySettings.
    /// </summary>
    /// <param name="source">Объект StoryPrivacySettings для преобразования.</param>
    /// <param name="destination">Целевой объект TgStoryPrivacySettings.</param>
    /// <param name="context">Контекст преобразования.</param>
    /// <returns>Преобразованный объект TgStoryPrivacySettings.</returns>
    public TgStoryPrivacySettings? Convert(StoryPrivacySettings source, TgStoryPrivacySettings? destination, ResolutionContext context)
    {
        // Возвращаем данные в зависимости от типа настроек приватности
        return source switch
        {
            // Если storyPrivacySettings является типом StoryPrivacySettingsEveryone
            StoryPrivacySettings.StoryPrivacySettingsEveryone => TgStoryPrivacySettings.EveryOne,

            // Если storyPrivacySettings является типом StoryPrivacySettingsContacts
            StoryPrivacySettings.StoryPrivacySettingsContacts => TgStoryPrivacySettings.Contacts,

            // Если storyPrivacySettings является типом StoryPrivacySettingsCloseFriends
            StoryPrivacySettings.StoryPrivacySettingsCloseFriends => TgStoryPrivacySettings.CloseFriends,

            // Если storyPrivacySettings является типом StoryPrivacySettingsSelectedUsers
            StoryPrivacySettings.StoryPrivacySettingsSelectedUsers => TgStoryPrivacySettings.SelectedUsers,

            // По умолчанию Null
            _ => null
        };
    }
}