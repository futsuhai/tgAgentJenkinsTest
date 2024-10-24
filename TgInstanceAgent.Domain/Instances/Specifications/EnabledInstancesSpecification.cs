using TgInstanceAgent.Domain.Instances.Specifications.Visitor;
using TgInstanceAgent.Domain.Specifications.Abstractions;

namespace TgInstanceAgent.Domain.Instances.Specifications;

/// <inheritdoc/>
/// <summary>
/// Спецификация для включенных инстансов.
/// </summary>
public class EnabledInstancesSpecification : ISpecification<InstanceAggregate, IInstanceSpecificationVisitor>
{
    /// <inheritdoc/>
    /// <summary>
    /// Проверяет, удовлетворяет ли элемент условию.
    /// </summary>
    public bool IsSatisfiedBy(InstanceAggregate item) => item.Enabled;

    /// <inheritdoc/>
    /// <summary>
    /// Принимает посетителя спецификации инстанса.
    /// </summary>
    public void Accept(IInstanceSpecificationVisitor visitor) => visitor.Visit(this);
}