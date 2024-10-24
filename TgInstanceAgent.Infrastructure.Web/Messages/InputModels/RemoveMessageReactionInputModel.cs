using TgInstanceAgent.Infrastructure.Web.Components.Interfaces;

namespace TgInstanceAgent.Infrastructure.Web.Messages.InputModels;

/// <summary>
/// Модель входных данных для удаления реакции с сообщения
/// </summary>
public class RemoveMessageReactionInputModel: IWithInputChat, IWithInputReaction
{
    /// <summary>
    /// Идентификатор чата.
    /// При отправке запроса по идентификатору, необходимо быть уверенным, что приложению известен данный чат.
    /// </summary>
   public long? ChatId { get; init; }
    
    /// <summary>
    /// Имя пользователя чата. Если установлено, Chat может быть не задан.
    /// </summary>
    public string? Username { get; init; }
    
    /// <summary>
    /// Номер телефона пользователя. Если установлено, Chat и Username могут быть не заданы.
    /// </summary>
    public string? PhoneNumber { get; init; }
    
    /// <summary>
    /// Реакция
    /// </summary>
    public string? Emoji { get; init; }
    
    /// <summary>
    /// Премиум-реакция
    /// </summary>
    public long? EmojiId { get; init; }
    
    /// <summary>
    /// Идентификатор сообщения
    /// </summary>
    public required long MessageId { get; init; }
}