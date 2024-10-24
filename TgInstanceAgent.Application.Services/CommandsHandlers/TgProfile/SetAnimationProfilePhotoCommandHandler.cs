using MediatR;
using TgInstanceAgent.Application.Abstractions.Commands.TgProfile;
using TgInstanceAgent.Application.Abstractions.FileStorage;
using TgInstanceAgent.Application.Abstractions.InstancesService;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Functions.Components;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Functions.Profile;
using TgInstanceAgent.Application.Services.Extensions;

namespace TgInstanceAgent.Application.Services.CommandsHandlers.TgProfile;

/// <summary>
/// Обработчик команды установки анимационной фотографии профиля.
/// </summary>
/// <param name="instancesService">Сервис для работы с инстансами.</param>
/// <param name="fileStorage">Менеджер файлов.</param>
public class SetAnimationProfilePhotoCommandHandler(IInstancesService instancesService, IFileStorage fileStorage)
    : IRequestHandler<SetAnimationProfilePhotoCommand>
{
    /// <summary>
    /// Обрабатывает команду установки анимационной фотографии профиля.
    /// </summary>
    /// <param name="request">Команда установки анимационной фотографии профиля.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Задача, представляющая асинхронную операцию.</returns>
    public async Task Handle(SetAnimationProfilePhotoCommand request, CancellationToken cancellationToken)
    {
        // Запуск клиента для обработки команды
        var service = await instancesService.StartClientAsync(request.InstanceId, cancellationToken);

        // Установка фотографии профиля
        await service.SetProfilePhotoAsync(new TgSetProfilePhotoRequest
        {
            // Создаем новый экземпляр
            ProfilePhoto = new TgInputProfilePhotoAnimation
            {
                // Устанавливаем локальный путь до файла
                File = await request.GetInputFileAsync(fileStorage, request.InstanceId),
                
                // Устанавливаем временную метку кадра, с которой закончится анимация.
                MainFrameTimestamp = request.MainFrameTimestamp,
            }
        }, cancellationToken);
    }
}