using MediatR;
using Microsoft.Extensions.Caching.Memory;
using TgInstanceAgent.Application.Abstractions.DTOs.Reports;
using TgInstanceAgent.Application.Abstractions.Queries.Reports;
using TgInstanceAgent.Domain.Abstractions.Repositories;
using TgInstanceAgent.Domain.Reports.Ordering;
using TgInstanceAgent.Domain.Reports.Specifications;

namespace TgInstanceAgent.Application.Services.QueriesHandlers.Reports;

/// <summary>
/// Обработчик запроса на получение отчётов в диапазоне дат.
/// </summary>
/// <param name="repository">Репозиторий для работы с отчётами.</param>
/// <param name="cache">Кэш.</param> 
public class GetReportsByDatesRangeQueryHandler(IReportRepository repository, IMemoryCache cache)
    : IRequestHandler<GetReportsByDatesRangeQuery, ReportDto[]>
{
    /// <summary>
    /// Обрабатывает запрос на получение отчётов в диапазоне дат.
    /// </summary>
    /// <param name="request">Запрос на получение отчётов в диапазоне дат.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Данные инстанса.</returns>
    public async Task<ReportDto[]> Handle(GetReportsByDatesRangeQuery request, CancellationToken cancellationToken)
    {
        // Спецификация отчетов за текущий день.
        var datesRangeReportSpecification =
            new DatesRangeReportSpecification(request.InstanceId, request.StartDate, request.EndDate);
        
        // Спецификация для сортировки отчётов по дате
        var reportsSortingSpecification = new ReportsOrderByDate();
        
        // Асинхронно выполняем поиск отчётов.
        var reportsByDatesRange = await repository.FindAsync(datesRangeReportSpecification, reportsSortingSpecification);
            
        // Преобразуем коллекцию ReportAggregate в массив ReportDto
        var reports = reportsByDatesRange.Select(report => new ReportDto
        {
            // Идентификатор отчёта
            Id = report.Id,
            
            // Количество отправленных сообщений
            Sent = report.Sent,
            
            // Количество полученных сообщений
            Received = report.Received,
            
            // Дата отчёта
            Date = report.Date,
            
            // Идентификатор инстанса.
            InstanceId = report.InstanceId
        }).ToArray();
        
        // Возвращаем отчёты
        return reports;
    }
}