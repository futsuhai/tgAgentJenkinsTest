namespace TgInstanceAgent.Application.Abstractions.TelegramClient.Objects.Chats;

/// <summary>
/// Результат подсчета количества элементов и их получения.
/// </summary>
public class TgChatsResult
{
    /// <summary>
    /// Коллекция элементов.
    /// </summary>
    public required IReadOnlyCollection<TgChat> List { get; init; }

    /// <summary>
    /// Общее количество элементов.
    /// </summary>
    public required bool HasMore { get; init; }
}