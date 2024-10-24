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
/// Обработчик команды для отправки сообщения с документом.
/// </summary>
/// <param name="instancesService">Сервис, используемый для запуска клиентских инстансов.</param>
/// <param name="fileStorage">Сервис, используемый для сохранения файлов локально.</param>
public class SendDocumentMessageCommandHandler(IInstancesService instancesService, IInstanceRepository repository, IMemoryCache cache, IFileStorage fileStorage)
    : IRequestHandler<SendDocumentMessageCommand, TgMessage>
{
    /// <summary>
    /// Асинхронная обработка отправки сообщения с документом.
    /// </summary>
    /// <param name="request">Запрос с данными для отправки документа.</param>
    /// <param name="cancellationToken">Токен для отслеживания отмены операции.</param>
    /// <returns>Данные отправленного сообщения</returns>
    public async Task<TgMessage> Handle(SendDocumentMessageCommand request, CancellationToken cancellationToken)
    {
        // Получаем инстанс из кэша/репозитория по указанному идентификатору асинхронно
        var instance = await cache.GetInstanceAsync(request.InstanceId, repository, cancellationToken);

        // Проверяем возможность отправки сообщений
        instance.CheckSendRestrictions();
        
        // Запускаем клиентский сервис для указанного идентификатора инстанса
        var service = await instancesService.StartClientAsync(request.InstanceId, cancellationToken);

        // Создаем запрос на отправку сообщения с документом
        var message = await service.SendMessageAsync(new TgSendMessageRequest
        {
            // Получаем чат
            Chat = request.GetChat(),

            // Заполняем данные для документа, включая заголовок и пути к файлу и миниатюре
            InputMessageData = new TgInputDocumentMessage
            {
                // Устанавливаем описание
                Caption = request.Caption,
                
                // Устанавливаем файл
                File = await request.GetInputFileAsync(fileStorage, request.InstanceId)
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