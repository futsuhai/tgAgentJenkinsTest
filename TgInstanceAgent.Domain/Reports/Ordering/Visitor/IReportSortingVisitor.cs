using TgInstanceAgent.Domain.Ordering.Abstractions;

namespace TgInstanceAgent.Domain.Reports.Ordering.Visitor;

/// <summary>
/// Интерфейс для посетителя сортировки инстансов.
/// </summary>
public interface IReportSortingVisitor : ISortingVisitor<IReportSortingVisitor, ReportAggregate>
{
    /// <summary>
    /// Посещает сортировку отчётов по дате.
    /// </summary>
    void Visit(ReportsOrderByDate order);
}