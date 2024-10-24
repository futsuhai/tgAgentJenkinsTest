using TgInstanceAgent.Domain.Abstractions;

namespace TgInstanceAgent.Domain.Reports;

/// <summary>
/// Агрегат ReportAggregate  
/// </summary>
public class ReportAggregate(Guid id): AggregateRoot(id)
{
    /// <summary>
    /// Количество отправленных сообщений
    /// </summary>
    public uint Sent { get; private set; } = 0;

    /// <summary>
    /// Количество полученных сообщений
    /// </summary>
    public uint Received { get; private set; } = 0;

    /// <summary>
    /// Дата отчёта
    /// </summary>
    public DateOnly Date { get; set; }
    
    /// <summary>
    /// Идентификатор инстанса.
    /// </summary>
    public Guid InstanceId  { get; set; }
    
    /// <summary>
    /// Конструктор
    /// </summary>
    public ReportAggregate(Guid id, uint sent, uint received, DateOnly date, Guid instanceId) : this(id)
    {
        Sent = sent;
        Received = received;
        Date = date;
        InstanceId = instanceId;
    }

    /// <summary>
    /// Добавить отправленное сообщение
    /// </summary>
    public void AddSent() => Sent++;
    
    /// <summary>
    /// Добавить полученное сообщение
    /// </summary>
    public void AddReceived() => Received++;
}