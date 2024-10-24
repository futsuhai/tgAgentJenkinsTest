namespace TgInstanceAgent.Application.Abstractions.TelegramClient.Objects.Messages;

/// <summary>
/// Найденные сообщения
/// </summary>
public class TgFoundMessages
{
    /// <summary>
    /// Коллекция элементов.
    /// </summary>
    public required IReadOnlyCollection<TgMessage> List { get; init; }

    /// <summary>
    /// Общее количество элементов.
    /// </summary>
    public required int TotalCount { get; init; }
    
    /// <summary>
    /// Следующее значение сдвига.
    /// </summary>
    public string? NextOffset { get; init; }
}