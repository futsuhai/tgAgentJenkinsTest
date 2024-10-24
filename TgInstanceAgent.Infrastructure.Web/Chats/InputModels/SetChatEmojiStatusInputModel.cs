namespace TgInstanceAgent.Infrastructure.Web.Chats.InputModels;

/// <summary>
/// Входная модель для изменения емозди статуса чата
/// </summary>
public class SetChatEmojiStatusInputModel
{
    /// <summary>
    /// Идентификатор чата.
    /// </summary>
    public long? ChatId { get; init; }
    
    /// <summary>
    ///  Идентификатор пользовательского эмодзи, который будет установлен в качестве статуса.
    /// </summary>
    public long? CustomEmojiId { get; init; }
    
    /// <summary>
    /// Дата истечения срока действия статуса эмодзи, заданная в виде UNIX-времени.
    /// </summary>
    public int? ExpirationDate { get; init; }
}