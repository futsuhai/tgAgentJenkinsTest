using MediatR;
using TgInstanceAgent.Application.Abstractions.InstancesService;
using TgInstanceAgent.Application.Abstractions.Queries.TgMessages;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Functions.Messages;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Objects.Messages;
using TgInstanceAgent.Application.Services.Extensions;

namespace TgInstanceAgent.Application.Services.QueriesHandlers.TgMessages;

/// <summary>
/// Обработчик запроса на получение данных сообщения.
/// </summary>
public class GetMessageQueryHandler(IInstancesService instancesService) : IRequestHandler<GetMessageQuery, TgMessage>
{
    /// <summary>
    /// Обрабатывает запрос на получение данных сообщения асинхронно.
    /// </summary>
    /// <param name="request">Запрос на получение данных сообщения.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Коллекцию контактов.</returns>
    public async Task<TgMessage> Handle(GetMessageQuery request, CancellationToken cancellationToken)
    {
        // Запуск телеграм клиента инстанса.
        var telegramClient = await instancesService.StartClientAsync(request.InstanceId, cancellationToken);

        // Получение истории чата через сервис инстансов.
        return await telegramClient.GetMessageAsync(new TgGetMessageRequest
        {
            // Получаем чат
            Chat = request.GetChat(),

            // Id сообщения
            MessageId = request.MessageId
        }, cancellationToken);
    }
}