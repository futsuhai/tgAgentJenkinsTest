using TgInstanceAgent.Application.Abstractions.Interfaces;

namespace TgInstanceAgent.Application.Abstractions.DTOs.CommandsStore;

/// <summary>
/// Класс, представляющий логи выполнения команд инстанса.
/// </summary>
public class CommandsHistoryDto
{
    /// <summary>
    /// Коллекция элементов.
    /// </summary>
    public required IReadOnlyCollection<IWithCommandId> Сommands { get; init; }
    
    /// <summary>
    /// Количество элементов
    /// </summary>
    public required int TotalCount { get; init; }
}