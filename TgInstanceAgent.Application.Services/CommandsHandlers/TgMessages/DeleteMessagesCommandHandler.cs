using MediatR;
using TgInstanceAgent.Application.Abstractions.Commands.TgMessages;
using TgInstanceAgent.Application.Abstractions.InstancesService;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Functions.Messages;
using TgInstanceAgent.Application.Services.Extensions;

namespace TgInstanceAgent.Application.Services.CommandsHandlers.TgMessages;

/// <summary>
/// Обработчик команды для удаления сообщений в чате
/// </summary>
/// <param name="instancesService">Сервис, используемый для запуска клиентских инстансов.</param>
public class DeleteMessagesCommandHandler(IInstancesService instancesService)
    : IRequestHandler<DeleteMessagesCommand>
{
    /// <summary>
    /// Обработка команды для удаления сообщений в чате
    /// </summary>
    /// <param name="request">Команда для удаления сообщей в чате</param>
    /// <param name="cancellationToken">Токен отмены операции</param>
    /// <returns></returns>
    public async Task Handle(DeleteMessagesCommand request, CancellationToken cancellationToken)
    {
        // Запускаем клиентский сервис для пользователя с указанным в запросе идентификатором.
        var service = await instancesService.StartClientAsync(request.InstanceId, cancellationToken);
        
        // Удаляем сообщения через сервис
        await service.DeleteMessagesAsync(new TgDeleteMessagesRequest
        {
            // Получаем чат
            Chat = request.GetChat(),
            
            // Идентификаторы удаляемых сообщений
            MessageIds = request.MessageIds,
            
            // Истинно, если требуется удалить для всех участников чата
            Revoke = request.Revoke
        }, cancellationToken);
    }
}