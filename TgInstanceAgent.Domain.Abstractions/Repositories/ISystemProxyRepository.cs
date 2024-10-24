using TgInstanceAgent.Domain.Abstractions.Interfaces;
using TgInstanceAgent.Domain.SystemProxy;
using TgInstanceAgent.Domain.SystemProxy.Ordering.Visitor;
using TgInstanceAgent.Domain.SystemProxy.Specifications.Visitor;

namespace TgInstanceAgent.Domain.Abstractions.Repositories;

/// <summary>
/// Интерфейс репозитория для работы с системными прокси
/// </summary>
public interface ISystemProxyRepository : IRepository<SystemProxyAggregate, Guid, ISystemProxySpecificationVisitor, ISystemProxySortingVisitor>;