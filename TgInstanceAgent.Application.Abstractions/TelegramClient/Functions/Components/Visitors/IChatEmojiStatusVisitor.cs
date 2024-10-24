namespace TgInstanceAgent.Application.Abstractions.TelegramClient.Functions.Components.Visitors;

/// <summary>
/// Интерфейс посетителя статуса емозди чата
/// </summary>
public interface IChatEmojiStatusVisitor
{
    /// <summary>
    /// Посетить пользователя, идентификатор емозди и время истечения статуса.
    /// </summary>
    /// <param name="emojiId">Идентификатор емозди.</param>
    /// <param name="expirationDate">Время истечения статуса емозди.</param>
    void Visit(long? emojiId, int? expirationDate);
}