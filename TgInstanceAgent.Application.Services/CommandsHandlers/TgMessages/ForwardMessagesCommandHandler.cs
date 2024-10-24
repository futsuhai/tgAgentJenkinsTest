using MediatR;
using Microsoft.Extensions.Caching.Memory;
using TgInstanceAgent.Application.Abstractions.Commands.TgMessages;
using TgInstanceAgent.Application.Abstractions.InstancesService;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Functions.Messages;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Objects.Messages;
using TgInstanceAgent.Application.Services.Extensions;
using TgInstanceAgent.Domain.Abstractions.Repositories;

namespace TgInstanceAgent.Application.Services.CommandsHandlers.TgMessages;

/// <summary>
/// Обработчик команды для пересылки сообщений 
/// </summary>
/// <param name="instancesService">Сервис, используемый для запуска клиентских инстансов.</param>
/// <param name="repository">Репозиторий инстансов</param>
/// <param name="cache">Кэш приложения</param>
public class ForwardMessagesCommandHandler(IInstancesService instancesService, IInstanceRepository repository, IMemoryCache cache)
    : IRequestHandler<ForwardMessagesCommand, IReadOnlyCollection<TgMessage>>
{
    /// <summary>
    /// Обработка команды для пересылки сообщений
    /// </summary>
    /// <param name="request">Команда запроса на пересылку сообщений</param>
    /// <param name="cancellationToken">Токен отмены операции</param>
    /// <returns>Коллекцию сообщений</returns>
    public async Task<IReadOnlyCollection<TgMessage>> Handle(ForwardMessagesCommand request, CancellationToken cancellationToken)
    {
        // Получаем инстанс из кэша/репозитория по указанному идентификатору асинхронно
        var instance = await cache.GetInstanceAsync(request.InstanceId, repository, cancellationToken);
        
        // Проверяем возможность отправки сообщений
        instance.CheckSendRestrictions();
        
        // Запускаем клиентский сервис для пользователя с указанным в запросе идентификатором.
        var service = await instancesService.StartClientAsync(request.InstanceId, cancellationToken);

        // Пересылаем сообщения через сервис
        var messages = await service.ForwardMessagesAsync(new TgForwardMessagesRequest
        {
            // Получаем чат
            Chat = request.GetChat(),

            // Идентификатор чата, с которого идет пересылка сообщения
            FromChatId = request.FromChatId,

            // Идентификаторы пересылаемых сообщений
            MessageIds = request.MessageIds!,

            // Истинно, если требуется убрать подпись в медиа файлах
            RemoveCaption = request.RemoveCaption,

            // Истинно, если требуется переслать сообщения без ссылок на пользователей этих сообщений
            SendCopy = request.SendCopy,
            
            // Получаем параметры отправки сообщения
            MessageSendOptions = request.GetMessageSendOptions()
        }, cancellationToken);
        
        // Обновляем органичения по отправке
        instance.UpdateSendRestrictions();
        
        // Обновляем данные в базе данных
        await repository.UpdateAsync(instance);
        
        // Возвращем сообщения
        return messages;
    }
}