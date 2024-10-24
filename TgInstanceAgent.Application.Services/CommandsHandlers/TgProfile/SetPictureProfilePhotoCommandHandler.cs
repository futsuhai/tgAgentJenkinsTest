using MediatR;
using TgInstanceAgent.Application.Abstractions.Commands.TgProfile;
using TgInstanceAgent.Application.Abstractions.FileStorage;
using TgInstanceAgent.Application.Abstractions.InstancesService;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Functions.Components;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Functions.Profile;
using TgInstanceAgent.Application.Services.Extensions;

namespace TgInstanceAgent.Application.Services.CommandsHandlers.TgProfile;

/// <summary>
/// Обработчик команды установки фотографии профиля.
/// </summary>
/// <param name="instancesService">Сервис для работы с инстансами.</param>
/// <param name="fileStorage">Менеджер файлов.</param>
public class SetPictureProfilePhotoCommandHandler(IInstancesService instancesService, IFileStorage fileStorage)
    : IRequestHandler<SetPictureProfilePhotoCommand>
{
    /// <summary>
    /// Обрабатывает команду установки фотографии профиля.
    /// </summary>
    /// <param name="request">Команда установки фотографии профиля.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Задача, представляющая асинхронную операцию.</returns>
    public async Task Handle(SetPictureProfilePhotoCommand request, CancellationToken cancellationToken)
    {
        // Запуск клиента для обработки команды
        var service = await instancesService.StartClientAsync(request.InstanceId, cancellationToken);
        
        // Установка фотографии профиля
        await service.SetProfilePhotoAsync(new TgSetProfilePhotoRequest
        {
            ProfilePhoto = new TgInputProfilePhotoPicture
            {
                File = await request.GetInputFileAsync(fileStorage, request.InstanceId)
            }
        }, cancellationToken);
    }
}