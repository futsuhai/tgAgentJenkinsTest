using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TgInstanceAgent.Application.Abstractions.DTOs.Reports;
using TgInstanceAgent.Application.Abstractions.Queries.Reports;
using TgInstanceAgent.Infrastructure.Web.Reports.InputModels;

namespace TgInstanceAgent.Infrastructure.Web.Reports.Controllers;

/// <summary>
/// Контроллер отвечающий за работу с отчётами
/// </summary>
/// <param name="mediator">Медиатор</param>
[Authorize]
[ApiController]
[Route("apiTg/[controller]/[action]/{instanceId:guid}")]
public class ReportsController(ISender mediator, IMapper mapper) : ControllerBase
{
    /// <summary>
    /// Получить отчёт по дате
    /// </summary>
    /// <param name="instanceId">Идентификатор инстанса</param>
    /// <param name="model">Модель входных данных для получения отчёта по дате</param>
    /// <param name="cancellationToken">Токен отмены</param>
    [HttpGet]
    public async Task<ReportDto> GetReportByDate(Guid instanceId, 
        [FromQuery] GetReportByDateInputModel model, CancellationToken cancellationToken)
    {
        // Создаем команду
        var command = mapper.Map<GetReportByDateQuery>(model, options => options.Items.Add("InstanceId", instanceId));

        // Отправляем запрос медиатору на получение отчёта по дате и возвращаем его
        return await mediator.Send(command, cancellationToken);
    }
    
    /// <summary>
    /// Получить отчёт по диапазону дат
    /// </summary>
    /// <param name="instanceId">Идентификатор инстанса</param>
    /// <param name="model">Модель входных данных для получения отчёта по диапазону дат</param>
    /// <param name="cancellationToken">Токен отмены</param>
    [HttpGet]
    public async Task<ReportDto[]> GetReportByDateRange(Guid instanceId, 
        [FromQuery] GetReportsByDatesRangeInputModel model, CancellationToken cancellationToken)
    {
        // Создаем команду
        var command = mapper.Map<GetReportsByDatesRangeQuery>(model, options => options.Items.Add("InstanceId", instanceId));

        // Отправляем запрос медиатору на получение отчётов по диапазону дат и возвращаем его
        return await mediator.Send(command, cancellationToken);
    }
}