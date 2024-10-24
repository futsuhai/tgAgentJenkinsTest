using TgInstanceAgent.Domain.Reports;
using TgInstanceAgent.Infrastructure.Storage.Context;
using TgInstanceAgent.Infrastructure.Storage.Entities.Reports;
using TgInstanceAgent.Infrastructure.Storage.Mappers.Abstractions;

namespace TgInstanceAgent.Infrastructure.Storage.Mappers.AggregateMappers;

/// <summary>
/// Класс для преобразования ReportModel в ReportAggregate
/// </summary>
public class ReportMapper : IAggregateMapperUnit<ReportAggregate, ReportModel>
{
    /// <summary>
    /// Преобразуем ReportModel в ReportAggregate
    /// </summary>
    /// <param name="model">Модель отчёта</param>
    /// <param name="context">Контекст базы данных</param>
    public Task<ReportAggregate> MapAsync(ReportModel model, ApplicationDbContext context)
    {
        // Создаем новый экземпляр ReportAggregate с помощью конструктора
        var report = new ReportAggregate(model.Id, model.Sent, model.Received, model.Date, model.InstanceId);
        
        // Возвращаем агрегат
        return Task.FromResult(report);
    }
}