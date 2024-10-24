using TgInstanceAgent.Domain.Ordering.Abstractions;
using TgInstanceAgent.Domain.Reports.Ordering.Visitor;

namespace TgInstanceAgent.Domain.Reports.Ordering;

/// <inheritdoc/>
/// <summary>
/// Класс для сортировки отчётов по дате.
/// </summary>
public class ReportsOrderByDate : IOrderBy<ReportAggregate, IReportSortingVisitor>
{
    /// <inheritdoc/>
    /// <summary>
    /// Сортирует коллекцию отчётов по дате.
    /// </summary>
    public IEnumerable<ReportAggregate> Order(IEnumerable<ReportAggregate> items) => items.OrderBy(x => x.Date);

    /// <inheritdoc/>
    /// <summary>
    /// Разделяет коллекцию отчётов на группы по дате и возвращает список групп.
    /// </summary>
    public IList<IEnumerable<ReportAggregate>> Divide(IEnumerable<ReportAggregate> items) =>
        Order(items).GroupBy(x => x.Date).Select(x => x.AsEnumerable()).ToList();

    /// <inheritdoc/>
    /// <summary>
    /// Принимает посетителя сортировки отчётов.
    /// </summary>
    public void Accept(IReportSortingVisitor visitor) => visitor.Visit(this);
}