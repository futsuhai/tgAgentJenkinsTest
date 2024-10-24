using MediatR;
using TgInstanceAgent.Application.Abstractions.Interfaces;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Objects.Users;

namespace TgInstanceAgent.Application.Abstractions.Queries.TgUsers;

/// <summary>
/// Запрос для получения информации о моем аккаунте.
/// Результат - данные авторизованного аккаунта.
/// </summary>
public class GetMeQuery : IWithInstanceId, IRequest<TgUser>
{
    /// <summary>
    /// Уникальный идентификатор инстанса.
    /// </summary>
    public required Guid InstanceId { get; init; }
}