using MediatR;
using TgInstanceAgent.Application.Abstractions.Commands.TgStories;
using TgInstanceAgent.Application.Abstractions.FileStorage;
using TgInstanceAgent.Application.Abstractions.InstancesService;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Functions.Components;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Functions.Stories;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Objects.Stories;
using TgInstanceAgent.Application.Services.Extensions;

namespace TgInstanceAgent.Application.Services.CommandsHandlers.TgStories;

/// <summary>
/// Обработчик команды установки новой истории пользователя в виде фотографии.
/// </summary>
/// <param name="instancesService">Сервис для работы с инстансами.</param>
/// <param name="fileStorage">Менеджер файлов.</param>
public class PostPhotoStoryCommandHandler(IInstancesService instancesService, IFileStorage fileStorage)
    : IRequestHandler<PostPhotoStoryCommand, TgStory>
{
    /// <summary>
    /// Обрабатывает команду новой истории пользователя в виде фотографии.
    /// </summary>
    /// <param name="request">Команда установки новой истории пользователя в виде фотографии.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Информация об установленной истории.</returns>
    public async Task<TgStory> Handle(PostPhotoStoryCommand request, CancellationToken cancellationToken)
    {
        // Запуск клиента для обработки команды
        var service = await instancesService.StartClientAsync(request.InstanceId, cancellationToken);
        
        // Установка истории
        return await service.PostStoryAsync(new TgPostStoryRequest
        {
            // Получаем чат
            Chat = request.GetChat(),
            
            // Создаем контент фото
            StoryContent = new TgInputStoryContentPhoto
            {
                // Получаем файл
                File = await request.GetInputFileAsync(fileStorage, request.InstanceId)
            },
            
            // Получаем настройки приватности истории
            StoryPrivacySettings = request.GetStoryPrivacySettings(),
            
            // Подпись
            Caption = request.Caption,
            
            // Период действия истории
            ActivePeriod = request.ActivePeriod,
            
            // Истинно, если история защищена от скриншотов и пересылок
            ProtectContent = request.ProtectContent,
        }, cancellationToken);
    }
}