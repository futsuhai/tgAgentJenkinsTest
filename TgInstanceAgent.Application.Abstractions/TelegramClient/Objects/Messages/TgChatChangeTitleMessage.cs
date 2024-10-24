namespace TgInstanceAgent.Application.Abstractions.TelegramClient.Objects.Messages;

/// <summary>
/// Класс, предоставляющий собой сообщение о смене названия чата
/// </summary>
public class TgChatChangeTitleMessage: TgMessageContent
{
    /// <summary>
    /// Новое название чата
    /// </summary>
    public string Title { get; set; }
}