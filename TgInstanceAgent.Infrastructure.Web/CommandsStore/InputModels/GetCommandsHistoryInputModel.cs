using TgInstanceAgent.Infrastructure.Web.Components.Interfaces;

namespace TgInstanceAgent.Infrastructure.Web.CommandsStore.InputModels;

/// <summary>
/// Модель входных данных для получения истории выполнения команд инстанса.
/// </summary>
public class GetCommandsHistoryInputModel : IWithInputLimit
{
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