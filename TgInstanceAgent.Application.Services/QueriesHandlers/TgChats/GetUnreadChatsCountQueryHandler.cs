using MediatR;
using TgInstanceAgent.Application.Abstractions.InstancesService;
using TgInstanceAgent.Application.Abstractions.Queries.TgChats;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Objects.Chats;

namespace TgInstanceAgent.Application.Services.QueriesHandlers.TgChats;

/// <summary>
/// Обработчик запроса на получение количества непрочитанных чатов в списках чатов.
/// </summary>
public class GetUnreadChatsCountQueryHandler(IInstancesService instancesService) : IRequestHandler<GetUnreadChatsCountQuery, IReadOnlyCollection<TgUnreadChatsCount>>
{
    /// <summary>
    /// Обрабатывает запрос на получение количества непрочитанных чатов в списках чатов асинхронно.
    /// </summary>
    /// <param name="request">Запрос на получение количества непрочитанных чатов в списках чатов.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Коллекция объектов количества непрочитанных чатов в списках чатов.</returns>
    public async Task<IReadOnlyCollection<TgUnreadChatsCount>> Handle(GetUnreadChatsCountQuery request,
        CancellationToken cancellationToken)
    {
        // Запуск телеграм клиента инстанса
        var telegramClient = await instancesService.StartClientAsync(request.InstanceId, cancellationToken);

        // Получение папок чатов
        return telegramClient.GetUnreadChatsCount();
    }
}