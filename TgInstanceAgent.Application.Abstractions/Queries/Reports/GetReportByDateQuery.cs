using MediatR;
using TgInstanceAgent.Application.Abstractions.DTOs.Reports;
using TgInstanceAgent.Application.Abstractions.Interfaces;

namespace TgInstanceAgent.Application.Abstractions.Queries.Reports;

/// <summary>
/// Запрос на получение отчёта по дате
/// </summary>
public class GetReportByDateQuery : IWithInstanceId, IRequest<ReportDto>
{
    /// <summary>
    /// Уникальный идентификатор инстанса.
    /// </summary>
    public required Guid InstanceId { get; init; }
    
    /// <summary>
    /// Дата.
    /// </summary>
    public required DateOnly Date { get; init; }
}