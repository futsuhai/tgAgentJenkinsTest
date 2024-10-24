using TgInstanceAgent.Application.Abstractions.TelegramClient.Objects.Users;

namespace TgInstanceAgent.Application.Abstractions.TelegramClient.Objects.Messages;

/// <summary>
/// Результат запроса сообщений
/// </summary>
public class TgMessages
{
    /// <summary>
    /// Чаты.
    /// </summary>
    public required IReadOnlyCollection<TgMessage> Messages { get; init; }
    
    /// <summary>
    /// Пользователи, чьи идентификаторы присутствуют в обновлении, необходимо для отображения чата.
    /// </summary>
    public IReadOnlyCollection<TgUser>? Users { get; init; } 
    
    /// <summary>
    /// Общее количество элементов.
    /// </summary>
    public required int TotalCount { get; init; }
}