using TgInstanceAgent.Domain.Abstractions;
using TgInstanceAgent.Infrastructure.Storage.Context;
using TgInstanceAgent.Infrastructure.Storage.Entities.Abstractions;

namespace TgInstanceAgent.Infrastructure.Storage.Mappers.Abstractions;

/// <summary>
/// Интерфейс отображения агрегатной модели на модель базы данных.
/// </summary>
public interface IAggregateMapperUnit<TAggregate, in TModel> where TModel : IAggregateModel
{
    /// <summary>
    /// Отображает модель на агрегат.
    /// </summary>
    /// <param name="model">Модель для отображения.</param>
    /// <param name="context"></param>
    Task<TAggregate> MapAsync(TModel model, ApplicationDbContext context);
}
