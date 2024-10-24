using MediatR;
using TgInstanceAgent.Application.Abstractions.Interfaces;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Objects.Messages;

namespace TgInstanceAgent.Application.Abstractions.Commands.TgMessages;

/// <summary>
/// Команда для отправки текстового сообщения.
/// </summary>
public class SendTextMessageCommand : IWithChat, IWithSendOptions, IWithInstanceId, IRequest<TgMessage>
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
    /// Текст сообщения.
    /// </summary>
    public required string Message { get; init; }

    /// <summary>
    /// True, если предварительный просмотр ссылок должен быть отключен.
    /// </summary>
    public bool DisableUrlPreview { get; init; }

    /// <summary>
    /// URL для использования в предварительном просмотре ссылки. Если пусто, будет использован первый URL, найденный в тексте сообщения.
    /// </summary>
    public string? UrlPreview { get; init; }
    
    /// <summary>
    /// Флаг, указывающий, нужно ли отправлять активность перед сообщением
    /// </summary>
    public bool NeedShowActivity { get; init; }
    
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