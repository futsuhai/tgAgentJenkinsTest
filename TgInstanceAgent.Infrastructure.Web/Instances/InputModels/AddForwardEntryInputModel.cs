namespace TgInstanceAgent.Infrastructure.Web.Instances.InputModels;

/// <summary>
/// Модель входных данных для добавления пересылки сообщений.
/// </summary>
public class AddForwardEntryInputModel
{
    /// <summary>
    /// Идентификатор чата, из которого будут пересылка сообщения.
    /// </summary>
    public long For { get; init; }
    
    /// <summary>
    /// Идентификатор чата, в который будут пересылка сообщения.
    /// </summary>
    public long To { get; init; }
    
    /// <summary>
    /// Флаг, определяющий переслать сообщение или отправить копию.
    /// </summary>
    public bool SendCopy { get; init; }
}