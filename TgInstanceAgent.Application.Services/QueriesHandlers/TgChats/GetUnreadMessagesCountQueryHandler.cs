using MediatR;
using TgInstanceAgent.Application.Abstractions.InstancesService;
using TgInstanceAgent.Application.Abstractions.Queries.TgChats;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Objects.Chats;

namespace TgInstanceAgent.Application.Services.QueriesHandlers.TgChats;

/// <summary>
/// Обработчик запроса на получение количества непрочитанных сообщений в списках чатов.
/// </summary>
public class GetUnreadMessagesCountQueryHandler(IInstancesService instancesService)
    : IRequestHandler<GetUnreadMessagesCountQuery, IReadOnlyCollection<TgUnreadMessagesCount>>
{
    /// <summary>
    /// Обрабатывает запрос на получение количества непрочитанных сообщений в списках чатов асинхронно.
    /// </summary>
    /// <param name="request">Запрос на получение количества непрочитанных сообщений в списках чатов.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Коллекция объектов количества непрочитанных сообщений в списках чатов.</returns>
    public async Task<IReadOnlyCollection<TgUnreadMessagesCount>> Handle(GetUnreadMessagesCountQuery request,
        CancellationToken cancellationToken)
    {
        // Запуск телеграм клиента инстанса
        var telegramClient = await instancesService.StartClientAsync(request.InstanceId, cancellationToken);

        // Получение папок чатов
        return telegramClient.GetUnreadMessagesCount();
    }
}