using TgInstanceAgent.Domain.Specifications.Abstractions;
using TgInstanceAgent.Domain.SystemProxy.Specifications.Visitor;

namespace TgInstanceAgent.Domain.SystemProxy.Specifications;

/// <inheritdoc/>
/// <summary>
/// Спецификация для истекших системных прокси.
/// </summary>
public class ExpiredSystemProxiesSpecification : ISpecification<SystemProxyAggregate, ISystemProxySpecificationVisitor>
{
    /// <inheritdoc/>
    /// <summary>
    /// Проверяет, удовлетворяет ли элемент условию истечения срока действия.
    /// </summary>
    public bool IsSatisfiedBy(SystemProxyAggregate item) => item.ExpirationTimeUtc > DateTime.UtcNow.AddHours(-24);

    /// <inheritdoc/>
    /// <summary>
    /// Принимает посетителя спецификации системного прокси.
    /// </summary>
    public void Accept(ISystemProxySpecificationVisitor visitor) => visitor.Visit(this);
}