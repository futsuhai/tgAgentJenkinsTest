namespace TgInstanceAgent.Application.Abstractions.TelegramClient.Objects.Chats;

/// <summary>
/// Список команд ботов в группе
/// </summary>
public class TgBotCommands
{
    /// <summary>
    /// Идентификатор бота
    /// </summary>
    public required long BotUserId { get; init; }
    
    /// <summary>
    /// Список команд бота
    /// </summary>
    public TgBotCommand[] Commands { get; init; }
}