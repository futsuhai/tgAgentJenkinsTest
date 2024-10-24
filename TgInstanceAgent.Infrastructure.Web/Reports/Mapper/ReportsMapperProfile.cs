using TgInstanceAgent.Application.Abstractions.Queries.Reports;
using TgInstanceAgent.Infrastructure.Web.Components.Mapper;
using TgInstanceAgent.Infrastructure.Web.Reports.InputModels;

namespace TgInstanceAgent.Infrastructure.Web.Reports.Mapper;

/// <summary>
/// Класс для маппинга входных моделей для работы с отчётами в команды
/// </summary>
public class ReportsMapperProfile : AutoMapper.Profile
{
    /// <summary>
    /// Маппинг входных моделей в команды
    /// </summary>
    public ReportsMapperProfile()
    {
        // Карта для GetReportByDateInputModel в GetReportByDateQuery
        CreateMap<GetReportByDateInputModel, GetReportByDateQuery>().MapInstanceId();
        
        // Карта для GetReportsByDateRangeInputModel в GetReportsByDateRangeQuery
        CreateMap<GetReportsByDatesRangeInputModel, GetReportsByDatesRangeQuery>().MapInstanceId();
    }
}