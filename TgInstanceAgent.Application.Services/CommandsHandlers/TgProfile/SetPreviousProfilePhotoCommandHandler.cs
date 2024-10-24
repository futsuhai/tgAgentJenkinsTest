using MediatR;
using TgInstanceAgent.Application.Abstractions.Commands.TgProfile;
using TgInstanceAgent.Application.Abstractions.InstancesService;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Functions.Components;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Functions.Profile;

namespace TgInstanceAgent.Application.Services.CommandsHandlers.TgProfile;

/// <summary>
/// Обработчик команды установки предыдущей фотографии профиля.
/// </summary>
/// <param name="instancesService">Сервис для работы с инстансами.</param>
public class SetPreviousProfilePhotoCommandHandler(IInstancesService instancesService)
    : IRequestHandler<SetPreviousProfilePhotoCommand>
{
    /// <summary>
    /// Обрабатывает команду установки предыдущей фотографии профиля.
    /// </summary>
    /// <param name="request">Команда установки предыдущей фотографии профиля.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Задача, представляющая асинхронную операцию.</returns>
    public async Task Handle(SetPreviousProfilePhotoCommand request, CancellationToken cancellationToken)
    {
        // Запуск клиента для обработки команды
        var service = await instancesService.StartClientAsync(request.InstanceId, cancellationToken);
        
        // Установка фотографии профиля
        await service.SetProfilePhotoAsync(new TgSetProfilePhotoRequest
        {
            ProfilePhoto = new TgInputProfilePhotoPrevious
            {
                PhotoId = request.PhotoId
            }
        }, cancellationToken);
    }
}