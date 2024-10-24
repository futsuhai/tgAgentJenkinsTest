using MediatR;
using TgInstanceAgent.Application.Abstractions.InstancesService;
using TgInstanceAgent.Application.Abstractions.Queries.TgStories;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Functions.Stories;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Objects.Stories;
using TgInstanceAgent.Application.Services.Extensions;

namespace TgInstanceAgent.Application.Services.QueriesHandlers.TgStories;

/// <summary>
/// Обработчик запроса на получение истории по идентификатору
/// </summary>
/// <param name="instancesService">Сервис для работы с инстансами.</param>
public class GetStoryQueryHandler(IInstancesService instancesService) : IRequestHandler<GetStoryQuery, TgStory>
{
    /// <summary>
    /// Обработка запроса на получение истории
    /// </summary>
    /// <param name="request">Запрос на получение истории</param>
    /// <param name="cancellationToken">Токен отмены операции</param>
    /// <returns>Модель, содержащую информацию об истории</returns>
    public async Task<TgStory> Handle(GetStoryQuery request, CancellationToken cancellationToken)
    {
        // Запуск телеграм клиента инстанса.
        var service = await instancesService.StartClientAsync(request.InstanceId, cancellationToken);

        // Получение информации об истории
        return await service.GetStoryAsync(new TgGetStoryRequest
        {
            // Получаем чат
            Chat = request.GetChat(),
            
            // Устанавливаем идентификатор истории
            StoryId = request.StoryId
        }, cancellationToken);
    }
}