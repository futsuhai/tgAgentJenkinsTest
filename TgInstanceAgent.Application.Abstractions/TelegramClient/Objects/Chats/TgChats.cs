using TgInstanceAgent.Application.Abstractions.TelegramClient.Objects.Users;

namespace TgInstanceAgent.Application.Abstractions.TelegramClient.Objects.Chats;

/// <summary>
/// Результат запроса чатов
/// </summary>
public class TgChats
{
    /// <summary>
    /// Чаты.
    /// </summary>
    public required IReadOnlyCollection<TgChat> Chats { get; init; }
    
    /// <summary>
    /// Пользователи, чьи идентификаторы присутствуют в обновлении, необходимо для отображения чата.
    /// </summary>
    public IReadOnlyCollection<TgUser>? Users { get; init; } 
    
    /// <summary>
    /// Общее количество элементов.
    /// </summary>
    public required bool HasMore { get; init; }
}