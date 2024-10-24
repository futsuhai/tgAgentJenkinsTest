using MediatR;
using TgInstanceAgent.Application.Abstractions.Interfaces;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Objects.Stories;

namespace TgInstanceAgent.Application.Abstractions.Queries.TgStories;

/// <summary>
/// Запрос на получение истории по ее идентификатору
/// </summary>
public class GetStoryQuery : IWithChat, IWithInstanceId, IRequest<TgStory>
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
    /// Идентификатор истории
    /// </summary>
    public required int StoryId { get; init; }
}