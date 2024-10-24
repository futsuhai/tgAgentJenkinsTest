namespace TgInstanceAgent.Infrastructure.Web.Chats.InputModels;

/// <summary>
/// Модель входных данных для смены задержки отправки сообщений в чате
/// </summary>
public class SetChatSlowModeDelayInputModel
{
    /// <summary>
    /// Идентификатор чата.
    /// </summary>
    public long? ChatId { get; init; }
    
    /// <summary>
    /// Задержка
    /// </summary>
    public int? SlowModeDelay { get; init; }
}