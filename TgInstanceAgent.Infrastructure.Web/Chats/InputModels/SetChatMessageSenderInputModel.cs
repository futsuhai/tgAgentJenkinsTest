namespace TgInstanceAgent.Infrastructure.Web.Chats.InputModels;

/// <summary>
/// Входная модель для изменения отправителя сообщений в групповом чате
/// </summary>
public class SetChatMessageSenderInputModel
{
    /// <summary>
    /// Идентификатор чата.
    /// </summary>
    public long? ChatId { get; init; }
    
    /// <summary>
    /// Идентификатор пользователя.
    /// </summary>
    public long? SenderUserId { get; init; }
    
    /// <summary>
    /// Идентификатор чата.
    /// </summary>
    public long? SenderChatId { get; init; }
}