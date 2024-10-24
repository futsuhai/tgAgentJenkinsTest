namespace TgInstanceAgent.Infrastructure.Storage.Entities.Instances;

public class RestrictionsModel
{
    /// <summary>
    /// Текущая дата
    /// </summary>
    public DateOnly CurrentDate { get; set; } = DateOnly.FromDateTime(DateTime.Now);
    
    /// <summary>
    /// Доступное кол-во сообщений
    /// </summary>
    public int MessageCount { get; set; }
    
    /// <summary>
    /// Доступное кол-во загрузок файлов
    /// </summary>
    public int FileDownloadCount { get; set; }
    
    /// <summary>
    /// Идентификатор инстанса.
    /// </summary>
    public Guid InstanceId { get; set; }
}