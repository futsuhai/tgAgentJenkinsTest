using TgInstanceAgent.Domain.Reports.Specifications.Visitor;
using TgInstanceAgent.Domain.Specifications.Abstractions;

namespace TgInstanceAgent.Domain.Reports.Specifications;

/// <inheritdoc/>
/// <summary>
/// Спецификация для отчётов в диапазоне дат.
/// </summary>
public class DatesRangeReportSpecification(Guid instanceId, DateOnly startDate, DateOnly endDate)
    : ISpecification<ReportAggregate, IReportSpecificationVisitor>
{
    /// <summary>
    /// Идентификатор инстанса.
    /// </summary>
    public Guid InstanceId { get; } = instanceId;

    /// <summary>
    /// Дата начала диапазона
    /// </summary>
    public DateOnly StartDate { get; } = startDate;

    /// <summary>
    /// Дата конца диапазона
    /// </summary>
    public DateOnly EndDate { get; } = endDate;
    
    /// <inheritdoc/>
    /// <summary>
    /// Проверяет, удовлетворяет ли элемент условию.
    /// </summary>
    public bool IsSatisfiedBy(ReportAggregate item) => ((item.Date <= EndDate && item.Date >= StartDate) && item.InstanceId == InstanceId);
    
    /// <inheritdoc/>
    /// <summary>
    /// Принимает посетителя спецификации отчёта.
    /// </summary>
    public void Accept(IReportSpecificationVisitor visitor) => visitor.Visit(this);
}