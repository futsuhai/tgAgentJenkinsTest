using MediatR;
using TgInstanceAgent.Application.Abstractions.DTOs.Instances;
using TgInstanceAgent.Application.Abstractions.Interfaces;

namespace TgInstanceAgent.Application.Abstractions.Queries.Instances;

/// <summary>
/// Запрос данных инстанса.
/// </summary>
public class GetInstanceQuery : IRequest<InstanceDto>, IWithInstanceId
{
    /// <summary>
    /// Уникальный идентификатор инстанса.
    /// </summary>
    public required Guid InstanceId { get; init; }
}