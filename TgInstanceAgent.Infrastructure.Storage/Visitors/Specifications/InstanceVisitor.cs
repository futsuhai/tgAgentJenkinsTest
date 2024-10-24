using System.Linq.Expressions;
using TgInstanceAgent.Domain.Instances;
using TgInstanceAgent.Domain.Instances.Enums;
using TgInstanceAgent.Domain.Instances.Specifications;
using TgInstanceAgent.Domain.Instances.Specifications.Visitor;
using TgInstanceAgent.Domain.Specifications.Abstractions;
using TgInstanceAgent.Infrastructure.Storage.Entities.Instances;

namespace TgInstanceAgent.Infrastructure.Storage.Visitors.Specifications;

///  <inheritdoc cref="TgInstanceAgent.Domain.Instances.Specifications.Visitor.IInstanceSpecificationVisitor" />
/// <summary>
/// Реализация посетителя спецификации инстанса.
/// </summary>
public class InstanceVisitor : BaseVisitor<InstanceModel, IInstanceSpecificationVisitor, InstanceAggregate>,
    IInstanceSpecificationVisitor
{
    /// <inheritdoc/>
    /// <summary>
    /// Преобразует спецификацию в выражение.
    /// </summary>
    protected override Expression<Func<InstanceModel, bool>> ConvertSpecToExpression(
        ISpecification<InstanceAggregate, IInstanceSpecificationVisitor> spec)
    {
        // Создаем нового посетителя
        var visitor = new InstanceVisitor();
        
        // Посещаем спецификацию и получаем выражение
        spec.Accept(visitor);
        
        // Возвращаем выражение
        return visitor.Expr!;
    }

    /// <inheritdoc/>
    /// <summary>
    /// Посещает спецификацию по идентификатору пользователя инстанса.
    /// </summary>
    public void Visit(InstancesByUserIdSpecification specification) => Expr = x => x.UserId == specification.UserId;

    /// <inheritdoc/>
    /// <summary>
    /// Посещает спецификацию просроченных инстансов.
    /// </summary>
    public void Visit(ExpiredInstancesSpecification specification) => Expr = x => x.ExpirationTimeUtc < DateTime.UtcNow;
    
    /// <inheritdoc/>
    /// <summary>
    /// Посещает спецификацию включенных инстансов.
    /// </summary>
    public void Visit(EnabledInstancesSpecification specification) => Expr = x => x.Enabled;

    /// <inheritdoc/>
    /// <summary>
    /// Посещает спецификацию аутентифицированных инстансов.
    /// </summary>
    public void Visit(AuthenticatedInstancesSpecification specification) => Expr = x => x.State == State.Authenticated;
    
    /// <inheritdoc/>
    /// <summary>
    /// Посещает спецификацию инстансов с неустановленным прокси.
    /// </summary>
    public void Visit(InstancesWithOwnProxySpecification specification) => Expr = x => x.Proxy == null;
}