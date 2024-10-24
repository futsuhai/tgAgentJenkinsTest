using MediatR;
using TgInstanceAgent.Application.Abstractions.Interfaces;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Objects.Components;

namespace TgInstanceAgent.Application.Abstractions.Queries.TgUsers;

/// <summary>
/// Запрос на получение фотографий пользователя.
/// Результат - список фотографий.
/// </summary>
public class GetUserProfilePhotosQuery : IWithUser, IWithInstanceId, IRequest<TgCountResult<TgChatPhoto>>
{
    /// <summary>
    /// Уникальный идентификатор инстанса.
    /// </summary>
    public required Guid InstanceId { get; init; }
    
    /// <summary>
    /// Идентификатор пользователя.
    /// При отправке запроса по идентификатору, необходимо быть уверенным, что приложению известен данный пользователь.
    /// </summary>
    public long? UserId { get; init; }

    /// <summary>
    /// Имя пользователя, фотографии которого необходимо получить.
    /// </summary>
    public string? Username { get; init; }
    
    /// <summary>
    /// Номер телефона пользователя, фотографии которого необходимо получить. Если установлено, ChatId и Username могут быть не заданы.
    /// </summary>
    public string? PhoneNumber { get; init; }
    
    /// <summary>
    /// Смещение (Пример - У пользователя 3 фотки, смещение 2 -> получаем 1 фотку).
    /// </summary>
    public int Offset { get; init; }
    
    /// <summary>
    /// Число необходимых фотографий.
    /// </summary>
    public int Limit { get; init; }
}