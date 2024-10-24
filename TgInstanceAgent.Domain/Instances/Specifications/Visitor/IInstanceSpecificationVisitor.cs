using TgInstanceAgent.Domain.Specifications.Abstractions;

namespace TgInstanceAgent.Domain.Instances.Specifications.Visitor;

/// <summary>
/// Интерфейс для посетителя спецификации инстанса.
/// </summary>
public interface IInstanceSpecificationVisitor : ISpecificationVisitor<IInstanceSpecificationVisitor, InstanceAggregate>
{
    /// <summary>
    /// Посещает спецификацию инстанса по идентификатору пользователя.
    /// </summary>
    void Visit(InstancesByUserIdSpecification specification);

    /// <summary>
    /// Посещает спецификацию истекших инстансов.
    /// </summary>
    void Visit(ExpiredInstancesSpecification specification);

    /// <summary>
    /// Посещает спецификацию включенных инстансов.
    /// </summary>
    void Visit(EnabledInstancesSpecification specification);

    /// <summary>
    /// Посещает спецификацию аутентифицированных инстансов.
    /// </summary>
    void Visit(AuthenticatedInstancesSpecification specification);
    
    /// <summary>
    /// Посещает спецификацию инстансов с неустановленным прокси.
    /// </summary>
    void Visit(InstancesWithOwnProxySpecification specification);
}