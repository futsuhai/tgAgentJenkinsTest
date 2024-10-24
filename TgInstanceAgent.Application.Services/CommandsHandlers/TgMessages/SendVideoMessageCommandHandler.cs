using MediatR;
using Microsoft.Extensions.Caching.Memory;
using TgInstanceAgent.Application.Abstractions.Commands.TgMessages;
using TgInstanceAgent.Application.Abstractions.FileStorage;
using TgInstanceAgent.Application.Abstractions.InstancesService;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Functions.Components;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Functions.Messages;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Objects.Messages;
using TgInstanceAgent.Application.Services.Extensions;
using TgInstanceAgent.Domain.Abstractions.Repositories;

namespace TgInstanceAgent.Application.Services.CommandsHandlers.TgMessages;

/// <summary>
/// Обработчик команды для отправки видеосообщения.
/// </summary>
/// <param name="instancesService">Сервис, используемый для запуска клиентских инстансов.</param>
/// <param name="fileStorage">Сервис, используемый для сохранения файлов локально.</param>
public class SendVideoMessageCommandHandler(IInstancesService instancesService, IInstanceRepository repository, IMemoryCache cache, IFileStorage fileStorage)
    : IRequestHandler<SendVideoMessageCommand, TgMessage>
{
    /// <summary>
    /// Обрабатывает запрос на отправку видеосообщения.
    /// </summary>
    /// <param name="request">Команда на отправку видеосообщения.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Данные отправленного сообщения</returns>
    public async Task<TgMessage> Handle(SendVideoMessageCommand request, CancellationToken cancellationToken)
    {
        // Получаем инстанс из кэша/репозитория по указанному идентификатору асинхронно
        var instance = await cache.GetInstanceAsync(request.InstanceId, repository, cancellationToken);
        
        // Проверяем возможность отправки сообщений
        instance.CheckSendRestrictions();
        
        // Запускаем клиент для указанного ID
        var service = await instancesService.StartClientAsync(request.InstanceId, cancellationToken);
        
        // Создаем и отправляем видеосообщение
        var message = await service.SendMessageAsync(new TgSendMessageRequest
        {
            // Получаем чат
            Chat = request.GetChat(),

            // Данные сообщения
            InputMessageData = new TgInputVideoMessage
            {
                // Устанавливаем описание
                Caption = request.Caption,
                
                // Устанавливаем файл
                File = await request.GetInputFileAsync(fileStorage, request.InstanceId),
                
                // Отправка активности о записи видео
                NeedShowRecordingActivity = request.NeedShowRecordingActivity
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

        return message;
    }
}