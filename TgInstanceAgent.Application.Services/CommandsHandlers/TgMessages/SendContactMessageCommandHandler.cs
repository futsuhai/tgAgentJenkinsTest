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
/// Обработчик команды для отправки сообщения с контактом.
/// </summary>
/// <param name="instancesService">Сервис, используемый для запуска клиентских инстансов.</param>
public class SendContactMessageCommandHandler(IInstancesService instancesService, IInstanceRepository repository, IMemoryCache cache)
    : IRequestHandler<SendContactMessageCommand, TgMessage>
{
    /// <summary>
    /// Обрабатывает запрос на отправку сообщения с контактом.
    /// </summary>
    /// <param name="request">Команда на отправку сообщения с контактом.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Данные отправленного сообщения</returns>
    public async Task<TgMessage> Handle(SendContactMessageCommand request, CancellationToken cancellationToken)
    {
        // Получаем инстанс из кэша/репозитория по указанному идентификатору асинхронно
        var instance = await cache.GetInstanceAsync(request.InstanceId, repository, cancellationToken);
        
        // Проверяем возможность отправки сообщений
        instance.CheckSendRestrictions();
        
        // Запускаем клиент для указанного ID
        var service = await instancesService.StartClientAsync(request.InstanceId, cancellationToken);

        // Создаем и отправляем сообщение
        var message = await service.SendMessageAsync(new TgSendMessageRequest
        {
            // Получаем чат
            Chat = request.GetChat(),
            
            // Данные сообщения
            InputMessageData = new TgInputContactMessage
            {
                FirstName = request.FirstName, // имя
                LastName = request.LastName, // фамилия
                PhoneNumber = request.ContactPhoneNumber, // номер контакта
                VCard = request.VCard, // дополнительная информация
                Id = request.ContactId // идентификатор контакта
            },
            
            // Отправка активности
            NeedShowActivity = request.NeedShowActivity,
            
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