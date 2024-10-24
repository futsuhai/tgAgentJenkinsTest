using MediatR;
using TgInstanceAgent.Application.Abstractions.Interfaces;

namespace TgInstanceAgent.Application.Abstractions.Commands.TgChats;

/// <summary>
/// Команда изменения настроек уведомлений чата.
/// </summary>
public class SetChatNotificationSettingsCommand : IWithChat, IWithInstanceId, IRequest
{
    /// <summary>
    /// Уникальный идентификатор инстанса.
    /// </summary>
    public required Guid InstanceId { get; init; }
    
    /// <summary>
    /// Идентификатор чата.
    /// При отправке запроса по идентификатору, необходимо быть уверенным, что приложению известен данный чат.
    /// </summary>
    public long? ChatId { get; init; }

    /// <summary>
    /// Имя пользователя для отправки реакции. Если установлено, ChatId может быть не задан.
    /// </summary>
    public string? Username { get; init; }

    /// <summary>
    /// Номер телефона для отправки реакции. Если установлено, ChatId и Username могут быть не заданы.
    /// </summary>
    public string? PhoneNumber { get; init; }
    
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