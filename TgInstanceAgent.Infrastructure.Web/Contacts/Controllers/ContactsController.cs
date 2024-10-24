using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TgInstanceAgent.Application.Abstractions.Commands.TgContacts;
using TgInstanceAgent.Application.Abstractions.Queries.TgUsers;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Objects.Components;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Objects.Users;
using TgInstanceAgent.Infrastructure.Web.Contacts.InputModels;
using TgInstanceAgent.Infrastructure.Web.Filters;

namespace TgInstanceAgent.Infrastructure.Web.Contacts.Controllers;

/// <summary>
/// Контроллер отвечающий за работу с контактами
/// </summary>
/// <param name="mediator">Медиатор</param>
[Authorize]
[InstanceFilter]
[ApiController]
[Route("apiTg/[controller]/[action]/{instanceId:guid}")]
public class ContactsController(ISender mediator, IMapper mapper) : ControllerBase
{
    /// <summary>
    /// Получить список контактов
    /// </summary>
    /// <param name="instanceId">Идентификатор инстанса</param>
    /// <param name="model">Данные запроса</param>
    /// <param name="cancellationToken">Токен для отмены операции</param> 
    /// <response code="400">Невалидный запрос</response>
    /// <response code="500">Возникла ошибка на сервере</response>
    [HttpGet]
    public async Task<TgCountResult<TgUser>> GetContacts(Guid instanceId, [FromQuery] GetContactsInputModel model,
        CancellationToken cancellationToken)
    {
        // Маппинг моделей ввода в модели команд
        var query = mapper.Map<GetContactsQuery>(model, options => options.Items.Add("InstanceId", instanceId));

        // Отправляем запрос медиатору на импорт контактов
        return await mediator.Send(query, cancellationToken);
    }

    /// <summary>
    /// Импортировать контакты
    /// </summary>
    /// <param name="instanceId">Идентификатор инстанса</param>
    /// <param name="model">Данные контактов</param>
    /// <param name="cancellationToken">Токен для отмены операции</param> 
    /// <response code="400">Некорректные входные данные или невалидный запрос</response>
    /// <response code="500">Возникла ошибка на сервере</response>
    [HttpPost]
    public async Task ImportContacts(Guid instanceId, ImportContactInputModel[] model, CancellationToken cancellationToken)
    {
        // Маппинг моделей ввода в модели команд
        var command = new ImportContactsCommand
        {
            InstanceId = instanceId,
            Contacts = mapper.Map<ImportContact[]>(model)
        };

        // Отправляем запрос медиатору на импорт контактов
        await mediator.Send(command, cancellationToken);
    }

    /// <summary>
    /// Добавить контакт
    /// </summary>
    /// <param name="instanceId">Идентификатор инстанса</param>
    /// <param name="model">Данные контакта пользователя</param>
    /// <param name="cancellationToken">Токен для отмены операции</param> 
    /// <response code="400">Некорректные входные данные или невалидный запрос</response>
    /// <response code="500">Возникла ошибка на сервере</response>
    [HttpPost]
    public async Task AddContact(Guid instanceId, AddContactInputModel model, CancellationToken cancellationToken)
    {
        // Создаем команду
        var command = mapper.Map<AddContactCommand>(model, options => options.Items.Add("InstanceId", instanceId));

        // Отправляем запрос на добавление контакта
        await mediator.Send(command, cancellationToken);
    }

    /// <summary>
    /// Удалить контакт
    /// </summary>
    /// <param name="instanceId">Идентификатор инстанса</param>
    /// <param name="model">Данные контакта пользователя</param>
    /// <param name="cancellationToken">Токен для отмены операции</param> 
    /// <response code="400">Некорректные входные данные или невалидный запрос</response>
    /// <response code="500">Возникла ошибка на сервере</response>
    [HttpPost]
    public async Task RemoveContact(Guid instanceId, RemoveContactInputModel model, CancellationToken cancellationToken)
    {
        // Создаем команду
        var command = mapper.Map<RemoveContactCommand>(model, options => options.Items.Add("InstanceId", instanceId));

        // Отправляем команду на удаление контакта
        await mediator.Send(command, cancellationToken);
    }
    
    /// <summary>
    /// Поиск контактов
    /// </summary>
    /// <param name="instanceId">Идентификатор инстанса</param>
    /// <param name="model">Данные для поиска контактов</param>
    /// <param name="cancellationToken">Токен для отмены операции</param>
    /// <returns>Коллекция найденных контактов и их колличетво</returns>
    /// <response code="400">Некорректные входные данные или невалидный запрос</response>
    /// <response code="500">Возникла ошибка на сервере</response>
    [HttpGet]
    public async Task<TgCountResult<TgUser>> SearchContacts(Guid instanceId, 
        [FromQuery]SearchContactsInputModel model, CancellationToken cancellationToken)
    {
        // Создаем команду
        var command = mapper.Map<SearchContactsQuery>(model, options => options.Items.Add("InstanceId", instanceId));
        
        // Отправляем команду на поиск контактов
        return await mediator.Send(command, cancellationToken);
    }
}