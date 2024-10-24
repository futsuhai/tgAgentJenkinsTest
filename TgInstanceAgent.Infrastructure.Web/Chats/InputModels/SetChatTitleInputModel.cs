namespace TgInstanceAgent.Infrastructure.Web.Chats.InputModels;

/// <summary>
/// Модель входных данных для смены заголовка чата
/// </summary>
public class SetChatTitleInputModel
{
    /// <summary>
    /// Идентификатор чата.
    /// </summary>
    public long? ChatId { get; init; }
    
    /// <summary>
    /// Заголовок.
    /// </summary>
    public string? Title { get; init; }
}