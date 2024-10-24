using MediatR;
using TgInstanceAgent.Application.Abstractions.InstancesService;
using TgInstanceAgent.Application.Abstractions.Queries.TgUsers;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Objects.Users;

namespace TgInstanceAgent.Application.Services.QueriesHandlers.TgUsers;

/// <summary>
/// Обработчик команды, который получает информацию о контакте для заданного идентификатора пользователя.
/// </summary>
/// <param name="instancesService">Сервис, используемый для запуска клиентских инстансов.</param>
public class GetMeQueryHandler(IInstancesService instancesService) : IRequestHandler<GetMeQuery, TgUser>
{
    /// <summary>
    /// Обрабатывает входящий запрос на получение информации о контакте.
    /// </summary>
    /// <param name="request">Запрос, содержащий идентификатор пользователя, чью информацию о контакте нужно получить.</param>
    /// <param name="cancellationToken">Токен, который можно использовать для сигнализации о отмене запроса.</param>
    /// <returns>Задача, представляющая асинхронную операцию, которая возвращает информацию о контакте.</returns>
    public async Task<TgUser> Handle(GetMeQuery request, CancellationToken cancellationToken)
    {
        // Асинхронно запускаем клиентский сервис с использованием предоставленного из запроса идентификатора пользователя.
        var telegramClient = await instancesService.StartClientAsync(request.InstanceId, cancellationToken);
        
        // Получаем и возвращаем асинхронно информацию о контакте пользователя.
        return await telegramClient.GetMeAsync(cancellationToken);
    }
}