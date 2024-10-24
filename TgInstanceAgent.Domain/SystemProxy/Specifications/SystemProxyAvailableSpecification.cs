using TgInstanceAgent.Domain.Specifications.Abstractions;
using TgInstanceAgent.Domain.SystemProxy.Specifications.Visitor;

namespace TgInstanceAgent.Domain.SystemProxy.Specifications;

/// <inheritdoc/>
/// <summary>
/// Спецификация для системных прокси с временем жизни более 24 часа.
/// </summary>
public class SystemProxyAvailableSpecification : ISpecification<SystemProxyAggregate, ISystemProxySpecificationVisitor>
{
    /// <inheritdoc/>
    /// <summary>
    /// Проверяет, удовлетворяет ли элемент условию времени жизни прокси.
    /// </summary>
    public bool IsSatisfiedBy(SystemProxyAggregate item) => item.ExpirationTimeUtc > DateTime.UtcNow.AddHours(24);

    /// <inheritdoc/>
    /// <summary>
    /// Принимает посетителя спецификации системного прокси.
    /// </summary>
    public void Accept(ISystemProxySpecificationVisitor visitor) => visitor.Visit(this);
}