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
/// Обработчик команды для отправки аудиосообщений.
/// </summary>
/// <param name="instancesService">Сервис, используемый для запуска клиентских инстансов.</param>
/// <param name="fileStorage">Сервис, используемый для сохранения файлов локально.</param>
/// <param name="repository">Репозиторий инстансов</param>
/// <param name="cache">Кэш приложения</param>
public class SendAudioMessageCommandHandler(IInstancesService instancesService, IInstanceRepository repository, IMemoryCache cache, IFileStorage fileStorage)
    : IRequestHandler<SendAudioMessageCommand, TgMessage>
{
    /// <summary>
    /// Обработка команды отправки аудиосообщения.
    /// </summary>
    /// <param name="request">Команда запроса на отправку аудиосообщения.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Данные отправленного сообщения</returns>
    public async Task<TgMessage> Handle(SendAudioMessageCommand request, CancellationToken cancellationToken)
    {
        // Получаем инстанс из кэша/репозитория по указанному идентификатору асинхронно
        var instance = await cache.GetInstanceAsync(request.InstanceId, repository, cancellationToken);

        // Проверяем возможность отправки сообщений
        instance.CheckSendRestrictions();
        
        // Запускаем клиентский сервис для пользователя с указанным в запросе идентификатором.
        var service = await instancesService.StartClientAsync(request.InstanceId, cancellationToken);

        // Отправляем аудиосообщение через сервис.
        var message = await service.SendMessageAsync(new TgSendMessageRequest
        {
            // Получаем чат
            Chat = request.GetChat(),
            
            // Данные сообщения
            InputMessageData = new TgInputAudioMessage
            {
                // Текст аудиосообщения
                Caption = request.Caption,

                // Исполнитель
                Performer = request.Performer,

                // Название трека
                Title = request.Title,

                // Файл аудио
                File = await request.GetInputFileAsync(fileStorage, request.InstanceId),
                
                // Устанавливаем превью
                AlbumCoverThumbnail = await request.GetInputThumbnailAsync(fileStorage, request.InstanceId)
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