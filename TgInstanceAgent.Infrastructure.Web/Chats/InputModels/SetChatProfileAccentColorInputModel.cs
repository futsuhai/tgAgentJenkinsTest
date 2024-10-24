namespace TgInstanceAgent.Infrastructure.Web.Chats.InputModels;

/// <summary>
/// Входная модель для изменения цвета профиля группового чата.
/// </summary>
public class SetChatProfileAccentColorInputModel
{
    /// <summary>
    /// Идентификатор чата.
    /// </summary>
    public long? ChatId { get; init; }
    
    /// <summary>
    /// Идентификатор цвета
    /// </summary>
    public int? ProfileAccentColorId { get; init; }

    /// <summary>
    /// Идентификатор премиум емозди
    /// </summary>
    public long? ProfileBackgroundCustomEmojiId { get; init; }
}