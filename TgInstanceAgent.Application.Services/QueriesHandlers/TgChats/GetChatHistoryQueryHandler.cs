using MediatR;
using TgInstanceAgent.Application.Abstractions.InstancesService;
using TgInstanceAgent.Application.Abstractions.Queries.TgChats;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Functions.Chats;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Objects.Components;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Objects.Messages;
using TgInstanceAgent.Application.Services.Extensions;

namespace TgInstanceAgent.Application.Services.QueriesHandlers.TgChats;

/// <summary>
/// Обработчик запроса на получение истории чата.
/// </summary>
public class GetChatHistoryQueryHandler(IInstancesService instancesService)
    : IRequestHandler<GetChatHistoryQuery, Abstractions.TelegramClient.Objects.Messages.TgMessages>
{
    /// <summary>
    /// Обрабатывает запрос на получение истории чата асинхронно.
    /// </summary>
    /// <param name="request">Запрос на получение истории чата.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Коллекцию контактов.</returns>
    public async Task<Abstractions.TelegramClient.Objects.Messages.TgMessages> Handle(GetChatHistoryQuery request,
        CancellationToken cancellationToken)
    {
        // Запуск телеграм клиента инстанса.
        var telegramClient = await instancesService.StartClientAsync(request.InstanceId, cancellationToken);

        // Получение истории чата через сервис инстансов.
        return await telegramClient.GetChatHistoryAsync(new TgGetChatHistoryRequest
        {
            FromMessageId = request.FromMessageId,
            Limit = request.Limit,
            Offset = request.Offset,
            Chat = request.GetChat(),
            IncludeUsers = request.IncludeUsers
        }, cancellationToken);
    }
}