using TgInstanceAgent.Domain.Instances.Specifications.Visitor;
using TgInstanceAgent.Domain.Specifications.Abstractions;

namespace TgInstanceAgent.Domain.Instances.Specifications;

/// <inheritdoc/>
/// <summary>
/// Спецификация для истекших инстансов.
/// </summary>
public class ExpiredInstancesSpecification : ISpecification<InstanceAggregate, IInstanceSpecificationVisitor>
{
    /// <inheritdoc/>
    /// <summary>
    /// Проверяет, удовлетворяет ли элемент условию.
    /// </summary>
    public bool IsSatisfiedBy(InstanceAggregate item) => item.ExpirationTimeUtc > DateTime.UtcNow;

    /// <inheritdoc/>
    /// <summary>
    /// Принимает посетителя спецификации инстанса.
    /// </summary>
    public void Accept(IInstanceSpecificationVisitor visitor) => visitor.Visit(this);
}