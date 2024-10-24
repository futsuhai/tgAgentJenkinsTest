namespace TgInstanceAgent.Infrastructure.Web.Instances.InputModels;

/// <summary>
/// Модель входных данных для удаления пересылки сообщений.
/// </summary>
public class RemoveForwardEntryInputModel
{
    /// <summary>
    /// Идентификатор чата, из которого будут пересылка сообщения.
    /// </summary>
    public long For { get; init; }
    
    /// <summary>
    /// Идентификатор чата, в который будут пересылка сообщения.
    /// </summary>
    public long To { get; init; }
}