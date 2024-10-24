using TgInstanceAgent.Application.Abstractions.TelegramClient.Objects.Components;

namespace TgInstanceAgent.Application.Abstractions.TelegramClient.Objects.Chats;

/// <summary>
/// Базовая информация о фото чата.
/// </summary>
public class TgChatPhotoInfo
{
    /// <summary>
    /// ID маленькой копии (160x160) фото.
    /// </summary>
    public required TgFile Small { get; init; }

    /// <summary>
    /// ID большой копии  (640x640) фото.
    /// </summary>
    public required TgFile Big { get; init; }

    /// <summary>
    /// Анимированное фото
    /// </summary>
    public required bool HasAnimation { get; init; }

    /// <summary>
    /// Установлено текущим пользователем
    /// </summary>
    public required bool IsPersonal { get; init; }
    
    /// <summary>
    /// Миниатюра фото чата; может быть null
    /// </summary>
    public TgMiniThumbnail? MiniThumbnail { get; init; }
}
