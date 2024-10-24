namespace TgInstanceAgent.Application.Abstractions.Interfaces;

/// <summary>
/// Интерфейс, определяющий наличие информации для запроса файла.
/// </summary>
public interface IWithFileRequest
{
    /// <summary>
    /// Локальный идентификатор файла
    /// </summary>
    public int? LocalId { get; init; }

    /// <summary>
    /// Удаленный идентификатор файла
    /// </summary>
    public string? RemoteId { get; init; }
    
    /// <summary>
    /// Данные сообщения для получения файла
    /// </summary>
    public FileFromMessageData? FileFromMessage { get; init; }
}

/// <summary>
/// Класс, описывающий данные для получения файла из сообщения
/// </summary>
public class FileFromMessageData : IWithChat
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
    /// Идентификатор сообщения.
    /// </summary>
    public required long MessageId { get; init; }
}