using TgInstanceAgent.Domain.Instances.Specifications.Visitor;
using TgInstanceAgent.Domain.Specifications.Abstractions;

namespace TgInstanceAgent.Domain.Instances.Specifications;

/// <inheritdoc/>
/// <summary>
/// Спецификация для количества инстансов с неустановленным прокси.
/// </summary>
public class InstancesWithOwnProxySpecification: ISpecification<InstanceAggregate, IInstanceSpecificationVisitor>
{
    /// <inheritdoc/>
    /// <summary>
    /// Проверяет, удовлетворяет ли элемент условию не установленного прокси.
    /// </summary>
    public bool IsSatisfiedBy(InstanceAggregate item) => item.Proxy != null;

    /// <inheritdoc/>
    /// <summary>
    /// Принимает посетителя спецификации инстанса.
    /// </summary>
    public void Accept(IInstanceSpecificationVisitor visitor) => visitor.Visit(this);
}