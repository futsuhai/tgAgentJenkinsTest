using MediatR;
using TgInstanceAgent.Application.Abstractions.InstancesService;
using TgInstanceAgent.Application.Abstractions.Queries.TgUsers;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Objects.Users;
using TgInstanceAgent.Application.Services.Extensions;

namespace TgInstanceAgent.Application.Services.QueriesHandlers.TgUsers;

/// <summary>
/// Обработчик запроса на получение данных пользователя.
/// </summary>
public class GetUserQueryHandler(IInstancesService instancesService) : IRequestHandler<GetUserQuery, TgUser>
{
    /// <summary>
    /// Обрабатывает запрос на получение данных пользователя асинхронно.
    /// </summary>
    /// <param name="request">Запрос на получение данных пользователя.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Коллекцию контактов.</returns>
    public async Task<TgUser> Handle(GetUserQuery request, CancellationToken cancellationToken)
    {
        // Запуск телеграм клиента инстанса.
        var telegramClient = await instancesService.StartClientAsync(request.InstanceId, cancellationToken);

        // Получение данных пользователя через сервис инстансов.
        return await telegramClient.GetUserAsync(request.GetUser(), cancellationToken);
    }
}