using MediatR;
using TgInstanceAgent.Application.Abstractions.Commands.TgChats;
using TgInstanceAgent.Application.Abstractions.InstancesService;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Functions.Chats;

namespace TgInstanceAgent.Application.Services.CommandsHandlers.TgChats;

/// <summary>
/// Обработчик команды установки группы обсуждения
/// </summary>
/// <param name="instancesService"></param>
public class SetChatDiscussionGroupCommandHandler(IInstancesService instancesService) : IRequestHandler<SetChatDiscussionGroupCommand>
{
    /// <summary>
    /// Обрабатывает команду установки группы обсуждения
    /// </summary>
    /// <param name="request">Команда установки группы обсуждения.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    public async Task Handle(SetChatDiscussionGroupCommand request, CancellationToken cancellationToken)
    {
        // Запуск телеграм клиента инстанса.
        var client = await instancesService.StartClientAsync(request.InstanceId, cancellationToken);

        // Устанавливаем обсуждаемую группу
        await client.SetChatDiscussionGroupAsync(new TgSetChatDiscussionGroupRequest
        {
            // Идентификатор чата
            ChatId = request.ChatId,
            
            // Идентификатор обсуждаемой группы
            DiscussionChatId = request.DiscussionChatId
            
        }, cancellationToken);
    }
}