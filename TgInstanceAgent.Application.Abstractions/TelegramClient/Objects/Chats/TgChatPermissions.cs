namespace TgInstanceAgent.Application.Abstractions.TelegramClient.Objects.Chats;

/// <summary>
/// Содержит информацию о правах доступа пользователя в чате
/// </summary>
public class TgChatPermissions
{
    /// <summary>
    /// True, если пользователь может отправлять текстовые сообщения, контакты, розыгрыши, победителей розыгрышей, счета, геолокации и места
    /// </summary>
    public required bool CanSendBasicMessages { get; init; }

    /// <summary>
    /// True, если пользователь может отправлять музыкальные файлы
    /// </summary>
    public required bool CanSendAudios { get; init; }

    /// <summary>
    /// True, если пользователь может отправлять документы
    /// </summary>
    public required bool CanSendDocuments { get; init; }

    /// <summary>
    /// True, если пользователь может отправлять фотографии
    /// </summary>
    public required bool CanSendPhotos { get; init; }

    /// <summary>
    /// True, если пользователь может отправлять видео
    /// </summary>
    public required bool CanSendVideos { get; init; }

    /// <summary>
    /// True, если пользователь может отправлять видеосообщения
    /// </summary>
    public required bool CanSendVideoNotes { get; init; }

    /// <summary>
    /// True, если пользователь может отправлять голосовые сообщения
    /// </summary>
    public required bool CanSendVoiceNotes { get; init; }

    /// <summary>
    /// True, если пользователь может отправлять опросы
    /// </summary>
    public required bool CanSendPolls { get; init; }

    /// <summary>
    /// True, если пользователь может отправлять анимации, игры, стикеры, кубики и использовать встроенных ботов
    /// </summary>
    public required bool CanSendOtherMessages { get; init; }

    /// <summary>
    /// True, если пользователь может добавлять предварительный просмотр веб-страниц к своим сообщениям
    /// </summary>
    public required bool CanAddWebPagePreviews { get; init; }

    /// <summary>
    /// True, если пользователь может изменять название, фото и другие настройки чата
    /// </summary>
    public required bool CanChangeInfo { get; init; }

    /// <summary>
    /// True, если пользователь может приглашать новых пользователей в чат
    /// </summary>
    public required bool CanInviteUsers { get; init; }

    /// <summary>
    /// True, если пользователь может закреплять сообщения
    /// </summary>
    public required bool CanPinMessages { get; init; }

    /// <summary>
    /// True, если пользователь может создавать темы
    /// </summary>
    public required bool CanCreateTopics { get; init; }
}