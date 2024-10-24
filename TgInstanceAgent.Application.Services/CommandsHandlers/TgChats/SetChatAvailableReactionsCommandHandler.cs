using MediatR;
using TgInstanceAgent.Application.Abstractions.Commands.TgChats;
using TgInstanceAgent.Application.Abstractions.InstancesService;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Functions.Chats;
using TgInstanceAgent.Application.Services.Extensions;

namespace TgInstanceAgent.Application.Services.CommandsHandlers.TgChats;

/// <summary>
/// Обработчик команды изменения доступных в чате реакций.
/// </summary>
/// <param name="instancesService"></param>
public class SetChatAvailableReactionsCommandHandler(IInstancesService instancesService) : IRequestHandler<SetChatAvailableReactionsCommand>
{
    /// <summary>
    /// Обрабатывает команду изменения доступных в чате реакций.
    /// </summary>
    /// <param name="request">Команда изменения доступных в чате реакций.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    public async Task Handle(SetChatAvailableReactionsCommand request, CancellationToken cancellationToken)
    {
        // Запуск телеграм клиента инстанса.
        var client = await instancesService.StartClientAsync(request.InstanceId, cancellationToken);

        // Изменяем доступные в чате реакции.
        await client.SetChatAvailableReactionsAsync(new TgSetChatAvailableReactionsRequest
        {
            // Идентификатор чата
            ChatId = request.ChatId,
            
            // Максимальное количество реакций
            MaxReactionCount = request.MaxReactionCount,
            
            // Реакции
            Reactions = request.GetReactions(),
        }, cancellationToken);
    }
}