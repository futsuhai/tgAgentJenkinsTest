using MediatR;
using TgInstanceAgent.Application.Abstractions.InstancesService;
using TgInstanceAgent.Application.Abstractions.Queries.TgUsers;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Functions.Users;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Objects.Components;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Objects.Users;

namespace TgInstanceAgent.Application.Services.QueriesHandlers.TgUsers;

/// <summary>
/// Обработчик запроса на поиск контактов.
/// </summary>
public class SearchContactsQueryHandler(IInstancesService instancesService)
    : IRequestHandler<SearchContactsQuery, TgCountResult<TgUser>>
{
    /// <summary>
    /// Обрабатывает запрос на поиск контактов асинхронно.
    /// </summary>
    /// <param name="request">Запрос на поиск контактов.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Коллекцию контактов.</returns>
    public async Task<TgCountResult<TgUser>> Handle(SearchContactsQuery request, CancellationToken cancellationToken)
    {
        // Запуск телеграм клиента инстанса.
        var telegramClient = await instancesService.StartClientAsync(request.InstanceId, cancellationToken);

        // Поиск контактов через сервис инстансов.
        return await telegramClient.SearchContactsAsync(new TgSearchContactsRequest
        {
            // Число необходимых контактов.
            Limit = request.Limit,
            
            // Строка с ключевыми словами для поиска.
            Query = request.Query
        }, cancellationToken);
    }
}