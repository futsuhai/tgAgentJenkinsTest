using TgInstanceAgent.Domain.Specifications.Abstractions;
using TgInstanceAgent.Domain.SystemProxy.Enums;
using TgInstanceAgent.Domain.SystemProxy.Specifications.Visitor;

namespace TgInstanceAgent.Domain.SystemProxy.Specifications;

/// <inheritdoc/>
/// <summary>
/// Спецификация для версии системных прокси.
/// </summary>
public class SystemProxyVersionSpecification(IpVersion ipVersion) : ISpecification<SystemProxyAggregate, ISystemProxySpecificationVisitor>
{
    /// <summary>
    /// Версия системного прокси.
    /// </summary>
    public IpVersion IpVersion { get; } = ipVersion;

    /// <inheritdoc/>
    /// <summary>
    /// Проверяет, удовлетворяет ли элемент условию версии прокси.
    /// </summary>
    public bool IsSatisfiedBy(SystemProxyAggregate item) => item.IpVersion == IpVersion;

    /// <inheritdoc/>
    /// <summary>
    /// Принимает посетителя спецификации системного прокси.
    /// </summary>
    public void Accept(ISystemProxySpecificationVisitor visitor) => visitor.Visit(this);
}