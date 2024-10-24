namespace TgInstanceAgent.Infrastructure.Web.Reports.InputModels;

/// <summary>
/// Модель входных данных для получения отчёта в диапазоне дат
/// </summary>
public class GetReportsByDatesRangeInputModel
{
    /// <summary>
    /// Дата начала диапазона
    /// </summary>
    public DateOnly StartDate { get; init; }
    
    /// <summary>
    /// Дата конца диапазона
    /// </summary>
    public DateOnly EndDate { get; init; }
}