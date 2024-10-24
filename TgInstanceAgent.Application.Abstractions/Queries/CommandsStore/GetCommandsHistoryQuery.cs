using MediatR;
using TgInstanceAgent.Application.Abstractions.DTOs.CommandsStore;
using TgInstanceAgent.Application.Abstractions.Interfaces;

namespace TgInstanceAgent.Application.Abstractions.Queries.CommandsStore;

/// <summary>
/// Запрос истории выполнения команд инстанса.
/// </summary>
public class GetCommandsHistoryQuery : IWithInstanceId, IRequest<CommandsHistoryDto>
{
    /// <summary>
    /// Уникальный идентификатор инстанса.
    /// </summary>
    public required Guid InstanceId { get; init; }
    
    /// <summary>
    /// Лимит команд.
    /// </summary>
    public int Limit { get; init; } = 100;

    /// <summary>
    /// Смещение.
    /// </summary>
    public int? Offset { get; init; }
    
    /// <summary>
    /// Идентификатор последней команды в предыдущем запросе.
    /// </summary>
    public Guid? LastId { get; init; }
}