using MediatR;
using TgInstanceAgent.Application.Abstractions.Commands.TgChats;
using TgInstanceAgent.Application.Abstractions.FileStorage;
using TgInstanceAgent.Application.Abstractions.InstancesService;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Functions.Chats;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Functions.Components;
using TgInstanceAgent.Application.Services.Extensions;

namespace TgInstanceAgent.Application.Services.CommandsHandlers.TgChats;

/// <summary>
/// Обработчик команды установки емозди в качестве фотографии профиля группового чата.
/// </summary>
/// <param name="instancesService">Сервис для работы с инстансами.</param>
/// <param name="fileStorage">Менеджер файлов.</param>
public class SetChatEmojiProfilePhotoCommandHandler(IInstancesService instancesService, IFileStorage fileStorage)
    : IRequestHandler<SetChatEmojiProfilePhotoCommand>
{
    /// <summary>
    /// Обрабатывает команду установки емозди в качестве фотографии профиля группового чата.
    /// </summary>
    /// <param name="request">Команда установки емозди в качестве фотографии профиля группового чата.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Задача, представляющая асинхронную операцию.</returns>
    public async Task Handle(SetChatEmojiProfilePhotoCommand request, CancellationToken cancellationToken)
    {
        // Запуск клиента для обработки команды
        var service = await instancesService.StartClientAsync(request.InstanceId, cancellationToken);
        
        // Установка емозди в качестве фотографии профиля группового чата
        await service.SetChatPhotoAsync(new TgSetChatProfilePhotoRequest
        {
            ChatId = request.ChatId,
            
            ChatProfilePhoto = new TgInputProfilePhotoEmoji
            {
                // Задаем идентификатор эмоджи
                CustomEmojiId = request.CustomEmojiId,
                
                // Получаем фон с помощью метода расширения GetBackgroundFill
                BackgroundFill = request.GetBackgroundFill()
            }
        }, cancellationToken);
    }
}