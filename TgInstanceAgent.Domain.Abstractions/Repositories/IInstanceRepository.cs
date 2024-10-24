using TgInstanceAgent.Domain.Abstractions.Interfaces;
using TgInstanceAgent.Domain.Instances;
using TgInstanceAgent.Domain.Instances.Ordering.Visitor;
using TgInstanceAgent.Domain.Instances.Specifications.Visitor;

namespace TgInstanceAgent.Domain.Abstractions.Repositories;

/// <summary>
/// Интерфейс репозитория для работы с инстансами.
/// </summary>
public interface IInstanceRepository : IRepository<InstanceAggregate, Guid, IInstanceSpecificationVisitor, IInstanceSortingVisitor>;
