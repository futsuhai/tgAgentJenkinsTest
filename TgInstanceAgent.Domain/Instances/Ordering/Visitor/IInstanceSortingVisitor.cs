using TgInstanceAgent.Domain.Ordering.Abstractions;

namespace TgInstanceAgent.Domain.Instances.Ordering.Visitor;

/// <summary>
/// Интерфейс для посетителя сортировки инстансов.
/// </summary>
public interface IInstanceSortingVisitor : ISortingVisitor<IInstanceSortingVisitor, InstanceAggregate>
{
    /// <summary>
    /// Посещает сортировку инстансов по дате истечения срока действия.
    /// </summary>
    void Visit(InstanceOrderByExpirationDate order);
}