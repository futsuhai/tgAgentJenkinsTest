using TgInstanceAgent.Domain.Ordering.Abstractions;
using TgInstanceAgent.Domain.SystemProxy;
using TgInstanceAgent.Domain.SystemProxy.Ordering;
using TgInstanceAgent.Domain.SystemProxy.Ordering.Visitor;
using TgInstanceAgent.Infrastructure.Storage.Entities.SystemProxies;
using TgInstanceAgent.Infrastructure.Storage.Visitors.Sorting.Models;

namespace TgInstanceAgent.Infrastructure.Storage.Visitors.Sorting;

/// <inheritdoc cref="ISystemProxySortingVisitor" />
/// <summary>
/// Реализация посетителя сортировки системных прокси.
/// </summary>
public class SystemProxySortingVisitor : BaseSortingVisitor<SystemProxyModel, ISystemProxySortingVisitor, SystemProxyAggregate>,
    ISystemProxySortingVisitor
{
    /// <inheritdoc/>
    /// <summary>
    /// Конвертирует сортировку в список параметров для сортировки
    /// </summary>
    protected override List<SortData<SystemProxyModel>> ConvertOrderToList(IOrderBy<SystemProxyAggregate, ISystemProxySortingVisitor> order)
    {
        // Создание инстанса посетителя
        var visitor = new SystemProxySortingVisitor();
        
        // Применение посетителя к сортировке для выполнения необходимых действий
        order.Accept(visitor);
        
        // Возвращение результирующего списка SortItems после применения посетителя
        return visitor.SortItems;
    }
    
    /// <inheritdoc/>
    /// <summary>
    /// Посещает сортировку инстансов в случайном порядке.
    /// </summary>
    public void Visit(SystemProxyOrderByRandom order)
    {
        // Дополнительные действия не требуются, потому что сортировка в случайном порядке уже обрабатывается в методе Order класса SystemProxyOrderByRandom.
    }
    
    /// <inheritdoc/>
    /// <summary>
    /// Посещает сортировку инстансов в случайном порядке.
    /// </summary>
    public void Visit(SystemProxyOrderByInstancesCount order)
    {
        // Добавление в список SortItems сортировки по количеству инстансов в убывающем порядке для сущности SystemProxyModel
        SortItems.Add(new SortData<SystemProxyModel>(x => x.Instances.Count, false));
    }
}