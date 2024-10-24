namespace TgInstanceAgent.Infrastructure.Web.Chats.InputModels;

/// <summary>
/// Модель входных данных для смены разрешений в чате
/// </summary>
public class SetChatPermissionsInputModel
{
    /// <summary>
    /// Идентификатор чата.
    /// </summary>
    public long? ChatId { get; init; }

    /// <summary>
    /// Истинно, если пользователь может отправлять текстовые сообщения, контакты, розыгрыши, победителей розыгрышей, счета, местоположения и места проведения
    /// </summary>
    public bool? CanSendBasicMessages { get; init; }

    /// <summary>
    /// Истинно, если пользователь может отправлять музыкальные файлы
    /// </summary>
    public bool? CanSendAudios { get; init; }

    /// <summary>
    /// Истинно, если пользователь может отправлять документы
    /// </summary>
    public bool? CanSendDocuments { get; init; }

    /// <summary>
    /// Истинно, если пользователь может отправлять фотографии
    /// </summary>
    public bool? CanSendPhotos { get; init; }

    /// <summary>
    /// Истинно, если пользователь может отправлять видео
    /// </summary>
    public bool? CanSendVideos { get; init; }

    /// <summary>
    /// Истинно, если пользователь может отправлять видеозаметки
    /// </summary>
    public bool? CanSendVideoNotes { get; init; }

    /// <summary>
    /// Истинно, если пользователь может отправлять голосовые заметки
    /// </summary>
    public bool? CanSendVoiceNotes { get; init; }

    /// <summary>
    /// Истинно, если пользователь может отправлять опросы
    /// </summary>
    public bool? CanSendPolls { get; init; }

    /// <summary>
    /// Истинно, если пользователь может отправлять анимации, игры, стикеры, кости и использовать встроенных ботов
    /// </summary>
    public bool? CanSendOtherMessages { get; init; }

    /// <summary>
    /// Истинно, если пользователь может добавлять предварительный просмотр веб-страниц к своим сообщениям
    /// </summary>
    public bool? CanAddWebPagePreviews { get; init; }

    /// <summary>
    /// Истинно, если пользователь может изменять название чата, фото и другие настройки
    /// </summary>
    public bool? CanChangeInfo { get; init; }

    /// <summary>
    /// Истинно, если пользователь может приглашать новых пользователей в чат
    /// </summary>
    public bool? CanInviteUsers { get; init; }

    /// <summary>
    /// Истинно, если пользователь может закреплять сообщения
    /// </summary>
    public bool? CanPinMessages { get; init; }

    /// <summary>
    /// Истинно, если пользователь может создавать темы
    /// </summary>
    public bool? CanCreateTopics { get; init; }
}