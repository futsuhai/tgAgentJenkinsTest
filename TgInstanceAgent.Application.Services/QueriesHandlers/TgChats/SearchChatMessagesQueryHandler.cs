using MediatR;
using TgInstanceAgent.Application.Abstractions.InstancesService;
using TgInstanceAgent.Application.Abstractions.Queries.TgChats;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Functions.Chats;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Objects.Components;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Objects.Messages;
using TgInstanceAgent.Application.Services.Extensions;

namespace TgInstanceAgent.Application.Services.QueriesHandlers.TgChats;

/// <summary>
/// Обработчик запроса на поиск сообщений в чате.
/// </summary>
public class SearchChatMessagesQueryHandler(IInstancesService instancesService)
    : IRequestHandler<SearchChatMessagesQuery, TgCountResult<TgMessage>>
{
    /// <summary>
    /// Обрабатывает запрос на поиск сообщений в чате асинхронно.
    /// </summary>
    /// <param name="request">Запрос на на поиск сообщений в чате.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Коллекцию сообщений.</returns>
    public async Task<TgCountResult<TgMessage>> Handle(SearchChatMessagesQuery request, CancellationToken cancellationToken)
    {
        // Запуск телеграм клиента инстанса.
        var telegramClient = await instancesService.StartClientAsync(request.InstanceId, cancellationToken);

        // Получение истории чата через сервис инстансов.
        return await telegramClient.SearchChatMessagesAsync(new TgSearchChatMessagesRequest
        {
            // Получаем чат.
            Chat = request.GetChat(),
            
            // Отправитель.
            MessageSender = request.GetMessageSender(),
            
            // Строка с ключевыми словами для поиска.
            Query = request.Query,
            
            // Лимит сообщений.
            Limit = request.Limit,
            
            // Смещение.
            Offset = request.Offset,
            
            // Начиная с идентификатора сообщения.
            FromMessageId = request.FromMessageId,
            
            // Идентификатор потока сообщений (thread) в чате.
            MessageThreadId = request.MessageThreadId,
            
            // Идентификатор темы сохраненных сообщений.
            SavedMessagesTopicId = request.SavedMessagesTopicId,
            
            // Тип фильтра.
            Filter = request.Filter
        }, cancellationToken);
    }
}