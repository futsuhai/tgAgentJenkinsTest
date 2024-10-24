using TgInstanceAgent.Domain.Ordering.Abstractions;
using TgInstanceAgent.Domain.SystemProxy.Ordering.Visitor;

namespace TgInstanceAgent.Domain.SystemProxy.Ordering;

/// <inheritdoc/>
/// <summary>
/// Класс для сортировки системных прокси по количеству инстансов.
/// </summary>
public class SystemProxyOrderByInstancesCount : IOrderBy<SystemProxyAggregate, ISystemProxySortingVisitor>
{
    /// <inheritdoc/>
    /// <summary>
    /// Сортирует коллекцию системных прокси по количеству инстансов.
    /// </summary>
    public IEnumerable<SystemProxyAggregate> Order(IEnumerable<SystemProxyAggregate> items) =>
        items.OrderBy(x => x.InstancesCount);

    /// <inheritdoc/>
    /// <summary>
    /// Разделяет коллекцию инстансов на группы по количеству инстансов и возвращает список групп.
    /// </summary>
    public IList<IEnumerable<SystemProxyAggregate>> Divide(IEnumerable<SystemProxyAggregate> items) =>
        Order(items).GroupBy(x => x.InstancesCount).Select(x => x.AsEnumerable()).ToList();

    /// <inheritdoc/>
    /// <summary>
    /// Принимает посетителя сортировки системных прокси.
    /// </summary>
    public void Accept(ISystemProxySortingVisitor visitor) => visitor.Visit(this);
}