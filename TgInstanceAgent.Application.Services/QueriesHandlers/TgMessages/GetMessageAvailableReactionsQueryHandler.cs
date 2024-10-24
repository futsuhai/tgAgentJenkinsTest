using MediatR;
using TgInstanceAgent.Application.Abstractions.InstancesService;
using TgInstanceAgent.Application.Abstractions.Queries.TgMessages;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Functions.Messages;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Objects.Messages;
using TgInstanceAgent.Application.Services.Extensions;

namespace TgInstanceAgent.Application.Services.QueriesHandlers.TgMessages;

/// <summary>
/// Обработчик запроса получения списка доступных реакций на сообщение.
/// </summary>
public class GetMessageAvailableReactionsQueryHandler(IInstancesService instancesService)
    : IRequestHandler<GetMessageAvailableReactionsQuery, TgMessageAvailableReactions>
{
    /// <summary>
    /// Обрабатывает запрос на получение списка доступных реакций на сообщение.
    /// </summary>
    /// <param name="request">Запрос на получение списка доступных реакций на сообщение.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns></returns>
    public async Task<TgMessageAvailableReactions> Handle(GetMessageAvailableReactionsQuery request,
        CancellationToken cancellationToken)
    {
        // Запуск телеграм клиента инстанса.
        var instanceService = await instancesService.StartClientAsync(request.InstanceId, cancellationToken);

        // Получание списка доступных реакций на сообщение через сервис инстансов.
        return await instanceService.GetMessageAvailableReactionsAsync(new TgGetMessageReactionsRequest
        {
            // Получаем чат
            Chat = request.GetChat(),

            // Id сообщения
            MessageId = request.MessageId
        }, cancellationToken);
    }
}