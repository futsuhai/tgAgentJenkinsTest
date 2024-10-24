using MediatR;
using TgInstanceAgent.Application.Abstractions.Interfaces;

namespace TgInstanceAgent.Application.Abstractions.Commands.Instances;

/// <summary>
/// Команда для удаления URL из инстанса.
/// Реализует запрос через MediatR для обработки операции удаления URL.
/// </summary>
public class RemoveWebhookUrlCommand : IRequest, IWithCommandId, IWithInstanceId
{
    /// <summary>
    /// Уникальный идентификатор команды.
    /// </summary>
    public required Guid CommandId { get; init; }
    
    /// <summary>
    /// Уникальный идентификатор инстанса, для которого нужно удалить URL.
    /// </summary>
    public required Guid InstanceId { get; init; }

    /// <summary>
    /// URL, который нужно удалить из инстанса.
    /// </summary>
    public required Uri Url { get; init; }
}

