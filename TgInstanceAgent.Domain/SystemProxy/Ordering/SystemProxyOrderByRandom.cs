using TgInstanceAgent.Domain.Ordering.Abstractions;
using TgInstanceAgent.Domain.SystemProxy.Ordering.Visitor;

namespace TgInstanceAgent.Domain.SystemProxy.Ordering;

/// <inheritdoc/>
/// <summary>
/// Класс для сортировки системных прокси в случайном прорядке.
/// </summary>
public class SystemProxyOrderByRandom : IOrderBy<SystemProxyAggregate, ISystemProxySortingVisitor>
{
    /// <inheritdoc/>
    /// <summary>
    /// Сортирует коллекцию системных прокси в случайном прорядке.
    /// </summary>
    public IEnumerable<SystemProxyAggregate> Order(IEnumerable<SystemProxyAggregate> items)
    {
        var random = new Random();
        return items.OrderBy(x => random.Next());
    }

    /// <inheritdoc/>
    /// <summary>
    /// Разделяет коллекцию инстансов на группы в случайном порядке и возвращает список групп.
    /// </summary>
    public IList<IEnumerable<SystemProxyAggregate>> Divide(IEnumerable<SystemProxyAggregate> items)
    {
        throw new NotImplementedException("Разделение не поддерживается для сортировки в случайном порядке");
    }

    /// <inheritdoc/>
    /// <summary>
    /// Принимает посетителя сортировки системных прокси.
    /// </summary>
    public void Accept(ISystemProxySortingVisitor visitor) => visitor.Visit(this);
}