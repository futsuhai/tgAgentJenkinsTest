using MediatR;
using TgInstanceAgent.Application.Abstractions.Commands.TgProfile;
using TgInstanceAgent.Application.Abstractions.InstancesService;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Functions.Components;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Functions.Profile;
using TgInstanceAgent.Application.Services.Extensions;

namespace TgInstanceAgent.Application.Services.CommandsHandlers.TgProfile;

/// <summary>
/// Обработчик команды установки эмоджи в качестве фотографии профиля.
/// </summary>
/// <param name="instancesService">Сервис для работы с инстансами.</param>
public class SetEmojiProfilePhotoCommandHandler(IInstancesService instancesService)
    : IRequestHandler<SetEmojiProfilePhotoCommand>
{
    /// <summary>
    /// Обрабатывает команду установки фотографии профиля.
    /// </summary>
    /// <param name="request">Команда установки фотографии профиля.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Задача, представляющая асинхронную операцию.</returns>
    public async Task Handle(SetEmojiProfilePhotoCommand request, CancellationToken cancellationToken)
    {
        // Запуск клиента для обработки команды
        var service = await instancesService.StartClientAsync(request.InstanceId, cancellationToken);
        
        // Установка фотографии профиля
        await service.SetProfilePhotoAsync(new TgSetProfilePhotoRequest
        {
            // Создаем объект класса ProfilePhotoEmoji
            ProfilePhoto = new TgInputProfilePhotoEmoji
            {
                // Задаем идентификатор эмоджи
                CustomEmojiId = request.CustomEmojiId,
                
                // Получаем фон с помощь метода расширения GetBackgroundFill
                BackgroundFill = request.GetBackgroundFill()
            }
        }, cancellationToken);
    }
}