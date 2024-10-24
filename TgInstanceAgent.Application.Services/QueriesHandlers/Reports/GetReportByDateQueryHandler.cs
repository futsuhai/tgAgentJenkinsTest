using MediatR;
using Microsoft.Extensions.Caching.Memory;
using TgInstanceAgent.Application.Abstractions.DTOs.Reports;
using TgInstanceAgent.Application.Abstractions.Exceptions;
using TgInstanceAgent.Application.Abstractions.Queries.Reports;
using TgInstanceAgent.Domain.Abstractions.Repositories;
using TgInstanceAgent.Domain.Reports.Specifications;

namespace TgInstanceAgent.Application.Services.QueriesHandlers.Reports;

/// <summary>
/// Обработчик запроса на получение отчёта.
/// </summary>
/// <param name="repository">Репозиторий для работы с отчётами.</param>
/// <param name="cache">Кэш.</param> 
public class GetReportByDateQueryHandler(IReportRepository repository, IMemoryCache cache)
    : IRequestHandler<GetReportByDateQuery, ReportDto>
{
    /// <summary>
    /// Обрабатывает запрос на получение отчёта.
    /// </summary>
    /// <param name="request">Запрос на получение отчёта.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Данные инстанса.</returns>
    public async Task<ReportDto> Handle(GetReportByDateQuery request, CancellationToken cancellationToken)
    {
        // Спецификация отчетов за текущий день.
        var dateReportSpecification = new DateReportSpecification(request.InstanceId, request.Date);
        
        // Асинхронно выполняем поиск отчётов.
        var reports = await repository.FindAsync(dateReportSpecification);
        
        // Получаем отчёт за текущую дату.
        var report = reports.FirstOrDefault();

        // Если отчёт не найден, выбрасываем исключение
        if (report == null) throw new ReportNotFoundException();
            
        // Возвращаем отчёт
        return new ReportDto
        {
            // Идентификатор отчёта
            Id = report.Id,
            
            // Идентификатор инстанса.
            InstanceId = report.InstanceId,
            
            // Количество отправленных сообщений
            Sent = report.Sent,
            
            // Количество полученных сообщений
            Received = report.Received,
            
            // Дата отчёта
            Date = report.Date
        };
    }
}