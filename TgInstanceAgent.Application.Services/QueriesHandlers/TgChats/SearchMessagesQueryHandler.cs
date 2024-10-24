using MediatR;
using TgInstanceAgent.Application.Abstractions.InstancesService;
using TgInstanceAgent.Application.Abstractions.Queries.TgChats;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Functions.Chats;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Objects.Components;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Objects.Messages;

namespace TgInstanceAgent.Application.Services.QueriesHandlers.TgChats;

/// <summary>
/// Обработчик запроса на поиск сообщений во всех чатах.
/// </summary>
public class SearchMessagesQueryHandler(IInstancesService instancesService)
    : IRequestHandler<SearchMessagesQuery, TgFoundMessages>
{
    /// <summary>
    /// Обрабатывает запрос на поиск сообщений во всех чатах.
    /// </summary>
    /// <param name="request">Запрос на на поиск сообщений во всех чатах.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Коллекцию сообщений.</returns>
    public async Task<TgFoundMessages> Handle(SearchMessagesQuery request, CancellationToken cancellationToken)
    {
        // Запуск телеграм клиента инстанса.
        var telegramClient = await instancesService.StartClientAsync(request.InstanceId, cancellationToken);

        // Получение истории чата через сервис инстансов.
        return await telegramClient.SearchMessagesAsync(new TgSearchMessagesRequest
        {
            // Строка с ключевыми словами для поиска.
            Query = request.Query,

            // Тип списка
            List = request.List,

            // Лимит сообщений.
            Limit = request.Limit,

            // Смещение.
            Offset = request.Offset,

            // Только в каналах
            OnlyInChannels = request.OnlyInChannels,

            // Тип фильтра.
            Filter = request.Filter,
            
            // Минимальная дата
            MinDate = request.MinDate,
            
            // Максимальная дата
            MaxDate = request.MaxDate
            
        }, cancellationToken);
    }
}