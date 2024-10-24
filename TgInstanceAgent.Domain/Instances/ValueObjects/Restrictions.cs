namespace TgInstanceAgent.Domain.Instances.ValueObjects;

/// <summary>
/// Value Object представляющий ограничения.
/// </summary>
public class Restrictions
{
    /// <summary>
    /// Текущая дата
    /// </summary>
    public DateOnly CurrentDate { get; private set; } = DateOnly.FromDateTime(DateTime.UtcNow);
    
    /// <summary>
    /// Доступное кол-во сообщений
    /// </summary>
    public int MessageCount { get; init; }
    
    /// <summary>
    /// Доступное кол-во загрузок файлов
    /// </summary>
    public int FileDownloadCount { get; init; }
    
    /// <summary>
    /// Флаг - истекли ли ограничения на текущую дату
    /// </summary>
    public bool IsRestrictionsExpired => CurrentDate < DateOnly.FromDateTime(DateTime.UtcNow);
}