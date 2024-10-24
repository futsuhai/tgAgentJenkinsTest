using MediatR;
using TgInstanceAgent.Application.Abstractions.InstancesService;
using TgInstanceAgent.Application.Abstractions.Queries.TgChats;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Functions.Chats;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Objects.Chats;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Objects.Components;

namespace TgInstanceAgent.Application.Services.QueriesHandlers.TgChats;

/// <summary>
/// Обработчик запроса на поиск чатов.
/// </summary>
public class SearchChatsQueryHandler(IInstancesService instancesService)
    : IRequestHandler<SearchChatsQuery, TgCountResult<TgChat>>
{
    /// <summary>
    /// Обрабатывает запрос на поиск чатов асинхронно.
    /// </summary>
    /// <param name="request">Запрос на поиск чатов.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Коллекция чатов.</returns>
    public async Task<TgCountResult<TgChat>> Handle(SearchChatsQuery request, CancellationToken cancellationToken)
    {
        // Запуск телеграм клиента инстанса.
        var telegramClient = await instancesService.StartClientAsync(request.InstanceId, cancellationToken);

        // Поиск контактов через сервис инстансов.
        return await telegramClient.SearchChatsAsync(new TgSearchChatsRequest
        {
            // Число необходимых чатов.
            Limit = request.Limit,
            
            // Строка с ключевыми словами для поиска.
            Query = request.Query
        }, cancellationToken);
    }
}