using MediatR;
using TgInstanceAgent.Application.Abstractions.InstancesService;
using TgInstanceAgent.Application.Abstractions.Queries.TgChats;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Functions.Chats;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Objects.Chats;

namespace TgInstanceAgent.Application.Services.QueriesHandlers.TgChats;

/// <summary>
/// Обработчик запроса на получение списка чатов.
/// </summary>
public class GetChatsQueryHandler(IInstancesService instancesService)
    : IRequestHandler<GetChatsQuery, Abstractions.TelegramClient.Objects.Chats.TgChats>
{
    /// <summary>
    /// Обрабатывает запрос на получение списка чатов асинхронно.
    /// </summary>
    /// <param name="request">Запрос на получение списка чатов.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Коллекцию чатов.</returns>
    public async Task<Abstractions.TelegramClient.Objects.Chats.TgChats> Handle(GetChatsQuery request, CancellationToken cancellationToken)
    {
        // Создаем клиент ватсап
        var telegramClient = await instancesService.StartClientAsync(request.InstanceId, cancellationToken);

        // Получаем список чатов пользователя
        return await telegramClient.GetChatsAsync(new TgGetChatsRequest
        {
            List = request.List,
            ChatFolderId = request.ChatFolderId,
            FromChatId = request.FromChatId,
            Limit = request.Limit,
            Offset = request.Offset,
            IncludeUsers = request.IncludeUsers
        }, cancellationToken);
    }
}