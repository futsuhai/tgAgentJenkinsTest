namespace TgInstanceAgent.Application.Abstractions.TelegramClient.Objects.Chats;

/// <summary>
/// Команда бота
/// </summary>
public class TgBotCommand
{
    /// <summary>
    /// Команда
    /// </summary>
    public required string Command { get; init; }
    
    /// <summary>
    /// Описание команды
    /// </summary>
    public required string Description { get; init; }
}