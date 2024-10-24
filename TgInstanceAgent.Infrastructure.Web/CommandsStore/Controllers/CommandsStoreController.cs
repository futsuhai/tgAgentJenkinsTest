using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using TgInstanceAgent.Application.Abstractions.DTOs.CommandsStore;
using TgInstanceAgent.Application.Abstractions.Queries.CommandsStore;
using TgInstanceAgent.Infrastructure.Web.CommandsStore.InputModels;

namespace TgInstanceAgent.Infrastructure.Web.CommandsStore.Controllers;

/// <summary>
/// Контроллер отвечающий за работу с логгированием команд инстанса
/// </summary>
/// <param name="mediator">Медиатор</param>
/// <param name="mapper">Медиатор</param>
[Route("apiTg/[controller]/[action]/{instanceId:guid}")]
public class CommandsStoreController(ISender mediator, IMapper mapper) : ControllerBase
{
    /// <summary>
    /// Получение истории выполнения команд инстанса.
    /// </summary>
    /// <param name="instanceId">Идентификатор инстанса.</param>
    /// <param name="model">Входная модель для получения истории выполнения команд инстанса.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <response code="400">Невалидный запрос</response>
    /// <response code="500">Возникла ошибка на сервере</response>
    [HttpGet]
    public async Task<CommandsHistoryDto> GetCommandsHistory(Guid instanceId, [FromQuery]GetCommandsHistoryInputModel model,
        CancellationToken cancellationToken)
    {
        // Маппинг моделей ввода в модели команд
        var command = mapper.Map<GetCommandsHistoryQuery>(model, options => options.Items.Add("InstanceId", instanceId));
        
        // Отправляем запрос медиатору на получение истории выполнения команд инстанса
        return await mediator.Send(command, cancellationToken);
    }
}