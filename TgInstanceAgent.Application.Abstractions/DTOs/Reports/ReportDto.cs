namespace TgInstanceAgent.Application.Abstractions.DTOs.Reports;

/// <summary>
/// Класс, представляющий данные отчёта.
/// </summary>
public class ReportDto
{
    /// <summary>
    /// Идентификатор отчёта.
    /// </summary>
    public required Guid Id { get; init; }
    
    /// <summary>
    /// Количество отправленных сообщений
    /// </summary>
    public required uint Sent { get; init; }

    /// <summary>
    /// Количество полученных сообщений
    /// </summary>
    public required uint Received { get; init; }

    /// <summary>
    /// Дата отчёта
    /// </summary>
    public required DateOnly Date { get; init; }
    
    /// <summary>
    /// Идентификатор инстанса.
    /// </summary>
    public required Guid InstanceId  { get; init; }
}