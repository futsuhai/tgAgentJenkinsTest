using TgInstanceAgent.Application.Abstractions.Interfaces;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Exceptions;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Functions.Components;

namespace TgInstanceAgent.Application.Services.Extensions;

/// <summary>
/// Класс, предоставляющий методы для преобразования настроек приватности истории.
/// </summary>
public static class StoryPrivacySettingsMapper
{
    /// <summary>
    /// Метод получения настроек приватности истории
    /// </summary>
    /// <param name="command">Команда, содержащая настройки приватности сети</param>
    /// <returns>Настройки приватности истории</returns>
    /// <exception cref="InvalidStoryPrivacySettingsException">Вызывается, если не определен тип настроек приватности истории</exception>
    public static TgInputStoryPrivacySettings GetStoryPrivacySettings(this IWithStoryPrivacySettings command) 
    {
        // Если массив идентификаторов пользователей в настройках приватности для контактов не null
        if (command.StoryPrivacySettingsContacts != null) 
        {
            // Возвращаем настройки приватности, позволяющие увидеть историю только контактам пользователя
            return new TgInputStoryPrivacySettingsContacts
            {
                // Массив идентификаторов пользователей, которые не увидят историю
                ExceptUserIds = command.StoryPrivacySettingsContacts.ExceptUserIds!
            };
        }

        // Если массив идентификаторов пользователей в настройках приватности для всех не null
        if (command.StoryPrivacySettingsEveryone != null)
        {
            // Возвращаем настройки приватности, позволяющие увидеть историю всем
            return new TgInputStoryPrivacySettingsEveryone
            {
                // Массив идентификаторов пользователей, которые не увидят историю
                ExceptUserIds = command.StoryPrivacySettingsEveryone.ExceptUserIds!
            };
        }
        
        // Если массив идентификаторов пользователей в настройках приватности для выбранных пользователей не null
        if (command.StoryPrivacySettingsSelectedUsers != null)
        {
            // Возвращаем настройки приватности, позволяющие увидеть историю выбранным пользователям
            return new TgInputStoryPrivacySettingsSelectedUsers
            {
                // Массив идентификаторов пользователей, для которых предназначена история
                UserIds = command.StoryPrivacySettingsSelectedUsers.UserIds!
            };
        }
        
        // Если настройки приватности истории для близких друзей не Null
        if (command.StoryPrivacySettingsCloseFriends != null)
        {
            // Возвращаем Настройки приватности, позволяющие увидеть историю пользователям, находящимся в списке близких друзей
            return new TgInputStoryPrivacySettingsCloseFriends();
        }
        
        // Вызываем исключение, если не удалось определить тип фона.
        throw new InvalidStoryPrivacySettingsException(); 
    }
}