using MediatR;
using TgInstanceAgent.Application.Abstractions.InstancesService;
using TgInstanceAgent.Application.Abstractions.Queries.TgChats;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Objects.Chats;
using TgInstanceAgent.Application.Services.Extensions;

namespace TgInstanceAgent.Application.Services.QueriesHandlers.TgChats;

/// <summary>
/// Обработчик запроса на получение чата.
/// </summary>
public class GetChatQueryHandler(IInstancesService instancesService) : IRequestHandler<GetChatQuery, TgChat>
{
    /// <summary>
    /// Обрабатывает запрос на получение чата асинхронно.
    /// </summary>
    /// <param name="request">Запрос на получение чата.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Чат.</returns>
    public async Task<TgChat> Handle(GetChatQuery request,
        CancellationToken cancellationToken)
    {
        // Запуск телеграм клиента инстанса.
        var telegramClient = await instancesService.StartClientAsync(request.InstanceId, cancellationToken);

        // Получение истории чата через сервис инстансов.
        return await telegramClient.GetChatAsync(request.GetChat(), cancellationToken);
    }
}