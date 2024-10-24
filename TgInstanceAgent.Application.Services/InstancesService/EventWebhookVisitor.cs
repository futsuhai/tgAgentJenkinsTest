using TgInstanceAgent.Application.Abstractions.TelegramClient.Events;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Events.Visitors;
using TgInstanceAgent.Domain.Instances.ValueObjects;

namespace TgInstanceAgent.Application.Services.InstancesService;

/// <inheritdoc/>
/// <summary>
/// Класс для посещения событий вебхука
/// </summary>
public class EventWebhookVisitor(WebhookSetting setting) : ITgEventVisitor
{
    /// <summary>
    /// Флаг необходимости отправки события
    /// </summary>
    public bool NeedSend { get; private set; }

    /// <inheritdoc/>
    /// <summary>
    /// Метод для посещения события аутентификации
    /// </summary>
    public void Visit(TgAuthenticatedEvent authenticatedEvent) => NeedSend = setting.Other;

    /// <inheritdoc/>
    /// <summary>
    /// Метод для посещения события изменения настроек срока действия сообщений
    /// </summary>
    public void Visit(TgUpdateChatMessageAutoDeleteTimeEvent autoDeleteTimeEvent) =>
        NeedSend = setting.Chats;

    /// <inheritdoc/>
    /// <summary>
    /// Метод для посещения события выхода из аккаунта
    /// </summary>
    public void Visit(TgLoggedOutEvent loggedOutEvent) => NeedSend = setting.Other;

    /// <inheritdoc/>
    /// <summary>
    /// Метод для посещения события изменения закрепленного сообщения
    /// </summary>
    public void Visit(TgMessagePinnedEvent pinnedMessageEvent) => NeedSend = setting.Messages;

    /// <inheritdoc/>
    /// <summary>
    /// Метод для посещения события отзыва сообщений
    /// </summary>
    public void Visit(TgMessagesDeletedEvent deletedMessagesEvent) => NeedSend = setting.Messages;

    /// <inheritdoc/>
    /// <summary>
    /// Метод для посещения события изменения названия чата
    /// </summary>
    public void Visit(TgUpdateChatTitleEvent chatTitleEvent) => NeedSend = setting.Chats;

    /// <inheritdoc/>
    /// <summary>
    /// Метод для посещения события изменения изображения чата
    /// </summary>
    public void Visit(TgUpdateChatPhotoEvent chatPhotoEvent) => NeedSend = setting.Chats;

    /// <inheritdoc/>
    /// <summary>
    /// Метод для посещения события нового сообщения
    /// </summary>
    public void Visit(TgNewMessageEvent newMessageEvent) =>
        NeedSend = !newMessageEvent.Message.MessageThreadId.HasValue && setting.Messages;

    /// <inheritdoc/>
    /// <summary>
    /// Метод для посещения события автоматической пересылки сообщения
    /// </summary>
    public void Visit(TgMessageAutoForwardedEvent messageAutoForwardedEvent) => NeedSend = setting.Messages;

    /// <inheritdoc/>
    /// <summary>
    /// Метод для посещения события отметки чата прочитанным/непрочитанным
    /// </summary>
    public void Visit(TgUpdateChatMarkedAsUnreadEvent updateChatUnreadEvent) => NeedSend = setting.Chats;

    /// <inheritdoc/>
    /// <summary>
    /// Метод для посещения события редактирования сообщения
    /// </summary>
    public void Visit(TgUpdateMessageContent updateMessageContentEvent) => NeedSend = setting.Messages;

    /// <inheritdoc/>
    /// <summary>
    /// Метод для посещения события просмотра живой локации сообщения
    /// </summary>
    public void Visit(TgMessageLiveLocationViewedEvent liveLocationViewedEvent) => NeedSend = setting.Messages;

    /// <inheritdoc/>
    /// <summary>
    /// Метод для посещения события удаления истории
    /// </summary>
    public void Visit(TgStoryDeletedEvent storyDeletedEvent) => NeedSend = setting.Stories;

    /// <inheritdoc/>
    /// <summary>
    /// Метод для посещения события обновления списка заблокированных участников чата
    /// </summary>
    public void Visit(TgUpdateChatBlockListEvent chatBlockListEvent) => NeedSend = setting.Chats;

    /// <inheritdoc/>
    /// <summary>
    /// Метод для посещения события обновления статуса эмодзи чата
    /// </summary>
    public void Visit(TgUpdateChatEmojiStatusEvent chatEmojiStatusEvent) => NeedSend = setting.Chats;

    /// <inheritdoc/>
    /// <summary>
    /// Метод для посещения события обновления количества участников онлайн в чате
    /// </summary>
    public void Visit(TgUpdateChatOnlineMemberCountEvent chatOnlineMemberCountEvent) => NeedSend = false;

    /// <inheritdoc/>
    /// <summary>
    /// Метод для посещения события обновления прав доступа в чате
    /// </summary>
    public void Visit(TgUpdateChatPermissionsEvent chatPermissionsEvent) => NeedSend = setting.Chats;

    /// <inheritdoc/>
    /// <summary>
    /// Метод для посещения события обновления защищенного контента в чате
    /// </summary>
    public void Visit(TgUpdateChatProtectedContentEvent chatProtectedContentEvent) => NeedSend = setting.Chats;

    /// <inheritdoc/>
    /// <summary>
    /// Метод для посещения события удаления чата из списка
    /// </summary>
    public void Visit(TgChatRemovedFromListEvent chatRemovedEvent) => NeedSend = setting.Chats;

    /// <inheritdoc/>
    /// <summary>
    /// Метод для посещения события обновления настроек запланированных сообщений
    /// </summary>
    public void Visit(TgUpdateChatScheduledMessagesEvent chatScheduledMessagesEvent) => NeedSend = setting.Chats;

    /// <inheritdoc/>
    /// <summary>
    /// Метод для посещения события обновления упоминаний в непрочитанных сообщениях
    /// </summary>
    public void Visit(TgUpdateChatUnreadMentionEvent chatUnreadMentionEvent) => NeedSend = false;

    /// <inheritdoc/>
    /// <summary>
    /// Метод для посещения события обновления реакций в непрочитанных сообщениях
    /// </summary>
    public void Visit(TgUpdateChatUnreadReactionEvent chatUnreadReactionEvent) => NeedSend = false;

    /// <inheritdoc/>
    /// <summary>
    /// Метод для посещения события обновления количества загрузок файла
    /// </summary>
    public void Visit(TgUpdateFileDownloadsEvent fileDownloadsEvent) => NeedSend = false;

    /// <inheritdoc/>
    /// <summary>
    /// Метод для посещения события обновления статуса пользователя
    /// </summary>
    public void Visit(TgUpdateUserStatusEvent userStatusEvent) => NeedSend = false;

    /// <inheritdoc/>
    /// <summary>
    /// Метод для посещения события действия в чате
    /// </summary>
    public void Visit(TgChatActionEvent chatActionEvent) => NeedSend = false;

    /// <summary>
    /// Метод для посещения обновления фона чата
    /// </summary>
    public void Visit(TgUpdateChatBackgroundEvent chatBackgroundEvent) => NeedSend = false;

    /// <summary>
    /// Метод для посещения события обновления последнего сообщения чата
    /// </summary>
    public void Visit(TgUpdateChatLastMessageEvent chatLastMessageEvent) => NeedSend = false;

    /// <summary>
    /// Метод для посещения события изменения непрочитанных сообщений
    /// </summary>
    public void Visit(TgUpdateChatReadInboxEvent chatReadInboxEvent) => NeedSend = false;

    /// <summary>
    /// Метод для посещения события изменения обновления историй
    /// </summary>
    public void Visit(TgUpdateChatActiveStories chatActiveStories) => NeedSend = setting.Stories;

    /// <summary>
    /// Метод для посещения события изменения позиции чата
    /// </summary>
    public void Visit(TgUpdateChatPositionEvent chatPositionEvent) => NeedSend = false;

    /// <summary>
    /// Метод для посещения события создания нового чата
    /// </summary>
    public void Visit(TgNewChatEvent chatPositionEvent) => NeedSend = false;

    /// <summary>
    /// Метод для посещения события загрузки файла
    /// </summary>
    public void Visit(TgUpdateFileEvent updateFileEvent) => NeedSend = setting.Files;

    /// <summary>
    /// Метод для посещения события обновления папок чатов
    /// </summary>
    public void Visit(TgUpdateChatFoldersEvent chatOnlineMemberCountEvent) => NeedSend = false;

    /// <summary>
    /// Метод для посещения события изменения количества непрочитанных чатов
    /// </summary>
    public void Visit(TgUpdateUnreadChatCount chatOnlineMemberCountEvent) => NeedSend = false;

    /// <summary>
    /// Метод для посещения события изменения количества непрочитанных сообщений
    /// </summary>
    public void Visit(TgUpdateUnreadMessageCount chatOnlineMemberCountEvent) => NeedSend = false;
    
    /// <summary>
    /// Метод для посещения события обновления информации о базовом групповом чате
    /// </summary>
    public void Visit(TgUpdateBasicGroupFullInfo updateBasicGroupFullInfo) => NeedSend = setting.Chats;
    
    /// <summary>
    /// Метод для посещения события обновления информации о настройках уведомлений чате
    /// </summary>
    public void Visit(TgUpdateChatNotificationSettings updateChatNotificationSettings) => NeedSend = setting.Chats;
    
    /// <summary>
    /// Метод для посещения события обновления информации об изменении акцентного цвета в чате
    /// </summary>
    public void Visit(TgUpdateChatAccentColors updateChatAccentColors) => NeedSend = setting.Chats;
    
    /// <summary>
    /// Метод для посещения события обновления информации об автоудаении сообщений в чате
    /// </summary>
    public void Visit(TgUpdateChatMessageAutoDeleteTime updateChatMessageAutoDeleteTime) => NeedSend = setting.Chats;
    
    /// <summary>
    /// Метод для посещения события обновления информации об изменении списка доступных в чате реакций
    /// </summary>
    public void Visit(TgUpdateChatAvailableReactions updateChatAvailableReactions) => NeedSend = setting.Chats;
    
    /// <summary>
    /// Метод для посещения события обновления отправителя сообщений в групповом чате
    /// </summary>
    public void Visit(TgUpdateChatMessageSender updateChatMessageSender) => NeedSend = setting.Chats;
}