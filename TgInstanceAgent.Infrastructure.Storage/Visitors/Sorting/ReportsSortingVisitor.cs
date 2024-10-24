using TgInstanceAgent.Domain.Ordering.Abstractions;
using TgInstanceAgent.Domain.Reports;
using TgInstanceAgent.Domain.Reports.Ordering;
using TgInstanceAgent.Domain.Reports.Ordering.Visitor;
using TgInstanceAgent.Infrastructure.Storage.Entities.Reports;
using TgInstanceAgent.Infrastructure.Storage.Visitors.Sorting.Models;

namespace TgInstanceAgent.Infrastructure.Storage.Visitors.Sorting;

/// <summary>
/// Реализация посетителя сортировки отчётов.
/// </summary>
public class ReportsSortingVisitor : BaseSortingVisitor<ReportModel, IReportSortingVisitor, ReportAggregate>,
    IReportSortingVisitor
{
    /// <summary>
    /// Посещает сортировку инстансов по дате истечения срока действия.
    /// </summary>
    public void Visit(ReportsOrderByDate order)
    {
        // Добавление в список SortItems сортировки по полю Date в убывающем порядке для сущности ReportModel
        SortItems.Add(new SortData<ReportModel>(x => x.Date, false));
    }

    /// <summary>
    /// Конвертирует сортировку в список параметров для сортировки
    /// </summary>
    protected override List<SortData<ReportModel>> ConvertOrderToList(
        IOrderBy<ReportAggregate, IReportSortingVisitor> order)
    {
        // Создание инстанса посетителя
        var visitor = new ReportsSortingVisitor();
        
        // Применение посетителя к сортировке для выполнения необходимых действий
        order.Accept(visitor);
        
        // Возвращение результирующего списка SortItems после применения посетителя
        return visitor.SortItems;
    }
}