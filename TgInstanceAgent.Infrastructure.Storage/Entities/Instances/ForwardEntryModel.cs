namespace TgInstanceAgent.Infrastructure.Storage.Entities.Instances;

/// <summary>
/// Модель пересылки сообщений в EntityFramework.
/// </summary>
public class ForwardEntryModel
{
    /// <summary>
    /// Идентификатор пересылки.
    /// </summary>
    public long Id { get; set; }
    
    /// <summary>
    /// Идентификатор чата, из которого будут пересылка сообщения.
    /// </summary>
    public long For { get; set; }
    
    /// <summary>
    /// Идентификатор чата, в который будут пересылка сообщения.
    /// </summary>
    public long To { get; set; }
    
    /// <summary>
    /// Флаг, определяющий переслать сообщение или отправить копию.
    /// </summary>
    public bool SendCopy { get; set; }
    
    /// <summary>
    /// Идентификатор инстанса.
    /// </summary>
    public Guid InstanceId { get; set; }

    /// <summary>
    /// Экземпляр, к которому относится прокси.
    /// </summary>
    public InstanceModel Instance { get; set; } = null!;
}