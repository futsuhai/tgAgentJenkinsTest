using TgInstanceAgent.Application.Abstractions.TelegramClient.Objects.Components;

namespace TgInstanceAgent.Application.Abstractions.TelegramClient.Objects.Messages;

/// <summary>
/// Класс, обозначающий сообщение о смене фото чата
/// </summary>
public class TgChatChangePhotoMessage: TgMessageContent
{
    /// <summary>
    /// Новое фото чата
    /// </summary>
    public required TgChatPhoto Photo { get; init; }
}