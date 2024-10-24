namespace TgInstanceAgent.Application.Abstractions.TelegramClient.Objects.Messages;

/// <summary>
/// Информация для ответа на сообщение.
/// </summary>
public abstract class TgMessageReplyTo
{
    /// <summary>
    /// Данные чата.
    /// </summary>
    public required long ChatId { get; init; }
}

/// <summary>
/// Ответ на сообщение типа сообщение.
/// </summary>
public class TgMessageReplyToMessage : TgMessageReplyTo
{
    /// <summary>
    /// Идентификатор сообщения.
    /// </summary>
    public required long MessageId { get; init; }
}

/// <summary>
/// Ответ на историю.
/// </summary>
public class TgMessageReplyToStory : TgMessageReplyTo
{
    /// <summary>
    /// Идентификатор истории.
    /// </summary>
    public required int StoryId { get; init; }
}