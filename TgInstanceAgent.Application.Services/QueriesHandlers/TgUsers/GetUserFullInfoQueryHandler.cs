using MediatR;
using TgInstanceAgent.Application.Abstractions.InstancesService;
using TgInstanceAgent.Application.Abstractions.Queries.TgUsers;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Objects.Users;
using TgInstanceAgent.Application.Services.Extensions;

namespace TgInstanceAgent.Application.Services.QueriesHandlers.TgUsers;

/// <summary>
/// Обработчик запроса на получение био пользователя.
/// </summary>
public class GetUserFullInfoQueryHandler(IInstancesService instancesService)
    : IRequestHandler<GetUserFullInfoQuery, TgUserFullInfo>
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="request">Запрос на получение био пользователя.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Био пользователя.</returns>
    public async Task<TgUserFullInfo> Handle(GetUserFullInfoQuery request, CancellationToken cancellationToken)
    {
        // Запуск телеграм клиента инстанса.
        var service = await instancesService.StartClientAsync(request.InstanceId, cancellationToken);

        // Получение информации о пользователе
        return await service.GetUserFullInfoAsync(request.GetUser(), cancellationToken);
    }
}