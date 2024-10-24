using TgInstanceAgent.Domain.Abstractions;
using TgInstanceAgent.Infrastructure.Storage.Context;
using TgInstanceAgent.Infrastructure.Storage.Entities.Abstractions;

namespace TgInstanceAgent.Infrastructure.Storage.Mappers.Abstractions;

/// <summary>
/// Интерфейс отображения модели базы данных на агрегатную модель.
/// </summary>
public interface IModelMapperUnit<TModel, in TAggregate>
    where TAggregate : AggregateRoot where TModel : IAggregateModel
{
    /// <summary>
    /// Асинхронно отображает агрегат на модель.
    /// </summary>
    /// <param name="model">Агрегат для отображения.</param>
    /// <param name="context">Контекст базы данных.</param>
    ///<returns>Асинхронную задачу, представляющую отображенную модель.</returns>
    Task<TModel> MapAsync(TAggregate model, ApplicationDbContext context);
}