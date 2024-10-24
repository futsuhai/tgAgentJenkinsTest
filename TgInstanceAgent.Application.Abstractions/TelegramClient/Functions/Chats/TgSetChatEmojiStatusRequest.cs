namespace TgInstanceAgent.Application.Abstractions.TelegramClient.Functions.Chats;

/// <summary>
/// Запрос на изменение емозди статуса у чата
/// </summary>
public class TgSetChatEmojiStatusRequest
{
    /// <summary>
    /// Идентификатор чата.
    /// </summary>
    public required long ChatId { get; init; }
    
    /// <summary>
    ///  Идентификатор пользовательского эмодзи, который будет установлен в качестве статуса.
    /// </summary>
    public long? CustomEmojiId { get; init; }
    
    /// <summary>
    /// Дата истечения срока действия статуса эмодзи, заданная в виде UNIX-времени.
    /// </summary>
    public int? ExpirationDate { get; init; }
}