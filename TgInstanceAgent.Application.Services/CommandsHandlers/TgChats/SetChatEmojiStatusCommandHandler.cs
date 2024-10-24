using MediatR;
using TgInstanceAgent.Application.Abstractions.Commands.TgChats;
using TgInstanceAgent.Application.Abstractions.InstancesService;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Functions.Chats;

namespace TgInstanceAgent.Application.Services.CommandsHandlers.TgChats;

/// <summary>
/// Обработчик команды изменения статуса емозди у чата.
/// </summary>
/// <param name="instancesService"></param>
public class SetChatEmojiStatusCommandHandler(IInstancesService instancesService) : IRequestHandler<SetChatEmojiStatusCommand>
{
    /// <summary>
    /// Обрабатывает команду статуса емозди у чата.
    /// </summary>
    /// <param name="request">Команда изменения статуса емозди у чата.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    public async Task Handle(SetChatEmojiStatusCommand request, CancellationToken cancellationToken)
    {
        // Запуск телеграм клиента инстанса.
        var client = await instancesService.StartClientAsync(request.InstanceId, cancellationToken);

        // Изменяем статус емозди у чата.
        await client.SetChatEmojiStatusAsync(new TgSetChatEmojiStatusRequest
        {
            // Идентификатор чата
            ChatId = request.ChatId,
            
            // Идентификатор пользовательского эмодзи
            CustomEmojiId = request.CustomEmojiId,
            
            // Дата истечения срока действия статуса эмодзи
            ExpirationDate = request.ExpirationDate
        }, cancellationToken);
    }
}