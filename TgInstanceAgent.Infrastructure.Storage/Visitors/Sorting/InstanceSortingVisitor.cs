using TgInstanceAgent.Domain.Instances;
using TgInstanceAgent.Domain.Instances.Ordering;
using TgInstanceAgent.Domain.Instances.Ordering.Visitor;
using TgInstanceAgent.Domain.Ordering.Abstractions;
using TgInstanceAgent.Infrastructure.Storage.Entities.Instances;
using TgInstanceAgent.Infrastructure.Storage.Visitors.Sorting.Models;

namespace TgInstanceAgent.Infrastructure.Storage.Visitors.Sorting;

/// <inheritdoc cref="TgInstanceAgent.Domain.Instances.Ordering.Visitor.IInstanceSortingVisitor" />
/// <summary>
/// Реализация посетителя сортировки инстансов.
/// </summary>
public class InstanceSortingVisitor : BaseSortingVisitor<InstanceModel, IInstanceSortingVisitor, InstanceAggregate>,
    IInstanceSortingVisitor
{
    /// <inheritdoc/>
    /// <summary>
    /// Конвертирует сортировку в список параметров для сортировки
    /// </summary>
    protected override List<SortData<InstanceModel>> ConvertOrderToList(
        IOrderBy<InstanceAggregate, IInstanceSortingVisitor> order)
    {
        // Создание инстанса посетителя
        var visitor = new InstanceSortingVisitor();
        
        // Применение посетителя к сортировке для выполнения необходимых действий
        order.Accept(visitor);
        
        // Возвращение результирующего списка SortItems после применения посетителя
        return visitor.SortItems;
    }

    /// <inheritdoc/>
    /// <summary>
    /// Посещает сортировку инстансов по дате истечения срока действия.
    /// </summary>
    public void Visit(InstanceOrderByExpirationDate order)
    {
        // Добавление в список SortItems сортировки по полю ExpirationTimeUtc в убывающем порядке для сущности InstanceModel
        SortItems.Add(new SortData<InstanceModel>(x => x.ExpirationTimeUtc, false));
    }
}