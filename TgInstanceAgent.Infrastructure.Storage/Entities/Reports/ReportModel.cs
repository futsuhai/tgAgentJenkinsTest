using System.ComponentModel.DataAnnotations.Schema;
using TgInstanceAgent.Infrastructure.Storage.Entities.Abstractions;

namespace TgInstanceAgent.Infrastructure.Storage.Entities.Reports;

/// <summary>
/// Модель отчёта в EntityFramework.
/// </summary>
public class ReportModel : IAggregateModel
{
    /// <summary>
    /// Идентификатор отчёта.
    /// </summary>
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public Guid Id { get; set; }
    
    /// <summary>
    /// Количество отправленных сообщений
    /// </summary>
    public uint Sent { get; set; }
    
    /// <summary>
    /// Количество полуученных сообщений
    /// </summary>
    public uint Received { get; set; }

    /// <summary>
    /// Дата отчёта
    /// </summary>
    public DateOnly Date { get; set; }
    
    /// <summary>
    /// Идентификатор инстанса.
    /// </summary>
    public Guid InstanceId  { get; set; }
}