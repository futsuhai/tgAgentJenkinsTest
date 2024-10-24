using TgInstanceAgent.Domain.Specifications.Abstractions;
using TgInstanceAgent.Domain.SystemProxy.Enums;
using TgInstanceAgent.Domain.SystemProxy.Specifications.Visitor;

namespace TgInstanceAgent.Domain.SystemProxy.Specifications;

/// <inheritdoc/>
/// <summary>
/// Спецификация для типа системных прокси.
/// </summary>
public class SystemProxyTypeSpecification(ProxyType proxyType) : ISpecification<SystemProxyAggregate, ISystemProxySpecificationVisitor>
{
    /// <summary>
    /// Тип системного прокси.
    /// </summary>
    public ProxyType Type { get; } = proxyType;

    /// <inheritdoc/>
    /// <summary>
    /// Проверяет, удовлетворяет ли элемент условию типа прокси.
    /// </summary>
    public bool IsSatisfiedBy(SystemProxyAggregate item) => item.Type == Type;

    /// <inheritdoc/>
    /// <summary>
    /// Принимает посетителя спецификации системного прокси.
    /// </summary>
    public void Accept(ISystemProxySpecificationVisitor visitor) => visitor.Visit(this);
}