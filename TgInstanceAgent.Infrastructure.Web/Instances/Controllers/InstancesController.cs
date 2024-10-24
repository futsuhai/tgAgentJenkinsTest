using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TgInstanceAgent.Application.Abstractions.Commands.Instances;
using TgInstanceAgent.Application.Abstractions.DTOs.Instances;
using TgInstanceAgent.Application.Abstractions.Queries.Instances;
using TgInstanceAgent.Infrastructure.Web.Filters;
using TgInstanceAgent.Infrastructure.Web.Instances.InputModels;

namespace TgInstanceAgent.Infrastructure.Web.Instances.Controllers;

/// <summary>
/// Контроллер отвечающий за работу с инстансами
/// </summary>
/// <param name="mediator">Медиатор</param>
/// <param name="mapper">Медиатор</param>
[Authorize]
[InstanceFilter]
[ApiController]
[Route("apiTg/[controller]/[action]/{instanceId:guid}")]
public class InstancesController(ISender mediator, IMapper mapper) : ControllerBase
{
    /// <summary>
    /// Запускает инстанс по заданному идентификатору.
    /// </summary>
    /// <param name="instanceId">Идентификатор инстанса.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Результат выполнения операции.</returns>
    [HttpPost]
    public async Task Start(Guid instanceId, CancellationToken cancellationToken)
    {
        // Маппинг моделей ввода в модели команд
        var command = new StartInstanceCommand {CommandId = Guid.NewGuid(), InstanceId = instanceId };

        // Отправляем запрос медиатору на запуск инстанса
        await mediator.Send(command, cancellationToken);
    }

    /// <summary>
    /// Останавливает инстанс по заданному идентификатору.
    /// </summary>
    /// <param name="instanceId">Идентификатор инстанса.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Результат выполнения операции.</returns>
    [HttpPost]
    public async Task Stop(Guid instanceId, CancellationToken cancellationToken)
    {
        // Маппинг моделей ввода в модели команд
        var command = new StopInstanceCommand {CommandId = Guid.NewGuid(), InstanceId = instanceId };

        // Отправляем запрос медиатору на остановку инстанса
        await mediator.Send(command, cancellationToken);
    }

    /// <summary>
    /// Останавливает инстанс по заданному идентификатору.
    /// </summary>
    /// <param name="instanceId">Идентификатор инстанса.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Результат выполнения операции.</returns>
    [HttpGet]
    public async Task<InstanceDto> Get(Guid instanceId, CancellationToken cancellationToken)
    {
        // Маппинг моделей ввода в модели команд
        var query = new GetInstanceQuery { InstanceId = instanceId };

        // Отправляем запрос медиатору на получение инстанса
        return await mediator.Send(query, cancellationToken);
    }

    /// <summary>
    /// Добавление ссылки на Webhook.
    /// </summary>
    /// <param name="instanceId">Идентификатор инстанса.</param>
    /// <param name="model">Входная модель для добаления ссылки.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <response code="400">Невалидный запрос</response>
    /// <response code="500">Возникла ошибка на сервере</response>
    [HttpPost]
    public async Task AddWebhookUrl(Guid instanceId, UrlInputModel model,
        CancellationToken cancellationToken)
    {
        // Маппинг моделей ввода в модели команд
        var command = mapper.Map<AddWebhookUrlCommand>(model, options => options.Items.Add("InstanceId", instanceId));

        // Отправляем запрос медиатору на добавление новой ссылки
        await mediator.Send(command, cancellationToken);
    }

    /// <summary>
    /// Удаление ссылки на Webhook.
    /// </summary>
    /// <param name="instanceId">Идентификатор инстанса.</param>
    /// <param name="model">Входная модель для удаления ссылки.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <response code="400">Невалидный запрос</response>
    /// <response code="500">Возникла ошибка на сервере</response>
    [HttpPost]
    public async Task RemoveWebhookUrl(Guid instanceId, UrlInputModel model, CancellationToken cancellationToken)
    {
        // Маппинг моделей ввода в модели команд
        var command =
            mapper.Map<RemoveWebhookUrlCommand>(model, options => options.Items.Add("InstanceId", instanceId));

        // Отправляем запрос медиатору на удаление ссылки
        await mediator.Send(command, cancellationToken);
    }

    /// <summary>
    /// Обновление настроек вебхука.
    /// </summary>
    /// <param name="instanceId">Идентификатор инстанса.</param>
    /// <param name="model">Модель ввода с новыми настройками вебхука.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <response code="400">Невалидный запрос</response>
    /// <response code="404">Не найден инстанс с указанным идентификатором</response>
    /// <response code="500">Возникла ошибка на сервере</response>
    [HttpPost]
    public async Task UpdateWebhookSetting(Guid instanceId, UpdateWebhookSettingInputModel model,
        CancellationToken cancellationToken)
    {
        // Маппинг моделей ввода в модели команд
        var command =
            mapper.Map<UpdateWebhookSettingCommand>(model, options => options.Items.Add("InstanceId", instanceId));

        // Отправляем запрос медиатору на добавление новой пересылки
        await mediator.Send(command, cancellationToken);
    }
    
    /// <summary>
    /// Добавление новой пересылки сообщений.
    /// </summary>
    /// <param name="instanceId">Идентификатор инстанса.</param>
    /// <param name="model">Входная модель для добавления пересылки.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <response code="400">Невалидный запрос</response>
    /// <response code="500">Возникла ошибка на сервере</response>
    [HttpPost]
    public async Task AddForwardEntry(Guid instanceId, AddForwardEntryInputModel model,
        CancellationToken cancellationToken)
    {
        // Маппинг моделей ввода в модели команд
        var command = mapper.Map<AddForwardEntryCommand>(model, options => options.Items.Add("InstanceId", instanceId));
        
        // Отправляем запрос медиатору на добавление новой пересылки
        await mediator.Send(command, cancellationToken);
    }
    
    /// <summary>
    /// Удаление новой пересылки сообщений.
    /// </summary>
    /// <param name="instanceId">Идентификатор инстанса.</param>
    /// <param name="model">Входная модель для удаления пересылки.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <response code="400">Невалидный запрос</response>
    /// <response code="500">Возникла ошибка на сервере</response>
    [HttpPost]
    public async Task RemoveForwardEntry(Guid instanceId, RemoveForwardEntryInputModel model,
        CancellationToken cancellationToken)
    {
        // Маппинг моделей ввода в модели команд
        var command = mapper.Map<RemoveForwardEntryCommand>(model, options => options.Items.Add("InstanceId", instanceId));
        
        // Отправляем запрос медиатору на добавление новой пересылки
        await mediator.Send(command, cancellationToken);
    }
}