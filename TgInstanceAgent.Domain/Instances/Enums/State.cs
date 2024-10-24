namespace TgInstanceAgent.Domain.Instances.Enums;

/// <summary>
/// Перечисление состояний инстансов.
/// </summary>
public enum State
{
    /// <summary>
    /// Не авторизирован
    /// </summary>
    NotAuthenticated = 1,
    
    /// <summary>
    /// Авторизирован
    /// </summary>
    Authenticated = 2,
    
    /// <summary>
    /// Удален
    /// </summary>
    Deleted = 3
}