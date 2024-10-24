using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TgInstanceAgent.Application.Abstractions.Commands.TgStories;
using TgInstanceAgent.Application.Abstractions.Queries.TgStories;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Objects.Stories;
using TgInstanceAgent.Infrastructure.Web.Filters;
using TgInstanceAgent.Infrastructure.Web.Stories.InputModels;

namespace TgInstanceAgent.Infrastructure.Web.Stories.Controllers;

/// <summary>
/// Контроллер отвечающий за работу с историями
/// </summary>
/// <param name="mediator">Медиатор</param>
[Authorize]
[InstanceFilter]
[ApiController]
[Route("apiTg/[controller]/[action]/{instanceId:guid}")]
public class StoriesController(ISender mediator, IMapper mapper) : ControllerBase
{
    /// <summary>
    /// Устанавливает новую историю пользователю в виде фотографии
    /// </summary>
    /// <param name="instanceId">Идентификатор инстанса.</param>
    /// <param name="model">Данные для установки истории.</param>
    /// <param name="cancellationToken">Токен для отмены операции</param>
    /// <returns>Информацию о выложенной истории.</returns>
    [HttpPost]
    public async Task<TgStory> PostPhotoStory(Guid instanceId, PostPhotoStoryInputModel model,
        CancellationToken cancellationToken)
    {
        // Создаем команду
        var command = mapper.Map<PostPhotoStoryCommand>(model, options => options.Items.Add("InstanceId", instanceId));

        try
        {
            // Отправляем запрос медиатору на отправку анимации
            return await mediator.Send(command, cancellationToken);
        }
        finally
        {
            // Закрываем поток с файлом вручную после выполнения команды
            if (command.File != null) await command.File.DisposeAsync();
        }
    }

    /// <summary>
    /// Устанавливает новую историю пользователю в виде видео
    /// </summary>
    /// <param name="instanceId">Идентификатор инстанса.</param>
    /// <param name="model">Данные для установки истории.</param>
    /// <param name="cancellationToken">Токен для отмены операции</param>
    /// <returns>Информацию о выложенной истории.</returns>
    [HttpPost]
    public async Task<TgStory> PostVideoStory(Guid instanceId, PostVideoStoryInputModel model,
        CancellationToken cancellationToken)
    {
        // Создаем команду
        var command = mapper.Map<PostVideoStoryCommand>(model, options => options.Items.Add("InstanceId", instanceId));

        try
        {
            // Отправляем запрос медиатору на отправку анимации
            return await mediator.Send(command, cancellationToken);
        }
        finally
        {
            // Закрываем поток с файлом вручную после выполнения команды
            if (command.File != null) await command.File.DisposeAsync();
        }
    }

    /// <summary>
    /// Получает историю пользователя по ее идентификатору
    /// </summary>
    /// <param name="instanceId">Идентификатор инстанса.</param>
    /// <param name="model">Данные для получения истории.</param>
    /// <param name="cancellationToken">Токен для отмены операции</param>
    /// <returns>Информацию об истории.</returns>
    [HttpGet]
    public async Task<TgStory> GetStory(Guid instanceId, [FromQuery] GetStoryInputModel model,
        CancellationToken cancellationToken)
    {
        // Создаем команду
        var command = mapper.Map<GetStoryQuery>(model, options => options.Items.Add("InstanceId", instanceId));

        // Отправляем запрос на получение истории
        return await mediator.Send(command, cancellationToken);
    }

    /// <summary>
    /// Получить информацию об активных историях пользователя
    /// </summary>
    /// <param name="instanceId">Идентификатор инстанса</param>
    /// <param name="model">Модель с информацией о пользователе</param>
    /// <param name="cancellationToken">Токен для отмены операции</param>
    /// <response code="400">Невалидный запрос</response>
    /// <response code="500">Возникла ошибка на сервере</response>
    [HttpGet]
    public async Task<TgChatActiveStories> GetChatActiveStories(Guid instanceId,
        [FromQuery] GetChatActiveStoriesInputModel model, CancellationToken cancellationToken)
    {
        // Создаем команду
        var command =
            mapper.Map<GetChatActiveStoriesQuery>(model, options => options.Items.Add("InstanceId", instanceId));

        // Отправляем запрос медиатору на получение активных историях пользователя
        return await mediator.Send(command, cancellationToken);
    }
}