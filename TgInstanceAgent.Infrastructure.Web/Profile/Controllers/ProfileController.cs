using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TgInstanceAgent.Application.Abstractions.Commands.TgProfile;
using TgInstanceAgent.Application.Abstractions.Queries.TgUsers;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Objects.Users;
using TgInstanceAgent.Infrastructure.Web.Filters;
using TgInstanceAgent.Infrastructure.Web.Profile.InputModels;

namespace TgInstanceAgent.Infrastructure.Web.Profile.Controllers;

/// <summary>
/// Контроллер отвечающий за работу с профилем
/// </summary>
/// <param name="mediator">Медиатор</param>
[Authorize]
[InstanceFilter]
[ApiController]
[Route("apiTg/[controller]/[action]/{instanceId:guid}")]
public class ProfileController(ISender mediator, IMapper mapper) : ControllerBase
{
    /// <summary>
    /// Устанавливает фотографию профиля.
    /// Перед установкой по идентификатору файла необходимо предварительно убедиться, что файл загружен на сервер.
    /// </summary>
    /// <param name="instanceId">Идентификатор инстанса.</param>
    /// <param name="model">Данные для установки фотографии профиля.</param>
    /// <param name="cancellationToken">Токен для отмены операции</param>
    [HttpPost]
    public async Task SetPictureProfilePhoto(Guid instanceId, SetPictureProfilePhotoInputModel model,
        CancellationToken cancellationToken)
    {
        // Создаем команду
        var command =
            mapper.Map<SetPictureProfilePhotoCommand>(model, options => options.Items.Add("InstanceId", instanceId));

        try
        {
            // Отправляем запрос медиатору на отправку анимации
            await mediator.Send(command, cancellationToken);
        }
        finally
        {
            // Закрываем поток с файлом вручную после выполнения команды
            if (command.File != null) await command.File.DisposeAsync();
        }
    }

    /// <summary>
    /// Устанавливает одну из предыдущих фотографий пользователя по id фотографии.
    /// </summary>
    /// <param name="instanceId">Идентификатор инстанса.</param>
    /// <param name="photoId">Идентификатор фотографии.</param>
    /// <param name="cancellationToken">Токен для отмены операции</param>
    [HttpPost]
    public async Task SetPreviousProfilePhoto(Guid instanceId, long photoId, CancellationToken cancellationToken)
    {
        // Отправляем команду на установку предыдущей фотографии профиля
        await mediator.Send(new SetPreviousProfilePhotoCommand
        {
            // Идентификатор инстанса.
            InstanceId = instanceId,

            // Идентификатор предыдущей фотографии, которую нужно установить.
            PhotoId = photoId
        }, cancellationToken);
    }

    /// <summary>
    /// Устанавливает фотографию профиля в виде кастомного эмоджи.
    /// </summary>
    /// <param name="instanceId">Идентификатор инстанса.</param>
    /// <param name="model">Данные для установки фотографии профиля.</param>
    /// <param name="cancellationToken">Токен для отмены операции</param>
    [HttpPost]
    public async Task SetEmojiProfilePhoto(Guid instanceId, SetEmojiProfilePhotoInputModel model,
        CancellationToken cancellationToken)
    {
        // Создаем команду
        var command =
            mapper.Map<SetEmojiProfilePhotoCommand>(model, options => options.Items.Add("InstanceId", instanceId));

        // Отправляем команду на установку профильной фотографии 
        await mediator.Send(command, cancellationToken);
    }

    /// <summary>
    /// Устанавливает фотографию профиля в виде стикера.
    /// </summary>
    /// <param name="instanceId">Идентификатор инстанса.</param>
    /// <param name="model">Данные для установки фотографии профиля.</param>
    /// <param name="cancellationToken">Токен для отмены операции</param>
    [HttpPost]
    public async Task SetStickerProfilePhoto(Guid instanceId, SetStickerProfilePhotoInputModel model,
        CancellationToken cancellationToken)
    {
        // Создаем команду
        var command =
            mapper.Map<SetStickerProfilePhotoCommand>(model, options => options.Items.Add("InstanceId", instanceId));

        // Отправляем команду на установку профильной фотографии 
        await mediator.Send(command, cancellationToken);
    }

    /// <summary>
    /// Устанавливает фотографию профиля в виде анимации.
    /// Перед установкой по идентификатору файла необходимо предварительно убедиться, что файл загружен на сервер.
    /// </summary>
    /// <param name="instanceId">Идентификатор инстанса.</param>
    /// <param name="model">Данные для установки фотографии профиля.</param>
    /// <param name="cancellationToken">Токен для отмены операции</param>
    [HttpPost]
    public async Task SetAnimationProfilePhoto(Guid instanceId, SetAnimationProfilePhotoInputModel model,
        CancellationToken cancellationToken)
    {
        // Создаем команду
        var command =
            mapper.Map<SetAnimationProfilePhotoCommand>(model, options => options.Items.Add("InstanceId", instanceId));

        try
        {
            // Отправляем запрос медиатору на отправку анимации
            await mediator.Send(command, cancellationToken);
        }
        finally
        {
            // Закрываем поток с файлом вручную после выполнения команды
            if (command.File != null) await command.File.DisposeAsync();
        }
    }

    /// <summary>
    /// Установить био пользователю.
    /// </summary>
    /// <param name="instanceId">Идентификатор инстанса.</param>
    /// <param name="model">Модель с информацией о био пользователя.</param>
    /// <param name="cancellationToken">Токен для отмены операции</param>
    [HttpPost]
    public async Task SetBio(Guid instanceId, SetUserBioInputModel model, CancellationToken cancellationToken)
    {
        // Создаем команду
        var command = mapper.Map<SetUserBioCommand>(model, options => options.Items.Add("InstanceId", instanceId));

        // Отправляем команду на установку информации о пользователе 
        await mediator.Send(command, cancellationToken);
    }
    
    /// <summary>
    /// Получение информации о авторизованном пользователе
    /// </summary>
    /// <param name="instanceId">Идентификатор инстанса</param>
    /// <param name="cancellationToken">Токен для отмены операции</param>
    /// <response code="400">Невалидный запрос</response>
    /// <response code="500">Возникла ошибка на сервере</response>
    [HttpGet]
    public async Task<TgUser> GetMe(Guid instanceId, CancellationToken cancellationToken)
    {
        // Отправляем запрос медиатору на получение пользователя
        return await mediator.Send(new GetMeQuery
        {
            // Идентификатор инстанса
            InstanceId = instanceId,
        }, cancellationToken);
    }
}