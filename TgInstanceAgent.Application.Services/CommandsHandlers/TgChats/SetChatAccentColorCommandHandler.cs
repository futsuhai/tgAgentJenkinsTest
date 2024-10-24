using MediatR;
using TgInstanceAgent.Application.Abstractions.Commands.TgChats;
using TgInstanceAgent.Application.Abstractions.InstancesService;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Functions.Chats;

namespace TgInstanceAgent.Application.Services.CommandsHandlers.TgChats;

/// <summary>
/// Обработчик команды изменения акцентного цвета чата.
/// </summary>
/// <param name="instancesService"></param>
public class SetChatAccentColorCommandHandler(IInstancesService instancesService) : IRequestHandler<SetChatAccentColorCommand>
{
    /// <summary>
    /// Обрабатывает команду изменения акцентного цвета чата.
    /// </summary>
    /// <param name="request">Команда изменения акцентного цвета чата.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    public async Task Handle(SetChatAccentColorCommand request, CancellationToken cancellationToken)
    {
        // Запуск телеграм клиента инстанса.
        var client = await instancesService.StartClientAsync(request.InstanceId, cancellationToken);

        // Устанавливаем акцентный цвет чата.
        await client.SetChatAccentColorAsync(new TgSetChatAccentColorRequest
        {
            // Идентификатор чата
            ChatId = request.ChatId,
            
            // Акцентный цвет
            AccentColorId = request.AccentColorId,
            
            // Эмозди
            BackgroundCustomEmojiId = request.BackgroundCustomEmojiId
        }, cancellationToken);
    }
}