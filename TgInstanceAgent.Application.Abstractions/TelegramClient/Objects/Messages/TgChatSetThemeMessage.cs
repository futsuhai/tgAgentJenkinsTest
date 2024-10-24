namespace TgInstanceAgent.Application.Abstractions.TelegramClient.Objects.Messages;

/// <summary>
/// Класс, представляющий сообщение присоединение пользователя к чату по ссылке
/// </summary>
public class TgChatSetThemeMessage: TgMessageContent
{
    /// <summary>
    /// Название темы, если пустое - тема по умолчанию
    /// </summary>
    public required string ThemeName { get; set; }
}