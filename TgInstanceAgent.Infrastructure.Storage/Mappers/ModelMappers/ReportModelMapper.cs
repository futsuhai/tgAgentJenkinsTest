using Microsoft.EntityFrameworkCore;
using TgInstanceAgent.Domain.Reports;
using TgInstanceAgent.Infrastructure.Storage.Context;
using TgInstanceAgent.Infrastructure.Storage.Entities.Reports;
using TgInstanceAgent.Infrastructure.Storage.Mappers.Abstractions;

namespace TgInstanceAgent.Infrastructure.Storage.Mappers.ModelMappers;

/// <summary>
/// Класс для преобразования ReportAggregate в ReportModel
/// </summary>
public class ReportModelMapper : IModelMapperUnit<ReportModel, ReportAggregate>
{
    /// <summary>
    /// Преобразуем ReportAggregate в ReportModel
    /// </summary>
    /// <param name="aggregate">Агрегат</param>
    /// <param name="context">Контекст базы данных</param>
    /// <returns></returns>
    public async Task<ReportModel> MapAsync(ReportAggregate aggregate, ApplicationDbContext context)
    {
        // Получаем модель инстанса из базы данных
        var model = await context.Reports
            .FirstOrDefaultAsync(x => x.Id == aggregate.Id);

        // Если модели null, то создаем новую модель, так как это новый инстанс 
        model ??= new ReportModel { Id = aggregate.Id };
        
        // Устанавливаем идентификатор инстанса
        model.InstanceId = aggregate.InstanceId;
        
        // Устанавливаем дату
        model.Date = aggregate.Date;

        // Устанавливаем количество отправленных сообщений
        model.Sent = aggregate.Sent;
        
        // Устанавливаем количество полученных сообщений
        model.Received = aggregate.Received;
        
        // Возвращаем модель
        return model;
    }
}