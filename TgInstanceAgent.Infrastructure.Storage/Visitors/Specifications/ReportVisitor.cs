using System.Linq.Expressions;
using TgInstanceAgent.Domain.Reports;
using TgInstanceAgent.Domain.Reports.Specifications;
using TgInstanceAgent.Domain.Reports.Specifications.Visitor;
using TgInstanceAgent.Domain.Specifications.Abstractions;
using TgInstanceAgent.Infrastructure.Storage.Entities.Reports;

namespace TgInstanceAgent.Infrastructure.Storage.Visitors.Specifications;

/// <summary>
/// Реализация посетителя спецификации инстанса.
/// </summary>
public class ReportVisitor : BaseVisitor<ReportModel, IReportSpecificationVisitor, ReportAggregate>,
    IReportSpecificationVisitor
{
    /// <inheritdoc/>
    /// <summary>
    /// Преобразует спецификацию в выражение.
    /// </summary>
    protected override Expression<Func<ReportModel, bool>> ConvertSpecToExpression(
        ISpecification<ReportAggregate, IReportSpecificationVisitor> spec)
    {
        // Создаем нового посетителя
        var visitor = new ReportVisitor();
        
        // Посещаем спецификацию и получаем выражение
        spec.Accept(visitor);
        
        // Возвращаем выражение
        return visitor.Expr!;
    }

    /// <inheritdoc/>
    /// <summary>
    /// Посещает спецификацию отчёта инстанса за день.
    /// </summary>
    public void Visit(DateReportSpecification specification) =>
        Expr = x => (x.Date == specification.Date && x.InstanceId == specification.InstanceId);

    /// <inheritdoc/>
    /// <summary>
    /// Посещает спецификацию отчёта инстанса в диапазоне дат.
    /// </summary>
    public void Visit(DatesRangeReportSpecification specification) =>
        Expr = x => ((x.Date <= specification.EndDate && x.Date >= specification.StartDate) && (x.InstanceId == specification.InstanceId));

}