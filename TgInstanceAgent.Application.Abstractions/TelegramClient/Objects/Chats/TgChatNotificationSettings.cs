namespace TgInstanceAgent.Application.Abstractions.TelegramClient.Objects.Chats;

/// <summary>
/// Настройки уведомлений чата
/// </summary>
public class TgChatNotificationSettings
{
    /// <summary>
    /// Если true, используется значение для соответствующего типа чата или форумного чата
    /// </summary>
    public required bool UseDefaultMuteFor { get; init; }

    /// <summary>
    /// Время, оставшееся до снятия отключения уведомлений, в секундах
    /// </summary>
    public required int MuteFor { get; init; }

    /// <summary>
    /// Если true, используется значение для соответствующего типа чата или форумного чата
    /// </summary>
    public required bool UseDefaultSound { get; init; }

    /// <summary>
    /// Идентификатор звукового уведомления, который будет воспроизводиться для сообщений; 0, если звук отключен
    /// </summary>
    public required long SoundId { get; init; }

    /// <summary>
    /// Если true, используется значение для соответствующего типа чата
    /// </summary>
    public required bool UseDefaultShowPreview { get; init; }

    /// <summary>
    /// Истинно, если содержимое сообщения должно отображаться в уведомлениях
    /// </summary>
    public required bool ShowPreview { get; init; }

    /// <summary>
    /// Если true, используется значение для соответствующего типа чата
    /// </summary>
    public required bool UseDefaultMuteStories { get; init; }

    /// <summary>
    /// Истинно, если уведомления о сториз отключены для чата
    /// </summary>
    public required bool MuteStories { get; init; }

    /// <summary>
    /// Если true, используется значение для соответствующего типа чата
    /// </summary>
    public required bool UseDefaultStorySound { get; init; }

    /// <summary>
    /// Идентификатор звукового уведомления, который будет воспроизводиться для сториз; 0, если звук отключен
    /// </summary>
    public required long StorySoundId { get; init; }

    /// <summary>
    /// Если true, используется значение для соответствующего типа чата
    /// </summary>
    public required bool UseDefaultShowStorySender { get; init; }

    /// <summary>
    /// Истинно, если отправитель сториз должен отображаться в уведомлениях
    /// </summary>
    public required bool ShowStorySender { get; init; }

    /// <summary>
    /// Если true, используется значение для соответствующего типа чата или форумного чата
    /// </summary>
    public required bool UseDefaultDisablePinnedMessageNotifications { get; init; }

    /// <summary>
    /// Если true, уведомления для входящих закрепленных сообщений будут создаваться как для обычного непрочитанного сообщения
    /// </summary>
    public required bool DisablePinnedMessageNotifications { get; init; }

    /// <summary>
    /// Если true, используется значение для соответствующего типа чата или форумного чата
    /// </summary>
    public required bool UseDefaultDisableMentionNotifications { get; init; }

    /// <summary>
    /// Если true, уведомления для сообщений с упоминаниями будут создаваться как для обычного непрочитанного сообщения
    /// </summary>
    public required bool DisableMentionNotifications { get; init; }
}