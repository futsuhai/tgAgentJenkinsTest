namespace TgInstanceAgent.Application.Abstractions.Interfaces;

/// <summary>
///  Интерфейс для команд, содержащих id отправителя.
/// </summary>
public interface IWitMessageSender
{
    /// <summary>
    /// Идентификатор пользователя.
    /// </summary>
    public long? SenderUserId { get; init; }
    
    /// <summary>
    /// Идентификатор чата.
    /// </summary>
    public long? SenderChatId { get; init; }
}