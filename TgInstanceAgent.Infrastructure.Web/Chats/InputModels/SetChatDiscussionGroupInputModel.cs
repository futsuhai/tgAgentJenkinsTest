namespace TgInstanceAgent.Infrastructure.Web.Chats.InputModels;

/// <summary>
/// Входная модель для установки группы обсуждения
/// </summary>
public class SetChatDiscussionGroupInputModel
{
    /// <summary>
    /// Идентификатор чата.
    /// </summary>
    public long? ChatId { get; init; }
    
    /// <summary>
    /// Идентификатор обсуждаемой группы
    /// </summary>
    public long? DiscussionChatId { get; init; }
}