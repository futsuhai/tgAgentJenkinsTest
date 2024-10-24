using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TgInstanceAgent.Application.Abstractions.Queries.TgUsers;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Objects.Components;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Objects.Users;
using TgInstanceAgent.Infrastructure.Web.Filters;
using TgInstanceAgent.Infrastructure.Web.Users.InputModels;

namespace TgInstanceAgent.Infrastructure.Web.Users.Controllers;

/// <summary>
/// Контроллер пользователей телеграмм
/// </summary>
/// <param name="mediator">Медиатор</param>
[Authorize]
[InstanceFilter]
[ApiController]
[Route("apiTg/[controller]/[action]/{instanceId:guid}")]
public class UsersController(ISender mediator, IMapper mapper) : ControllerBase
{
    /// <summary>
    /// Получение информации о пользователе
    /// </summary>
    /// <param name="instanceId">ID инстанса</param>
    /// <param name="model">Данные для получения пользователя</param>
    /// <param name="cancellationToken">Токен для отмены операции</param>
    /// <response code="400">Невалидный запрос</response>
    /// <response code="500">Возникла ошибка на сервере</response>
    [HttpGet]
    public async Task<TgUser> GetUser(Guid instanceId, [FromQuery] GetUserInputModel model,
        CancellationToken cancellationToken)
    {
        // Создаем команду
        var command = mapper.Map<GetUserQuery>(model, options => options.Items.Add("InstanceId", instanceId));

        // Отправляем запрос медиатору на получение пользователя
        return await mediator.Send(command, cancellationToken);
    }

    /// <summary>
    /// Получить био пользователя
    /// </summary>
    /// <param name="instanceId">Идентификатор инстанса</param>
    /// <param name="model">Модель с информацией о пользователе</param>
    /// <param name="cancellationToken">Токен для отмены операции</param>
    [HttpGet]
    public async Task<TgUserFullInfo> GetUserFullInfo(Guid instanceId, [FromQuery] GetUserInputModel model,
        CancellationToken cancellationToken)
    {
        // Создаем команду
        var command = mapper.Map<GetUserFullInfoQuery>(model, options => options.Items.Add("InstanceId", instanceId));

        // Отправляем запрос медиатору на получение информации о пользователе
        return await mediator.Send(command, cancellationToken);
    }

    /// <summary>
    /// Получить список фотографий пользователя
    /// </summary>
    /// <param name="cancellationToken">Токен для отмены операции</param>
    /// <param name="instanceId">Идентификатор инстанса</param>
    /// <param name="model">Модель с информацией о пользователе и запросе</param>
    [HttpGet]
    public async Task<TgCountResult<TgChatPhoto>> GetUserProfilePhotos(Guid instanceId,
        [FromQuery] GetUserProfilePhotosInputModel model, CancellationToken cancellationToken)
    {
        // Создаем команду
        var command =
            mapper.Map<GetUserProfilePhotosQuery>(model, options => options.Items.Add("InstanceId", instanceId));
        
        // Отправляем запрос медиатору на получение фотографий
        return await mediator.Send(command, cancellationToken);
    }
}