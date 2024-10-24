using TgInstanceAgent.Domain.Ordering.Abstractions;

namespace TgInstanceAgent.Domain.SystemProxy.Ordering.Visitor;

/// <summary>
/// Интерфейс для посетителя сортировки системных прокси.
/// </summary>
public interface ISystemProxySortingVisitor : ISortingVisitor<ISystemProxySortingVisitor, SystemProxyAggregate>
{
    /// <summary>
    /// Посещает сортировку системных прокси в случайном порядке.
    /// </summary>
    void Visit(SystemProxyOrderByRandom order);
    
    /// <summary>
    /// Посещает сортировку системных прокси по колличеству инстансов.
    /// </summary>
    void Visit(SystemProxyOrderByInstancesCount order);
}