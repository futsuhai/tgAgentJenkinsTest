using Microsoft.EntityFrameworkCore;
using TgInstanceAgent.Application.Abstractions.Commands.Instances;
using TgInstanceAgent.Application.Abstractions.CommandsStore;
using TgInstanceAgent.Application.Abstractions.Interfaces;
using TgInstanceAgent.Infrastructure.CommandsStore.Context;
using TgInstanceAgent.Infrastructure.CommandsStore.Entities;
using TgInstanceAgent.Infrastructure.CommandsStore.Extensions;

namespace TgInstanceAgent.Infrastructure.CommandsStore.CommandsStore;

/// <summary>
/// Класс для записи логов команд.
/// </summary>
public class CommandStore(CommandsDbContext context) : ICommandsStore
{
    /// <summary>
    /// Метод для сохранения даныых о выполнении команды в бд.
    /// </summary>
    /// <param name="command">Команда.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    public async Task StoreCommand(IWithCommandId command, CancellationToken cancellationToken)
    {
        // Создаем новый объект CommandModel
        var commandModel = new CommandModel
        {
            // Генерируем уникальный идентификатор команды
            CommandId = command.CommandId,
            
            // Устанавливаем время выполнения команды
            TimeStamp = DateTime.UtcNow
        };
        
        // Проверяем тип команды и устанавливаем соответствующее свойство объекта CommandModel
        switch (command)
        {
            // Если команда является экземпляром класса AddInstanceCommand
            case AddInstanceCommand addInstanceCommand:
                commandModel.InstanceId = addInstanceCommand.InstanceId;
                commandModel.AddInstanceCommand = addInstanceCommand;
                break;
            
            // Если команда является экземпляром класса AddWebhookUrlCommand
            case AddWebhookUrlCommand addWebhookUrlCommand:
                commandModel.InstanceId = addWebhookUrlCommand.InstanceId;
                commandModel.AddWebhookUrlCommand = addWebhookUrlCommand;
                break;
            
            // Если команда является экземпляром класса RemoveForwardEntryCommand
            case RemoveForwardEntryCommand removeForwardEntryCommand:
                commandModel.InstanceId = removeForwardEntryCommand.InstanceId;
                commandModel.RemoveForwardEntryCommand = removeForwardEntryCommand;
                break;
            
            // Если команда является экземпляром класса RemoveWebhookUrlCommand
            case RemoveWebhookUrlCommand removeWebhookUrlCommand:
                commandModel.InstanceId = removeWebhookUrlCommand.InstanceId;
                commandModel.RemoveWebhookUrlCommand = removeWebhookUrlCommand;
                break;
            
            // Если команда является экземпляром класса UpdateWebhookSettingCommand
            case UpdateWebhookSettingCommand updateWebhookSettingCommand:
                commandModel.InstanceId = updateWebhookSettingCommand.InstanceId;
                commandModel.UpdateWebhookSettingCommand = updateWebhookSettingCommand;
                break;
            
            // Если команда является экземпляром класса AddForwardEntryCommand
            case AddForwardEntryCommand addForwardEntryCommand:
                commandModel.InstanceId = addForwardEntryCommand.InstanceId;
                commandModel.AddForwardEntryCommand = addForwardEntryCommand;
                break;
            
            // Если команда является экземпляром класса StartInstanceCommand
            case StartInstanceCommand startInstanceCommand:
                commandModel.InstanceId = startInstanceCommand.InstanceId;
                commandModel.StartInstanceCommand = startInstanceCommand;
                break;
            
            // Если команда является экземпляром класса StopInstanceCommand
            case StopInstanceCommand stopInstanceCommand:
                commandModel.InstanceId = stopInstanceCommand.InstanceId;
                commandModel.StopInstanceCommand = stopInstanceCommand;
                break;
            
            // Если команда не является ни одним из этих типов, то выбрасываем исключение NotSupportedException
            default:
                throw new NotSupportedException($"Command type {command.GetType().Name} is not supported.");
        }

        // Асинхронное добавление сущности в контекст и сохранение изменений
        await context.AddAsync(commandModel, cancellationToken);
        
        // Асинхронно сохраняем изменения в базе данных
        await context.SaveChangesAsync(cancellationToken);
    }

    /// <summary>
    /// Получить историю выполнения команд для указанного инстанса с пагинацией.
    /// </summary>
    /// <param name="instanceId">Уникальный идентификатор инстанса.</param>
    /// <param name="limit">Максимальное количество команд, которое нужно вернуть.</param>
    /// <param name="offset">Смещение, которое нужно использовать для пагинации.</param>
    /// <param name="lastId">Идентификатор последней команды в предыдущем запросе. Используется для пагинации.</param>
    /// <param name="cancellationToken">Токен отмены для отмены операции.</param>
    /// <returns>Коллекция DTO моделей команд.</returns>
    public async Task<IReadOnlyCollection<IWithCommandId>> GetCommandsHistory(Guid instanceId, int limit, int? offset,
        Guid? lastId, CancellationToken cancellationToken)
    {
        // Получаем команду по lastId
        var lastCommandModel = await context.Commands
            .Where(c => c.InstanceId == instanceId && c.CommandId == lastId)
            .FirstOrDefaultAsync(cancellationToken);
        
        // Получаем список моделей команд из базы данных, фильтруя их
        var commandModels = await context.Commands
            .Where(c => c.InstanceId == instanceId && (lastCommandModel == null || c.TimeStamp < lastCommandModel.TimeStamp))
            .OrderByDescending(c => c.TimeStamp)
            .Skip(offset ?? 0)
            .Take(limit)
            .LoadDependencies()
            .ToArrayAsync(cancellationToken);
        
        // Выбираем нужные поля типа IRequest
        var commands = commandModels
            .SelectMany(c => c.GetType().GetProperties()
                .Where(p => typeof(IWithCommandId).IsAssignableFrom(p.PropertyType))
                .Select(p => p.GetValue(c))
                .Where(v => v != null)
                .Cast<IWithCommandId>())
            .ToArray();
        
        // Преобразуем список DTO команд в неизменяемую коллекцию и возвращаем ее.
        return commands;
    }
}