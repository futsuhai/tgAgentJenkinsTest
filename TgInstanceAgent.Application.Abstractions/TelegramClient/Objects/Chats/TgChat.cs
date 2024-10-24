using TgInstanceAgent.Application.Abstractions.TelegramClient.Objects.Components;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Objects.Messages;

namespace TgInstanceAgent.Application.Abstractions.TelegramClient.Objects.Chats;

/// <summary>
/// Чат. (Это может быть приватный чат, обычная группа, супергруппа или секретный чат)
/// </summary>
public class TgChat
{
    /// <summary>
    /// Уникальный идентификатор чата
    /// </summary>
    public required long Id { get; init; }

    /// <summary>
    /// Тип чата
    /// </summary>
    public required TgChatType Type { get; init; }

    /// <summary>
    /// Заголовок чата
    /// </summary>
    public string? Title { get; init; }

    /// <summary>
    /// Фото чата.
    /// </summary>
    public TgChatPhotoInfo? Photo { get; init; }

    /// <summary>
    /// Действия, которые неадминистраторы чата могут выполнять в чате
    /// </summary>
    public required TgChatPermissions Permissions { get; init; }

    /// <summary>
    /// Последнее сообщение в чате; может быть null, если нет или неизвестно
    /// </summary>
    public TgMessage? LastMessage { get; init; }

    /// <summary>
    /// Позиции чата в списках чатов
    /// </summary>
    public required TgChatPosition[] Positions { get; init; }

    /// <summary>
    /// Идентификатор пользователя или чата, выбранного для отправки сообщений в чате; может быть null, если пользователь не может менять отправителя сообщения
    /// </summary>
    public TgMessageSender? MessageSenderId { get; init; }

    /// <summary>
    /// Флаг, заблокирован ли чат.
    /// </summary>
    public required bool IsBlocked { get; init; }
    
    /// <summary>
    /// Флаг, заблокированы ли истории чата.
    /// </summary>
    public required bool IsStoriesBlocked { get; init; }

    /// <summary>
    /// True, если содержимое чата нельзя сохранять локально, пересылать или копировать
    /// </summary>
    public required bool HasProtectedContent { get; init; }

    /// <summary>
    /// True, если следует предлагать пользователю перевод всех сообщений в чате
    /// </summary>
    public required bool IsTranslatable { get; init; }

    /// <summary>
    /// True, если чат помечен как непрочитанный
    /// </summary>
    public required bool IsMarkedAsUnread { get; init; }

    /// <summary>
    /// True, если чат является супергруппой форума, которую следует отображать в режиме "Просмотр как темы", или чатом "Сохраненные сообщения", который следует отображать в режиме "Просмотр как чаты"
    /// </summary>
    public required bool ViewAsTopics { get; init; }

    /// <summary>
    /// True, если в чате есть запланированные сообщения
    /// </summary>
    public required bool HasScheduledMessages { get; init; }

    /// <summary>
    /// True, если сообщения чата можно удалять только для текущего пользователя, в то время как другие пользователи продолжат видеть сообщения
    /// </summary>
    public required bool CanBeDeletedOnlyForSelf { get; init; }

    /// <summary>
    /// True, если сообщения чата можно удалять для всех пользователей
    /// </summary>
    public required bool CanBeDeletedForAllUsers { get; init; }

    /// <summary>
    /// Значение по умолчанию параметра disable_notification, используемого при отправке сообщения в чат
    /// </summary>
    public required bool DefaultDisableNotification { get; init; }

    /// <summary>
    /// Количество непрочитанных сообщений в чате
    /// </summary>
    public required int UnreadCount { get; init; }

    /// <summary>
    /// Количество непрочитанных сообщений с упоминанием/ответом в чате
    /// </summary>
    public required int UnreadMentionCount { get; init; }

    /// <summary>
    /// Количество сообщений с непрочитанными реакциями в чате
    /// </summary>
    public required int UnreadReactionCount { get; init; }

    /// <summary>
    /// Доступные типы реакций в чате
    /// </summary>
    public required TgChatAvailableReactions AvailableReactions { get; init; }

    /// <summary>
    /// Текущие настройки таймера автоудаления или самоуничтожения сообщений для чата, в секундах. Таймер самоуничтожения в секретных чатах начинает работать после просмотра сообщения или его содержимого. Таймер автоудаления в других чатах начинает работать с даты отправки
    /// </summary>
    public int? MessageAutoDeleteTime { get; init; }

    /// <summary>
    /// Статус эмодзи, который будет отображаться вместе с заголовком чата
    /// </summary>
    public TgEmojiStatus? EmojiStatus { get; init; }

    /// <summary>
    /// Информация о запросах на вступление в ожидании
    /// </summary>
    public TgChatJoinRequestsInfo? PendingJoinRequests { get; init; }
    
    /// <summary>
    /// Фон конкретного чата
    /// </summary>
    public TgChatBackground? Background { get; init; }
}