namespace TgInstanceAgent.Application.Abstractions.TelegramClient.Objects.Messages;

/// <summary>
/// Информация о пересылаемом сообщении.
/// </summary>
public class TgForwardInfo
{
    /// <summary>
    /// Дата сообщения.
    /// </summary>
    public required DateTime Date { get; init; }
    
    /// <summary>
    /// MessageOrigin of the forwarded message
    /// </summary>
    public required TgMessageOrigin MessageOrigin { get; init; }
}