using MediatR;
using TgInstanceAgent.Application.Abstractions.Commands.TgProfile;
using TgInstanceAgent.Application.Abstractions.InstancesService;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Functions.Components;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Functions.Profile;
using TgInstanceAgent.Application.Services.Extensions;

namespace TgInstanceAgent.Application.Services.CommandsHandlers.TgProfile;

/// <summary>
/// Обработчик команды установки стикера в качестве фотографии профиля.
/// </summary>
/// <param name="instancesService">Сервис для работы с инстансами.</param>
public class SetStickerProfilePhotoCommandHandler(IInstancesService instancesService)
    : IRequestHandler<SetStickerProfilePhotoCommand>
{
    /// <summary>
    /// Обрабатывает команду установки фотографии профиля.
    /// </summary>
    /// <param name="request">Команда установки фотографии профиля.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Задача, представляющая асинхронную операцию.</returns>
    public async Task Handle(SetStickerProfilePhotoCommand request, CancellationToken cancellationToken)
    {
        // Запуск клиента для обработки команды
        var service = await instancesService.StartClientAsync(request.InstanceId, cancellationToken);
        
        // Установка фотографии профиля
        await service.SetProfilePhotoAsync(new TgSetProfilePhotoRequest
        {
            // Создаем объект класса ProfilePhotoSticker
            ProfilePhoto = new TgInputProfilePhotoSticker
            {
                // Задаем идентификатор набора стикеров
                StickerSetId = request.StickerSetId,
                
                // Задаем идентификатор стикера
                StickerId = request.StickerId,
                
                // Получаем фон с помощь метода расширения GetBackgroundFill
                BackgroundFill = request.GetBackgroundFill()
            }
        }, cancellationToken);
    }
}