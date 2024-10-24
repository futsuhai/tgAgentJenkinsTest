using MediatR;
using TgInstanceAgent.Application.Abstractions.DTOs.Reports;
using TgInstanceAgent.Application.Abstractions.Interfaces;

namespace TgInstanceAgent.Application.Abstractions.Queries.Reports;

/// <summary>
/// Запрос на получение отчёта в диапазоне дат
/// </summary>
public class GetReportsByDatesRangeQuery : IWithInstanceId, IRequest<ReportDto[]>
{
    /// <summary>
    /// Уникальный идентификатор инстанса.
    /// </summary>
    public required Guid InstanceId { get; init; }
    
    /// <summary>
    /// Дата начала диапазона
    /// </summary>
    public required DateOnly StartDate { get; init; }
    
    /// <summary>
    /// Дата конца диапазона
    /// </summary>
    public required DateOnly EndDate { get; init; }
}