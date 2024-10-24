namespace TgInstanceAgent.Infrastructure.Web.Reports.InputModels;

/// <summary>
/// Модель входных данных для получения отчёта по дате
/// </summary>
public class GetReportByDateInputModel
{
    /// <summary>
    /// Дата
    /// </summary>
    public DateOnly Date { get; init; }
}