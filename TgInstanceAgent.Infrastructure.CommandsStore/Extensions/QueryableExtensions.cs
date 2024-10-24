using Microsoft.EntityFrameworkCore;
using TgInstanceAgent.Infrastructure.CommandsStore.Entities;

namespace TgInstanceAgent.Infrastructure.CommandsStore.Extensions;

/// <summary>
/// Расширения для IQueryable
/// </summary>
public static class QueryableExtensions
{
    /// <summary>
    /// Расширение для IQueryable, которое загружает связанные сущности для CommandModel.
    /// </summary>
    /// <param name="queryable">Исходный запрос.</param>
    /// <returns>Запрос с загруженными связанными сущностями.</returns>
    public static IQueryable<CommandModel> LoadDependencies(this IQueryable<CommandModel> queryable)
    { 
        // Возвращаем IQueryable cо связанными сущностями
        return queryable

            .Include(commandModel => commandModel.AddInstanceCommand)
            .Include(commandModel => commandModel.AddWebhookUrlCommand)
            .Include(commandModel => commandModel.RemoveForwardEntryCommand)
            .Include(commandModel => commandModel.RemoveWebhookUrlCommand)
            .Include(commandModel => commandModel.UpdateWebhookSettingCommand)
            .Include(commandModel => commandModel.AddForwardEntryCommand)
            .Include(commandModel => commandModel.StartInstanceCommand)
            .Include(commandModel => commandModel.StopInstanceCommand);
    }
}