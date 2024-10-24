using TgInstanceAgent.Domain.Instances.Ordering.Visitor;
using TgInstanceAgent.Domain.Ordering.Abstractions;

namespace TgInstanceAgent.Domain.Instances.Ordering;

/// <inheritdoc/>
/// <summary>
/// Класс для сортировки инстансов по дате истечения срока действия.
/// </summary>
public class InstanceOrderByExpirationDate : IOrderBy<InstanceAggregate, IInstanceSortingVisitor>
{
    /// <inheritdoc/>
    /// <summary>
    /// Сортирует коллекцию инстансов по дате истечения срока действия.
    /// </summary>
    public IEnumerable<InstanceAggregate> Order(IEnumerable<InstanceAggregate> items) => items.OrderBy(x => x.ExpirationTimeUtc);

    /// <inheritdoc/>
    /// <summary>
    /// Разделяет коллекцию инстансов на группы по дате истечения срока действия и возвращает список групп.
    /// </summary>
    public IList<IEnumerable<InstanceAggregate>> Divide(IEnumerable<InstanceAggregate> items) =>
        Order(items).GroupBy(x => x.ExpirationTimeUtc).Select(x => x.AsEnumerable()).ToList();

    /// <inheritdoc/>
    /// <summary>
    /// Принимает посетителя сортировки инстансов.
    /// </summary>
    public void Accept(IInstanceSortingVisitor visitor) => visitor.Visit(this);
}