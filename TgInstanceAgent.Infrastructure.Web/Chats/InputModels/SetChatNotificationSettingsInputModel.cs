using TgInstanceAgent.Infrastructure.Web.Components.Interfaces;

namespace TgInstanceAgent.Infrastructure.Web.Chats.InputModels;

/// <summary>
/// Модель входных данных для смены настройки уведомлений чата
/// </summary>
public class SetChatNotificationSettingsInputModel : IWithInputChat
{
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
    /// Если true, используется значение для соответствующего типа чата или форумного чата
    /// </summary>
    public bool? UseDefaultMuteFor { get; init; }

    /// <summary>
    /// Время, оставшееся до снятия отключения уведомлений, в секундах
    /// </summary>
    public int? MuteFor { get; init; }

    /// <summary>
    /// Если true, используется значение для соответствующего типа чата или форумного чата
    /// </summary>
    public bool? UseDefaultSound { get; init; }

    /// <summary>
    /// Идентификатор звукового уведомления, который будет воспроизводиться для сообщений; 0, если звук отключен
    /// </summary>
    public long? SoundId { get; init; }

    /// <summary>
    /// Если true, используется значение для соответствующего типа чата
    /// </summary>
    public bool? UseDefaultShowPreview { get; init; }

    /// <summary>
    /// Истинно, если содержимое сообщения должно отображаться в уведомлениях
    /// </summary>
    public bool? ShowPreview { get; init; }

    /// <summary>
    /// Если true, используется значение для соответствующего типа чата
    /// </summary>
    public bool? UseDefaultMuteStories { get; init; }

    /// <summary>
    /// Истинно, если уведомления о сториз отключены для чата
    /// </summary>
    public bool? MuteStories { get; init; }

    /// <summary>
    /// Если true, используется значение для соответствующего типа чата
    /// </summary>
    public bool? UseDefaultStorySound { get; init; }

    /// <summary>
    /// Идентификатор звукового уведомления, который будет воспроизводиться для сториз; 0, если звук отключен
    /// </summary>
    public long? StorySoundId { get; init; }

    /// <summary>
    /// Если true, используется значение для соответствующего типа чата
    /// </summary>
    public bool? UseDefaultShowStorySender { get; init; }

    /// <summary>
    /// Истинно, если отправитель сториз должен отображаться в уведомлениях
    /// </summary>
    public bool? ShowStorySender { get; init; }

    /// <summary>
    /// Если true, используется значение для соответствующего типа чата или форумного чата
    /// </summary>
    public bool? UseDefaultDisablePinnedMessageNotifications { get; init; }

    /// <summary>
    /// Если true, уведомления для входящих закрепленных сообщений будут создаваться как для обычного непрочитанного сообщения
    /// </summary>
    public bool? DisablePinnedMessageNotifications { get; init; }

    /// <summary>
    /// Если true, используется значение для соответствующего типа чата или форумного чата
    /// </summary>
    public bool? UseDefaultDisableMentionNotifications { get; init; }

    /// <summary>
    /// Если true, уведомления для сообщений с упоминаниями будут создаваться как для обычного непрочитанного сообщения
    /// </summary>
    public bool? DisableMentionNotifications { get; init; }
}