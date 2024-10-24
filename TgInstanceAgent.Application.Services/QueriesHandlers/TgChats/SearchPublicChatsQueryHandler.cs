using MediatR;
using TgInstanceAgent.Application.Abstractions.InstancesService;
using TgInstanceAgent.Application.Abstractions.Queries.TgChats;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Objects.Chats;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Objects.Components;

namespace TgInstanceAgent.Application.Services.QueriesHandlers.TgChats;

/// <summary>
/// Обработчик запроса на поиск публичных чатов.
/// </summary>
public class SearchPublicChatsQueryHandler(IInstancesService instancesService)
    : IRequestHandler<SearchPublicChatsQuery, TgCountResult<TgChat>>
{
    /// <summary>
    /// Обрабатывает запрос на поиск публичных чатов асинхронно.
    /// </summary>
    /// <param name="request">Запрос на поиск публичных чатов.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Коллекцию публичных чатов.</returns>
    public async Task<TgCountResult<TgChat>> Handle(SearchPublicChatsQuery request, CancellationToken cancellationToken)
    {
        // Запуск телеграм клиента инстанса.
        var telegramClient = await instancesService.StartClientAsync(request.InstanceId, cancellationToken);

        // Поиск контактов через сервис инстансов.
        return await telegramClient.SearchPublicChatsAsync(request.Query, cancellationToken);
    }
}