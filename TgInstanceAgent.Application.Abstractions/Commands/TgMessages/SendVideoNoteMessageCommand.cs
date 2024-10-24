using MediatR;
using TgInstanceAgent.Application.Abstractions.Interfaces;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Objects.Messages;

namespace TgInstanceAgent.Application.Abstractions.Commands.TgMessages;

/// <summary>
/// Команда для отправки видеосообщения в формате заметки.
/// </summary>
public class SendVideoNoteMessageCommand : IWithChat, IWithSendOptions, IWithFile, IWithInstanceId, IRequest<TgMessage>
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
    /// Файл.
    /// </summary>
    public FileData? File { get; init; }

    /// <summary>
    /// Локальный идентификатор файла
    /// </summary>
    public int? LocalId { get; init; }

    /// <summary>
    /// Удаленный идентификатор файла
    /// </summary>
    public string? RemoteId { get; init; }

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