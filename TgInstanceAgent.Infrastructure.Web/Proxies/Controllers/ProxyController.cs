using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using TgInstanceAgent.Application.Abstractions.Commands.TgProxies;
using TgInstanceAgent.Infrastructure.Web.Proxies.InputModels;

namespace TgInstanceAgent.Infrastructure.Web.Proxies.Controllers;

/// <summary>
/// Контроллер отвечающий за работу с прокси
/// </summary>
/// <param name="mediator">Медиатор</param>
[ApiController]
[Route("apiTg/[controller]/[action]/{instanceId:guid}")]
public class ProxyController(ISender mediator, IMapper mapper) : ControllerBase
{
    /// <summary>
    /// Устанавливает прокси
    /// </summary>
    /// <param name="instanceId">Идентификатор инстанса</param>
    /// <param name="model">Модель для установки прокси</param>
    /// <param name="cancellationToken">Токен для отмены операции</param>
    [HttpPost]
    public async Task SetProxy(Guid instanceId, SetProxyInputModel model, CancellationToken cancellationToken)
    {
        // Создаем команду
        var command = mapper.Map<SetProxyCommand>(model, options => options.Items.Add("InstanceId", instanceId));
        
        // Отправляем команду медиатору на установку прокси сервера
        await mediator.Send(command, cancellationToken);
    }
}