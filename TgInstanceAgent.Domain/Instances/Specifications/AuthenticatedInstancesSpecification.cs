using TgInstanceAgent.Domain.Instances.Enums;
using TgInstanceAgent.Domain.Instances.Specifications.Visitor;
using TgInstanceAgent.Domain.Specifications.Abstractions;

namespace TgInstanceAgent.Domain.Instances.Specifications;

/// <inheritdoc/>
/// <summary>
/// Спецификация для аутентифицированных инстансов.
/// </summary>
public class AuthenticatedInstancesSpecification : ISpecification<InstanceAggregate, IInstanceSpecificationVisitor>
{
    /// <inheritdoc/>
    /// <summary>
    /// Проверяет, удовлетворяет ли элемент условию.
    /// </summary>
    public bool IsSatisfiedBy(InstanceAggregate item) => item.State == State.Authenticated;

    /// <inheritdoc/>
    /// <summary>
    /// Принимает посетителя спецификации инстанса.
    /// </summary>
    public void Accept(IInstanceSpecificationVisitor visitor) => visitor.Visit(this);
}