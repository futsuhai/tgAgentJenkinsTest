using TgInstanceAgent.Domain.Specifications.Abstractions;

namespace TgInstanceAgent.Domain.Reports.Specifications.Visitor;

/// <summary>
/// Интерфейс для посетителя спецификации отчета.
/// </summary>
public interface IReportSpecificationVisitor : ISpecificationVisitor<IReportSpecificationVisitor, ReportAggregate>
{
    /// <summary>
    /// Посещает спецификацию отчёта по идентификатору инстанса и текущей дате.
    /// </summary>
    void Visit(DateReportSpecification specification);
    
    /// <summary>
    /// Посещает спецификацию отчёта по идентификатору инстанса и диапазону дат.
    /// </summary>
    void Visit(DatesRangeReportSpecification specification);
}