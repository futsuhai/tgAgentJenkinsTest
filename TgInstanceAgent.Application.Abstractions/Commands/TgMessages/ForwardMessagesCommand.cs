using MediatR;
using TgInstanceAgent.Application.Abstractions.Interfaces;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Objects.Messages;

namespace TgInstanceAgent.Application.Abstractions.Commands.TgMessages;

/// <summary>
/// Команда для пересылки сообщений
/// Возвращает список пересланных сообщений
/// </summary>
public class ForwardMessagesCommand : IWithChat, IWithSendOptions, IWithInstanceId, IRequest<IReadOnlyCollection<TgMessage>>
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
    /// Имя пользователя чата. Если установлено, Chat может быть не задан.
    /// </summary>
    public string? Username { get; init; }

    /// <summary>
    /// Номер телефона пользователя. Если установлено, Chat и Username могут быть не заданы.
    /// </summary>
    public string? PhoneNumber { get; init; }
    
    /// <summary>
    /// Идентификатор чата, с которого производится пересылка сообщений
    /// </summary>
    public long FromChatId { get; init; }
    
    /// <summary>
    /// Массив идентификаторо сообщений для пересылки
    /// </summary>
    public long[]? MessageIds { get; init; }
    
    /// <summary>
    /// Истинно, если требуется убрать подпись в медиа файлах
    /// </summary>
    public bool RemoveCaption { get; init; }

    /// <summary>
    /// Истинно, если требуется переслать сообщения без ссылок на пользователей этих сообщений
    /// </summary>
    public bool SendCopy { get; init; }

    /// <summary>
    /// Флаг - выключить оповещение получателя
    /// </summary>
    public bool DisableNotification { get; init; }
    
    /// <summary>
    /// Флаг - можно ли пересылать, сохранять отправленное сообщение
    /// </summary>
    public bool ProtectContent { get; init; }

    /// <summary>
    /// Отправить, когда будет в сети
    /// </summary>
    public bool SendOnOnline { get; init; }

    /// <summary>
    /// Отправить по времени
    /// </summary>
    public int? SendOnTime { get; init; }
}