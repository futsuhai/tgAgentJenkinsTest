namespace TgInstanceAgent.Application.Abstractions.TelegramClient.Events.Visitors;

/// <summary>
/// Интерфейс посетителя событий Whatsapp
/// </summary>
public interface ITgEventVisitor
{
    /// <summary>
    /// Метод для посещения события аутентификации
    /// </summary>
    /// <param name="authenticatedEvent">Событие успешной аутентификации</param>
    void Visit(TgAuthenticatedEvent authenticatedEvent);

    /// <summary>
    /// Метод для посещения события изменения настроек срока действия сообщений
    /// </summary>
    /// <param name="autoDeleteTimeEvent">Событие изменения настроек срока действия сообщений</param>
    void Visit(TgUpdateChatMessageAutoDeleteTimeEvent autoDeleteTimeEvent);

    /// <summary>
    /// Метод для посещения события выхода из аккаунта
    /// </summary>
    /// <param name="loggedOutEvent">Событие выхода из аккаунта</param>
    void Visit(TgLoggedOutEvent loggedOutEvent);

    /// <summary>
    /// Метод для посещения события изменения закрепленного сообщения
    /// </summary>
    /// <param name="pinnedMessageEvent">Событие изменения закрепленного сообщения</param>
    void Visit(TgMessagePinnedEvent pinnedMessageEvent);

    /// <summary>
    /// Метод для посещения события отзыва сообщения
    /// </summary>
    /// <param name="deletedMessagesEvent">Событие отзыва сообщений</param>
    void Visit(TgMessagesDeletedEvent deletedMessagesEvent);

    /// <summary>
    /// Метод для посещения события изменения названия чата
    /// </summary>
    /// <param name="chatTitleEvent">Событие изменения названия чата</param>
    void Visit(TgUpdateChatTitleEvent chatTitleEvent);

    /// <summary>
    /// Метод для посещения события изменения изображения чата
    /// </summary>
    /// <param name="chatPhotoEvent">Событие изменения изображения чата</param>
    void Visit(TgUpdateChatPhotoEvent chatPhotoEvent);

    /// <summary>
    /// Метод для посещения события нового сообщения
    /// </summary>
    /// <param name="newMessageEvent">Событие нового сообщения</param>
    void Visit(TgNewMessageEvent newMessageEvent);

    /// <summary>
    /// Метод для посещения события автопересылки сообщения
    /// </summary>
    /// <param name="messageAutoForwardedEvent">Событие автопересылки сообщения</param>
    void Visit(TgMessageAutoForwardedEvent messageAutoForwardedEvent);

    /// <summary>
    /// Метод для посещения события отметки чата прочитанным/непрочитанным
    /// </summary>
    /// <param name="updateChatUnreadEvent">Событие отметки чата прочитанным/непрочитанным</param>
    void Visit(TgUpdateChatMarkedAsUnreadEvent updateChatUnreadEvent);

    /// <summary>
    /// Метод для посещения события редактирования сообщения
    /// </summary>
    /// <param name="updateMessageContentEvent">Событие редактирования сообщения</param>
    void Visit(TgUpdateMessageContent updateMessageContentEvent);

    /// <summary>
    /// Метод для посещения события просмотра живой локации сообщения
    /// </summary>
    /// <param name="liveLocationViewedEvent">Событие просмотра живой локации сообщения</param>
    void Visit(TgMessageLiveLocationViewedEvent liveLocationViewedEvent);
    
    /// <summary>s
    /// Метод для посещения события удаления истории
    /// </summary>
    /// <param name="storyDeletedEvent">Событие удаления истории</param>
    void Visit(TgStoryDeletedEvent storyDeletedEvent);

    /// <summary>
    /// Метод для посещения события обновления списка заблокированных участников чата
    /// </summary>
    /// <param name="chatBlockListEvent">Событие обновления списка заблокированных участников чата</param>
    void Visit(TgUpdateChatBlockListEvent chatBlockListEvent);

    /// <summary>
    /// Метод для посещения события обновления статуса эмодзи чата
    /// </summary>
    /// <param name="chatEmojiStatusEvent">Событие обновления статуса эмодзи чата</param>
    void Visit(TgUpdateChatEmojiStatusEvent chatEmojiStatusEvent);

    /// <summary>
    /// Метод для посещения события обновления количества участников онлайн в чате
    /// </summary>
    /// <param name="chatOnlineMemberCountEvent">Событие обновления количества участников онлайн в чате</param>
    void Visit(TgUpdateChatOnlineMemberCountEvent chatOnlineMemberCountEvent);

    /// <summary>
    /// Метод для посещения события обновления прав доступа в чате
    /// </summary>
    /// <param name="chatPermissionsEvent">Событие обновления прав доступа в чате</param>
    void Visit(TgUpdateChatPermissionsEvent chatPermissionsEvent);

    /// <summary>
    /// Метод для посещения события обновления защищенного контента в чате
    /// </summary>
    /// <param name="chatProtectedContentEvent">Событие обновления защищенного контента в чате</param>
    void Visit(TgUpdateChatProtectedContentEvent chatProtectedContentEvent);

    /// <summary>
    /// Метод для посещения события удаления чата из списка
    /// </summary>
    /// <param name="chatRemovedEvent">Событие удаления чата из списка</param>
    void Visit(TgChatRemovedFromListEvent chatRemovedEvent);

    /// <summary>
    /// Метод для посещения события обновления настроек запланированных сообщений
    /// </summary>
    /// <param name="chatScheduledMessagesEvent">Событие обновления настроек запланированных сообщений</param>
    void Visit(TgUpdateChatScheduledMessagesEvent chatScheduledMessagesEvent);

    /// <summary>
    /// Метод для посещения события обновления упоминаний в непрочитанных сообщениях
    /// </summary>
    /// <param name="chatUnreadMentionEvent">Событие обновления упоминаний в непрочитанных сообщениях</param>
    void Visit(TgUpdateChatUnreadMentionEvent chatUnreadMentionEvent);

    /// <summary>
    /// Метод для посещения события обновления реакций в непрочитанных сообщениях
    /// </summary>
    /// <param name="chatUnreadReactionEvent">Событие обновления реакций в непрочитанных сообщениях</param>
    void Visit(TgUpdateChatUnreadReactionEvent chatUnreadReactionEvent);

    /// <summary>
    /// Метод для посещения события обновления количества загрузок файла
    /// </summary>
    /// <param name="fileDownloadsEvent">Событие обновления количества загрузок файла</param>
    void Visit(TgUpdateFileDownloadsEvent fileDownloadsEvent);

    /// <summary>
    /// Метод для посещения события обновления статуса пользователя
    /// </summary>
    /// <param name="userStatusEvent">Событие обновления статуса пользователя</param>
    void Visit(TgUpdateUserStatusEvent userStatusEvent);

    /// <summary>
    /// Метод для посещения события действия в чате
    /// </summary>
    /// <param name="chatActionEvent">Событие действия в чате</param>
    void Visit(TgChatActionEvent chatActionEvent);

    /// <summary>
    /// Метод для посещения обновления фона чата
    /// </summary>
    void Visit(TgUpdateChatBackgroundEvent chatBackgroundEvent);

    /// <summary>
    /// Метод для посещения события обновления последнего сообщения чата
    /// </summary>
    void Visit(TgUpdateChatLastMessageEvent chatLastMessageEvent);

    /// <summary>
    /// Метод для посещения события изменения непрочитанных сообщений
    /// </summary>
    void Visit(TgUpdateChatReadInboxEvent chatReadInboxEvent);
    
    /// <summary>
    /// Метод для посещения события обновления историй
    /// </summary>
    void Visit(TgUpdateChatActiveStories chatActiveStories);
    
    /// <summary>
    /// Метод для посещения события изменения позиции чата
    /// </summary>
    void Visit(TgUpdateChatPositionEvent chatPositionEvent);
    
    /// <summary>
    /// Метод для посещения события создания нового чата
    /// </summary>
    void Visit(TgNewChatEvent chatPositionEvent);
    
    /// <summary>
    /// Метод для посещения события загрузки файла
    /// </summary>
    void Visit(TgUpdateFileEvent updateFileEvent);

    /// <summary>
    /// Метод для посещения события обновления папок чатов
    /// </summary>
    void Visit(TgUpdateChatFoldersEvent chatOnlineMemberCountEvent);

    /// <summary>
    /// Метод для посещения события обновления числа непрочитанных чатов
    /// </summary>
    void Visit(TgUpdateUnreadChatCount chatOnlineMemberCountEvent);
    
    /// <summary>
    /// Метод для посещения события обновления числа непрочитанных сообщений
    /// </summary>
    void Visit(TgUpdateUnreadMessageCount chatOnlineMemberCountEvent);
    
    /// <summary>
    /// Метод для посещения события обновления информации о базовом групповом чате
    /// </summary>
    void Visit(TgUpdateBasicGroupFullInfo updateBasicGroupFullInfo);
    
    /// <summary>
    /// Метод для посещения события обновления информации о настройках уведомлений в чате
    /// </summary>
    void Visit(TgUpdateChatNotificationSettings updateChatNotificationSettings);
    
    /// <summary>
    /// Метод для посещения события обновления информации об изменении акцентого цвета в чате
    /// </summary>
    void Visit(TgUpdateChatAccentColors updateChatAccentColors);
    
    /// <summary>
    /// Метод для посещения события обновления информации об автоудаении сообщений в чате
    /// </summary>
    void Visit(TgUpdateChatMessageAutoDeleteTime updateChatMessageAutoDeleteTime);
    
    /// <summary>
    /// Метод для посещения события обновления информации об изменении списка доступных в чате реакций
    /// </summary>
    void Visit(TgUpdateChatAvailableReactions updateChatAvailableReactions);
    
    /// <summary>
    /// Метод для посещения события обновления отправителя сообщений в групповом чате
    /// </summary>
    void Visit(TgUpdateChatMessageSender updateChatMessageSender);
}