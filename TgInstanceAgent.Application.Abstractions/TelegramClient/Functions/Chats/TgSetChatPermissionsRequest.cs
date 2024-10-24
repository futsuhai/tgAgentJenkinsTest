namespace TgInstanceAgent.Application.Abstractions.TelegramClient.Functions.Chats;

/// <summary>
/// Запрос на изменение разрешений в чате
/// </summary>
public class TgSetChatPermissionsRequest
{
    /// <summary>
    /// Идентификатор чата.
    /// </summary>
    public required long ChatId { get; init; }

    /// <summary>
    /// Истинно, если пользователь может отправлять текстовые сообщения, контакты, розыгрыши, победителей розыгрышей, счета, местоположения и места проведения
    /// </summary>
    public required bool CanSendBasicMessages { get; init; }

    /// <summary>
    /// Истинно, если пользователь может отправлять музыкальные файлы
    /// </summary>
    public required bool CanSendAudios { get; init; }

    /// <summary>
    /// Истинно, если пользователь может отправлять документы
    /// </summary>
    public required bool CanSendDocuments { get; init; }

    /// <summary>
    /// Истинно, если пользователь может отправлять фотографии
    /// </summary>
    public required bool CanSendPhotos { get; init; }

    /// <summary>
    /// Истинно, если пользователь может отправлять видео
    /// </summary>
    public required bool CanSendVideos { get; init; }

    /// <summary>
    /// Истинно, если пользователь может отправлять видеозаметки
    /// </summary>
    public required bool CanSendVideoNotes { get; init; }

    /// <summary>
    /// Истинно, если пользователь может отправлять голосовые заметки
    /// </summary>
    public required bool CanSendVoiceNotes { get; init; }

    /// <summary>
    /// Истинно, если пользователь может отправлять опросы
    /// </summary>
    public required bool CanSendPolls { get; init; }

    /// <summary>
    /// Истинно, если пользователь может отправлять анимации, игры, стикеры, кости и использовать встроенных ботов
    /// </summary>
    public required bool CanSendOtherMessages { get; init; }

    /// <summary>
    /// Истинно, если пользователь может добавлять предварительный просмотр веб-страниц к своим сообщениям
    /// </summary>
    public required bool CanAddWebPagePreviews { get; init; }

    /// <summary>
    /// Истинно, если пользователь может изменять название чата, фото и другие настройки
    /// </summary>
    public required bool CanChangeInfo { get; init; }

    /// <summary>
    /// Истинно, если пользователь может приглашать новых пользователей в чат
    /// </summary>
    public required bool CanInviteUsers { get; init; }

    /// <summary>
    /// Истинно, если пользователь может закреплять сообщения
    /// </summary>
    public required bool CanPinMessages { get; init; }

    /// <summary>
    /// Истинно, если пользователь может создавать темы
    /// </summary>
    public required bool CanCreateTopics { get; init; }
}