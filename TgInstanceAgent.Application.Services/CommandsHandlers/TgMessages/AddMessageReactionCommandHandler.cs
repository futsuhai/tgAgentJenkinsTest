using MediatR;
using TgInstanceAgent.Application.Abstractions.Commands.TgMessages;
using TgInstanceAgent.Application.Abstractions.InstancesService;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Functions.Messages;
using TgInstanceAgent.Application.Services.Extensions;

namespace TgInstanceAgent.Application.Services.CommandsHandlers.TgMessages;

/// <summary>
/// Обработчик команды для добавления реакции на сообщение
/// </summary>
/// <param name="instancesService">Сервис, используемый для запуска клиентских инстансов.</param>
public class AddMessageReactionCommandHandler(IInstancesService instancesService)
    : IRequestHandler<AddMessageReactionCommand>
{
    /// <summary>
    /// Обработка команды добавления реакции на сообщение
    /// </summary>
    /// <param name="request">Команда запроса на добавление реакции на сообщение</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    public async Task Handle(AddMessageReactionCommand request, CancellationToken cancellationToken)
    {
        // Запускаем клиентский сервис для пользователя с указанным в запросе идентификатором.
        var service = await instancesService.StartClientAsync(request.InstanceId, cancellationToken);

        // Добавляем реакцию на сообщение через сервис.
        await service.AddMessageReactionAsync(new TgMessageReactionRequest
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