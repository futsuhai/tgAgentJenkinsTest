using MediatR;
using TgInstanceAgent.Application.Abstractions.Commands.TgChats;
using TgInstanceAgent.Application.Abstractions.FileStorage;
using TgInstanceAgent.Application.Abstractions.InstancesService;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Functions.Chats;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Functions.Components;
using TgInstanceAgent.Application.Services.Extensions;

namespace TgInstanceAgent.Application.Services.CommandsHandlers.TgChats;

/// <summary>
/// Обработчик команды установки фотографии профиля группового чата.
/// </summary>
/// <param name="instancesService">Сервис для работы с инстансами.</param>
/// <param name="fileStorage">Менеджер файлов.</param>
public class SetChatProfilePhotoCommandHandler(IInstancesService instancesService, IFileStorage fileStorage)
    : IRequestHandler<SetChatProfilePhotoCommand>
{
    /// <summary>
    /// Обрабатывает команду установки фотографии профиля группового чата.
    /// </summary>
    /// <param name="request">Команда установки фотографии профиля группового чата.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Задача, представляющая асинхронную операцию.</returns>
    public async Task Handle(SetChatProfilePhotoCommand request, CancellationToken cancellationToken)
    {
        // Запуск клиента для обработки команды
        var service = await instancesService.StartClientAsync(request.InstanceId, cancellationToken);
        
        // Установка фотографии профиля группового чата
        await service.SetChatPhotoAsync(new TgSetChatProfilePhotoRequest
        {
            ChatId = request.ChatId,
            
            ChatProfilePhoto = new TgInputProfilePhotoPicture
            {
                File = await request.GetInputFileAsync(fileStorage, request.InstanceId)
            }
        }, cancellationToken);
    }
}