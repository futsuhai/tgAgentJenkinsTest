using MediatR;
using TgInstanceAgent.Application.Abstractions.InstancesService;
using TgInstanceAgent.Application.Abstractions.Queries.TgUsers;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Functions.Users;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Objects.Components;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Objects.Users;

namespace TgInstanceAgent.Application.Services.QueriesHandlers.TgUsers;

/// <summary>
/// Обработчик запроса на получение контактов.
/// </summary>
public class GetContactsQueryHandler(IInstancesService instancesService)
    : IRequestHandler<GetContactsQuery, TgCountResult<TgUser>>
{
    /// <summary>
    /// Обрабатывает запрос на получение контактов асинхронно.
    /// </summary>
    /// <param name="request">Запрос на получение контактов.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Коллекцию контактов.</returns>
    public async Task<TgCountResult<TgUser>> Handle(GetContactsQuery request, CancellationToken cancellationToken)
    {
        // Запуск телеграм клиента инстанса.
        var telegramClient = await instancesService.StartClientAsync(request.InstanceId, cancellationToken);

        // Получение контактов через сервис инстансов.
        return await telegramClient.GetContactsAsync(new TgGetContactsRequest
        {
            // Устанавливаем начальный ID контакта
            FromContactId = request.FromContactId,
            
            // Устанавливаем смещение.
            Offset = request.Offset,

            // Устанавливаем ограничение количества фотографий.
            Limit = request.Limit
        });
    }
}