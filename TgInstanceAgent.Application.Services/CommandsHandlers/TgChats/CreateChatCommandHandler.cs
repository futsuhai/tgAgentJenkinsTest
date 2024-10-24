using MediatR;
using TgInstanceAgent.Application.Abstractions.Commands.TgChats;
using TgInstanceAgent.Application.Abstractions.InstancesService;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Functions.Chats;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Objects.Chats;

namespace TgInstanceAgent.Application.Services.CommandsHandlers.TgChats;

/// <summary>
/// Обработчик команды создания чата.
/// </summary>
public class CreateChatCommandHandler(IInstancesService instancesService)
    : IRequestHandler<CreateChatCommand, TgChat>
{
    /// <summary>
    /// Обрабатывает команду создания чата.
    /// </summary>
    /// <param name="request">Команда создания чата.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    public async Task<TgChat> Handle(CreateChatCommand request, CancellationToken cancellationToken)
    {
        // Запуск телеграм клиента инстанса.
        var client = await instancesService.StartClientAsync(request.InstanceId, cancellationToken);

        // Создаем и возвращаем чат.
        return await client.CreateChatAsync(new TgCreateChatRequest() { ChatType = request.ChatType, Id = request.Id },
            cancellationToken);
    }
}