using TgInstanceAgent.Domain.Specifications.Abstractions;

namespace TgInstanceAgent.Domain.SystemProxy.Specifications.Visitor;

/// <summary>
/// Интерфейс для посетителя спецификации системных прокси.
/// </summary>
public interface ISystemProxySpecificationVisitor : ISpecificationVisitor<ISystemProxySpecificationVisitor, SystemProxyAggregate>
{
    /// <summary>
    /// Посещает спецификацию истекших системных прокси.
    /// </summary>
    void Visit(ExpiredSystemProxiesSpecification specification);
    
    /// <summary>
    /// Посещает спецификацию системных прокси с определённым типом прокси.
    /// </summary>
    void Visit(SystemProxyTypeSpecification specification);
    
    /// <summary>
    /// Посещает спецификацию системных прокси с определённой версией прокси.
    /// </summary>
    void Visit(SystemProxyVersionSpecification specification);
    
    /// <summary>
    /// Посещает спецификацию системных прокси с временем жизни более 24 часа.
    /// </summary>
    void Visit(SystemProxyAvailableSpecification specification);
    
    /// <summary>
    /// Посещает спецификацию системных прокси с временем жизни менее 24 часа.
    /// </summary>
    void Visit(ExpiringSystemProxySpecification specification);
}