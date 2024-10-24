using MediatR;
using TgInstanceAgent.Application.Abstractions.Commands.TgMessages;
using TgInstanceAgent.Application.Abstractions.InstancesService;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Functions.Messages;
using TgInstanceAgent.Application.Services.Extensions;

namespace TgInstanceAgent.Application.Services.CommandsHandlers.TgMessages;

/// <summary>
/// Обработчик команды для удаления реакции с сообщения
/// </summary>
/// <param name="instancesService">Сервис, используемый для запуска клиентских инстансов.</param>
public class RemoveMessageReactionCommandHandler(IInstancesService instancesService)
    : IRequestHandler<RemoveMessageReactionCommand>
{
    /// <summary>
    /// Обработка команды удаления реакции с сообщения
    /// </summary>
    /// <param name="request">Команда запроса на удаление реакции с сообщения.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    public async Task Handle(RemoveMessageReactionCommand request, CancellationToken cancellationToken)
    {
        // Запускаем клиентский сервис для пользователя с указанным в запросе идентификатором.
        var service = await instancesService.StartClientAsync(request.InstanceId, cancellationToken);

        // Удаляем реакцию с сообщения через сервис.
        await service.RemoveMessageReactionAsync(new TgMessageReactionRequest
        {
            // Получаем чат
            Chat = request.GetChat(),

            // Получаем реакцию
            Reaction = request.GetReaction(),
            
            // Устанавливаем идентификатор сообщения
            MessageId = request.MessageId
        }, cancellationToken);
    }
}