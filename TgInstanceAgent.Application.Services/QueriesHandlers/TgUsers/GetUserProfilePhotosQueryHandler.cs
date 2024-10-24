using MediatR;
using TgInstanceAgent.Application.Abstractions.InstancesService;
using TgInstanceAgent.Application.Abstractions.Queries.TgUsers;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Functions.Users;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Objects.Components;
using TgInstanceAgent.Application.Services.Extensions;

namespace TgInstanceAgent.Application.Services.QueriesHandlers.TgUsers;

/// <summary>
/// Обработчик команды, который получает список фотографий пользователя.
/// </summary>
/// <param name="instancesService">Сервис, используемый для запуска клиентских инстансов.</param>
public class GetUserProfilePhotosQueryHandler(IInstancesService instancesService)
    : IRequestHandler<GetUserProfilePhotosQuery, TgCountResult<TgChatPhoto>>
{
    /// <summary>
    /// Обрабатывает запрос на получение контактов асинхронно.
    /// </summary>
    /// <param name="request">Запрос на получение контактов.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Список фотографий пользователя.</returns>
    public async Task<TgCountResult<TgChatPhoto>> Handle(GetUserProfilePhotosQuery request,
        CancellationToken cancellationToken)
    {
        // Запуск телеграм клиента инстанса.
        var telegramClient = await instancesService.StartClientAsync(request.InstanceId, cancellationToken);

        // Получение фотографий пользователя.
        return await telegramClient.GetProfilePhotosAsync(new TgGetUserProfilePhotosRequest
        {
            // Устанавливаем чат.
            User = request.GetUser(),

            // Устанавливаем смещение.
            Offset = request.Offset,

            // Устанавливаем ограничение количества фотографий.
            Limit = request.Limit
            
        }, cancellationToken);
    }
}