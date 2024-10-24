using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TgInstanceAgent.Application.Abstractions.Commands.TgAuth;
using TgInstanceAgent.Infrastructure.Web.Authentication.InputModels;
using TgInstanceAgent.Infrastructure.Web.Filters;

namespace TgInstanceAgent.Infrastructure.Web.Authentication.Controllers;

/// <summary>
/// Контроллер отвечающий за работу с чатами
/// </summary>
/// <param name="mediator">Медиатор</param>
[Authorize]
[InstanceFilter]
[ApiController]
[Route("apiTg/[controller]/[action]/{instanceId:guid}")]
public class AuthenticationController(ISender mediator, IMapper mapper) : ControllerBase
{
    /// <summary>
    /// Установить номер телефона
    /// </summary>
    /// <param name="instanceId">Идентификатор инстанса</param>
    /// <param name="model">Номер телефона</param>
    /// <param name="cancellationToken">Токен для отмены операции</param> 
    /// <response code="400">Некорректные входные данные или невалидный запрос</response>
    /// <response code="500">Возникла ошибка на сервере</response>
    [HttpPost]
    public async Task SetPhone(Guid instanceId, SetPhoneNumberInputModel model, CancellationToken cancellationToken)
    {
        // Создаем команду
        var command = mapper.Map<SetPhoneNumberCommand>(model, options => options.Items.Add("InstanceId", instanceId));
        
        // Отправляем запрос медиатору на установку номера телефона
        await mediator.Send(command, cancellationToken);
    }

    /// <summary>
    /// Отправить проверочный код
    /// </summary>
    /// <param name="instanceId">Идентификатор инстанса</param>
    /// <param name="model">Проверочный код</param>
    /// <param name="cancellationToken">Токен для отмены операции</param> 
    /// <response code="400">Некорректные входные данные или невалидный запрос</response>
    /// <response code="500">Возникла ошибка на сервере</response>
    [HttpPost]
    public async Task CheckCode(Guid instanceId, CheckCodeInputModel model, CancellationToken cancellationToken)
    {
        // Создаем команду
        var command = mapper.Map<SetCodeCommand>(model, options => options.Items.Add("InstanceId", instanceId));
        
        // Отправляем запрос медиатору на установку проверочного кода
        await mediator.Send(command, cancellationToken);
    }

    /// <summary>
    /// Отправить пароль
    /// </summary>
    /// <param name="instanceId">Идентификатор инстанса</param>
    /// <param name="model">Пароль</param>
    /// <param name="cancellationToken">Токен для отмены операции</param> 
    /// <response code="400">Некорректные входные данные или невалидный запрос</response>
    /// <response code="500">Возникла ошибка на сервере</response>
    [HttpPost]
    public async Task CheckPassword(Guid instanceId, CheckPasswordInputModel model, CancellationToken cancellationToken)
    {
        // Создаем команду
        var command = mapper.Map<SetPasswordCommand>(model, options => options.Items.Add("InstanceId", instanceId));
        
        // Отправляем запрос медиатору на установку пароля
        await mediator.Send(command, cancellationToken);
    }
}