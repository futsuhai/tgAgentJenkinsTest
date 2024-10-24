using TgInstanceAgent.Domain.Reports.Specifications.Visitor;
using TgInstanceAgent.Domain.Specifications.Abstractions;

namespace TgInstanceAgent.Domain.Reports.Specifications;

/// <inheritdoc/>
/// <summary>
/// Спецификация для отчётов по дате.
/// </summary>
public class DateReportSpecification(Guid instanceId, DateOnly date) : ISpecification<ReportAggregate, IReportSpecificationVisitor>
{
    /// <summary>
    /// Идентификатор инстанса.
    /// </summary>
    public Guid InstanceId { get; } = instanceId;
    
    /// <summary>
    /// Дата
    /// </summary>
    public DateOnly Date { get; } = date;
    
    /// <inheritdoc/>
    /// <summary>
    /// Проверяет, удовлетворяет ли элемент условию.
    /// </summary>
    public bool IsSatisfiedBy(ReportAggregate item) => item.Date == Date && item.InstanceId == InstanceId;

    /// <inheritdoc/>
    /// <summary>
    /// Принимает посетителя спецификации отчёта.
    /// </summary>
    public void Accept(IReportSpecificationVisitor visitor) => visitor.Visit(this);
}