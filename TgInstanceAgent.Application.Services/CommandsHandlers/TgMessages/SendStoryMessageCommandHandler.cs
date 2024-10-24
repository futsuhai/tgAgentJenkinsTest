using MediatR;
using Microsoft.Extensions.Caching.Memory;
using TgInstanceAgent.Application.Abstractions.Commands.TgMessages;
using TgInstanceAgent.Application.Abstractions.InstancesService;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Functions.Components;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Functions.Messages;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Objects.Messages;
using TgInstanceAgent.Application.Services.Extensions;
using TgInstanceAgent.Domain.Abstractions.Repositories;

namespace TgInstanceAgent.Application.Services.CommandsHandlers.TgMessages;

/// <summary>
/// Обработчик команды отправки истории в сообщении
/// </summary>
/// <param name="instancesService">Сервис, используемый для запуска клиентских инстансов.</param>
public class SendStoryMessageCommandHandler(IInstancesService instancesService, IInstanceRepository repository, IMemoryCache cache) : IRequestHandler<SendStoryMessageCommand, TgMessage>
{
    public async Task<TgMessage> Handle(SendStoryMessageCommand request, CancellationToken cancellationToken)
    {
        // Получаем инстанс из кэша/репозитория по указанному идентификатору асинхронно
        var instance = await cache.GetInstanceAsync(request.InstanceId, repository, cancellationToken);

       

        // Проверяем возможность отправки сообщений
        instance.CheckSendRestrictions();
        
        // Запуск клиента для обработки команды
        var service = await instancesService.StartClientAsync(request.InstanceId, cancellationToken);

        // Отправка истории
        var message = await service.SendMessageAsync(new TgSendMessageRequest
        {
            // Получаем чат
            Chat = request.GetChat(),

            // Данные сообщения
            InputMessageData = new TgInputStoryMessage
            {
                // Идентификатор чата
                StorySenderChatId = request.StorySenderChatId,
                
                // Идентификатор истории
                StoryId = request.StoryId
            },
            
            // Получаем параметры отправки сообщения
            MessageSendOptions = request.GetMessageSendOptions()
        }, cancellationToken);
        
        // Обновляем органичения по отправке
        instance.UpdateSendRestrictions();
        
        // Обновляем данные в базе данных
        await repository.UpdateAsync(instance);
        
        // Возвращаем сообщение
        return message;
    }
}