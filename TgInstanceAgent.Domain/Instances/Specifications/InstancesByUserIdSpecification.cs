using TgInstanceAgent.Domain.Instances.Specifications.Visitor;
using TgInstanceAgent.Domain.Specifications.Abstractions;

namespace TgInstanceAgent.Domain.Instances.Specifications;

/// <inheritdoc/>
/// <summary>
/// Спецификация для инстансов по идентификатору пользователя.
/// </summary>
public class InstancesByUserIdSpecification(Guid userId) : ISpecification<InstanceAggregate, IInstanceSpecificationVisitor>
{
    /// <summary>
    /// Идентификатор пользователя.
    /// При отправке запроса по идентификатору, необходимо быть уверенным, что приложению известен данный пользователь.
    /// </summary>
    public Guid UserId { get; } = userId;

    /// <inheritdoc/>
    /// <summary>
    /// Проверяет, удовлетворяет ли элемент условию по идентификатору пользователя.
    /// </summary>
    public bool IsSatisfiedBy(InstanceAggregate item) => item.UserId == UserId;

    /// <inheritdoc/>
    /// <summary>
    /// Принимает посетителя спецификации инстанса.
    /// </summary>
    public void Accept(IInstanceSpecificationVisitor visitor) => visitor.Visit(this);
}