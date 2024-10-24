namespace TgInstanceAgent.Application.Abstractions.TelegramClient.Objects.Messages;

/// <summary>
/// Данные сообщения типа истории.
/// </summary>
public class TgStoryMessage : TgMessageContent
{
    /// <summary>
    /// Идентификатор чата, в котором опубликована история
    /// </summary>
    public required long StorySenderChatId { get; init; }
    
    /// <summary>
    /// Идентификатор истории
    /// </summary>
    public required int StoryId { get; init; }
}