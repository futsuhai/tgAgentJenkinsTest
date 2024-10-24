using TgInstanceAgent.Application.Abstractions.Interfaces;

namespace TgInstanceAgent.Application.Abstractions.CommandsStore;

/// <summary>
/// Интерфейс, описывающий логгирование команд.
/// </summary>
public interface ICommandsStore
{
    /// <summary>
    /// Сохранить данные о логгировании команды.
    /// </summary>
    /// <param name="command">Команда</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Задача, представляющая асинхронную операцию</returns>
    Task StoreCommand(IWithCommandId command, CancellationToken cancellationToken);
 
    /// <summary>
    /// Получить историю выполнения команд для указанного инстанса с пагинацией.
    /// </summary>
    /// <param name="instanceId">Уникальный идентификатор инстанса.</param>
    /// <param name="limit">Максимальное количество команд, которое нужно вернуть.</param>
    /// <param name="offset">Смещение, которое нужно использовать для пагинации.</param>
    /// <param name="lastId">Идентификатор последней команды в предыдущем запросе. Используется для пагинации.</param>
    /// <param name="cancellationToken">Токен отмены для отмены операции.</param>
    /// <returns>Коллекция DTO моделей команд.</returns>
    Task<IReadOnlyCollection<IWithCommandId>> GetCommandsHistory(Guid instanceId, int limit, int? offset,
        Guid? lastId, CancellationToken cancellationToken);
}