using MediatR;
using TgInstanceAgent.Application.Abstractions.CommandsStore;
using TgInstanceAgent.Application.Abstractions.DTOs.CommandsStore;
using TgInstanceAgent.Application.Abstractions.Queries.CommandsStore;

namespace TgInstanceAgent.Application.Services.QueriesHandlers.CommandsStore;

/// <summary>
/// Обработчик запроса на получение истории выполненных команд инстанса.
/// </summary>
/// <param name="commandsStore">Репозиторий для работы с логами команд.</param>
public class GetCommandsHistoryQueryHandler(ICommandsStore commandsStore)
    : IRequestHandler<GetCommandsHistoryQuery, CommandsHistoryDto>
{
    /// <summary>
    /// Обрабатывает запрос на получение истории выполненных инстанса.
    /// </summary>
    /// <param name="request">Запрос истории выполнения команд инстанса.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Данные инстанса.</returns>
    public async Task<CommandsHistoryDto> Handle(GetCommandsHistoryQuery request, CancellationToken cancellationToken)
    {
        // Получаем историю команд.
        var commandsHistory = await commandsStore.GetCommandsHistory(
            request.InstanceId, request.Limit, request.Offset, request.LastId, cancellationToken);
        
        // Создаем объект CommandsHistoryDto и устанавливаем его свойства.
        return new CommandsHistoryDto
        {
            TotalCount = commandsHistory.Count,
            Сommands = commandsHistory
        };
    }
}