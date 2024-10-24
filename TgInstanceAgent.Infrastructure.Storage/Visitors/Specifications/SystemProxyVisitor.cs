using System.Linq.Expressions;
using TgInstanceAgent.Domain.Specifications.Abstractions;
using TgInstanceAgent.Domain.SystemProxy;
using TgInstanceAgent.Domain.SystemProxy.Specifications;
using TgInstanceAgent.Domain.SystemProxy.Specifications.Visitor;
using TgInstanceAgent.Infrastructure.Storage.Entities.SystemProxies;

namespace TgInstanceAgent.Infrastructure.Storage.Visitors.Specifications;

///  <inheritdoc cref="ISystemProxySpecificationVisitor" />
/// <summary>
/// Реализация посетителя спецификации системного прокси.
/// </summary>
public class SystemProxyVisitor : BaseVisitor<SystemProxyModel, ISystemProxySpecificationVisitor, SystemProxyAggregate>,
    ISystemProxySpecificationVisitor
{
    /// <inheritdoc/>
    /// <summary>
    /// Преобразует спецификацию в выражение.
    /// </summary>
    protected override Expression<Func<SystemProxyModel, bool>> ConvertSpecToExpression(
        ISpecification<SystemProxyAggregate, ISystemProxySpecificationVisitor> spec)
    {
        // Создаем нового посетителя
        var visitor = new SystemProxyVisitor();

        // Посещаем спецификацию и получаем выражение
        spec.Accept(visitor);

        // Возвращаем выражение
        return visitor.Expr!;
    }

    /// <inheritdoc/>
    /// <summary>
    /// Посещает спецификацию просроченных системных прокси.
    /// </summary>
    public void Visit(ExpiredSystemProxiesSpecification specification) =>
        Expr = x => x.ExpirationTimeUtc < DateTime.UtcNow;

    /// <summary>
    /// Посещает спецификацию системных прокси с определённым типом прокси.
    /// </summary>
    public void Visit(SystemProxyTypeSpecification specification) => Expr = x => x.Type == specification.Type;

    /// <summary>
    /// Посещает спецификацию системных прокси с определённым типом прокси.
    /// </summary>
    public void Visit(SystemProxyVersionSpecification specification) =>
        Expr = x => x.IpVersion == specification.IpVersion;

    /// <summary>
    /// Посещает спецификацию системных прокси с временем жизни более 24 часа.
    /// </summary>
    public void Visit(SystemProxyAvailableSpecification specification) =>
        Expr = x => x.ExpirationTimeUtc > DateTime.UtcNow.AddHours(24);

    /// <summary>
    /// Посещает спецификацию системных прокси с временем жизни менее 24 часа.
    /// </summary>
    public void Visit(ExpiringSystemProxySpecification specification) =>
        Expr = x => x.ExpirationTimeUtc < DateTime.UtcNow.AddHours(24) && x.ExpirationTimeUtc > DateTime.UtcNow.AddHours(-24);
}