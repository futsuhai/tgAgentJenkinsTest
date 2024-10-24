using MediatR;
using TgInstanceAgent.Application.Abstractions.Interfaces;

namespace TgInstanceAgent.Application.Abstractions.Commands.Instances;

/// <summary>
/// Команда для добавления нового URL к инстансу.
/// Команда реализует IRequest для обработки MediatR и IWithInstanceId для обеспечения наличия идентификатора инстанса.
/// </summary>
public class AddWebhookUrlCommand : IRequest, IWithCommandId, IWithInstanceId
{
    /// <summary>
    /// Уникальный идентификатор команды.
    /// </summary>
    public required Guid CommandId { get; init; }
    
    /// <summary>
    /// Уникальный идентификатор инстанса, к которому добавляется URL.
    /// </summary>
    public required Guid InstanceId { get; init; }

    /// <summary>
    /// Новый URL, который нужно добавить к инстансу.
    /// </summary>
    public required Uri Url { get; init; }
}