using MediatR;
using TgInstanceAgent.Application.Abstractions.InstancesService;
using TgInstanceAgent.Application.Abstractions.Queries.TgChats;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Objects.Chats;

namespace TgInstanceAgent.Application.Services.QueriesHandlers.TgChats;

/// <summary>
/// Обработчик запроса на получение папок чатов.
/// </summary>
public class GetChatFoldersQueryHandler(IInstancesService instancesService) : IRequestHandler<GetChatFoldersQuery, TgChatFolders>
{
    /// <summary>
    /// Обрабатывает запрос на получение папок чатов асинхронно.
    /// </summary>
    /// <param name="request">Запрос на получение папок чатов.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Папки чатов.</returns>
    public async Task<TgChatFolders> Handle(GetChatFoldersQuery request,
        CancellationToken cancellationToken)
    {
        // Запуск телеграм клиента инстанса
        var telegramClient = await instancesService.StartClientAsync(request.InstanceId, cancellationToken);

        // Получение папок чатов
        return telegramClient.GetChatFolders();
    }
}