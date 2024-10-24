using MediatR;
using TgInstanceAgent.Application.Abstractions.InstancesService;
using TgInstanceAgent.Application.Abstractions.Queries.TgStories;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Objects.Stories;
using TgInstanceAgent.Application.Services.Extensions;

namespace TgInstanceAgent.Application.Services.QueriesHandlers.TgStories;

/// <summary>
/// Обработчик команды, который получает список активных историй пользователя.
/// </summary>
/// <param name="instancesService">Сервис, используемый для запуска клиентских инстансов.</param>
public class GetChatActiveStoriesQueryHandler(IInstancesService instancesService) : IRequestHandler<GetChatActiveStoriesQuery, TgChatActiveStories>
{
    /// <summary>
    /// Обрабатывает запрос на получение информации об историях пользователя асинхронно.
    /// </summary>
    /// <param name="request">Запрос на получение информация об историях пользователя.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Информация об историях пользователя.</returns>
    public async Task<TgChatActiveStories> Handle(GetChatActiveStoriesQuery request, CancellationToken cancellationToken)
    {
        // Запускаем клиентский сервис для пользователя с указанным в запросе идентификатором.
        var service = await instancesService.StartClientAsync(request.InstanceId, cancellationToken);

        // Получаем и возвращаем список активных историй
        return await service.GetChatActiveStories(request.GetChat(), cancellationToken);
    }
}