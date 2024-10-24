using Microsoft.EntityFrameworkCore;
using TgInstanceAgent.Infrastructure.Storage.Entities.Instances;

namespace TgInstanceAgent.Infrastructure.Storage.Extensions;

/// <summary>
/// Расширения для IQueryable
/// </summary>
public static class QueryableExtensions
{
    /// <summary>
    /// Расширение для IQueryable, которое загружает связанные сущности для InstanceModel.
    /// </summary>
    /// <param name="queryable">Исходный запрос.</param>
    /// <returns>Запрос с загруженными связанными сущностями.</returns>
    public static IQueryable<InstanceModel> LoadDependencies(this IQueryable<InstanceModel> queryable)
    { 
        // Возвращаем IQueryable cо связанными сущностями
        return queryable
            .Include(instanceModel => instanceModel.WebhookSetting)
            .Include(instanceModel => instanceModel.WebhookUrls)
            .Include(instanceModel => instanceModel.Proxy)
            .Include(instanceModel => instanceModel.Restrictions)
            .Include(instanceModel => instanceModel.ForwardEntries);
    }
}