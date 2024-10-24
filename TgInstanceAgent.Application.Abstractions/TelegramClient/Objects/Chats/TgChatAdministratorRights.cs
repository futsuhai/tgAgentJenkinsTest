namespace TgInstanceAgent.Application.Abstractions.TelegramClient.Objects.Chats;

/// <summary>
/// Права администратора в групповом чате
/// </summary>
public class TgChatAdministratorRights
{
    /// <summary>
    /// Истина, если администратор может получить доступ к журналу событий чата, получить список бустов,
    /// видеть скрытых участников супергруппы и канала, сообщать о спаме в супергруппе и игнорировать медленный режим.
    /// Подразумевается при наличии любых других привилегий; применимо только к супергруппам и каналам
    /// </summary>
    public required bool CanManageChat { get; init; }
    
    /// <summary>
    /// Истина, если администратор может изменять заголовок, фото и другие настройки чата
    /// </summary>
    public required bool CanChangeInfo { get; init; }

    /// <summary>
    /// Истина, если администратор может создавать посты в канале
    /// </summary>
    public required bool CanPostMessages { get; init; }

    /// <summary>
    /// Истина, если администратор может редактировать сообщения других пользователей
    /// </summary>
    public required bool CanEditMessages { get; init; }

    /// <summary>
    /// Истина, если администратор может удалять сообщения других пользователей
    /// </summary>
    public required bool CanDeleteMessages { get; init; }

    /// <summary>
    /// Истина, если администратор может приглашать новых пользователей в чат
    /// </summary>
    public required bool CanInviteUsers { get; init; }

    /// <summary>
    /// Истина, если администратор может ограничивать, банить или разблокировать участников чата
    /// </summary>
    public required bool CanRestrictMembers { get; init; }

    /// <summary>
    ///  Истина, если администратор может закреплять сообщения; применимо только к обычным группам и супергруппам
    /// </summary>
    public required bool CanPinMessages { get; init; }

    /// <summary>
    /// Истина, если администратор может создавать, переименовывать, закрывать, открывать, скрывать и показывать темы форума.
    /// Применимо только к супергруппам с форумами
    /// </summary>
    public required bool CanManageTopics { get; init; }

    /// <summary>
    /// Истина, если администратор может добавлять новых администраторов с ограниченным набором привилегий
    /// или понижать в должности администраторов, которых он сам назначил
    /// </summary>
    public required bool CanPromoteMembers { get; init; }

    /// <summary>
    /// Истина, если администратор может управлять видеочатами
    /// </summary>
    public required bool CanManageVideoChats { get; init; }

    /// <summary>
    /// Истина, если администратор может создавать новые истории чата, редактировать и удалять опубликованные истории;
    /// Применимо только к супергруппам и каналам
    /// </summary>
    public required bool CanPostStories { get; init; }

    /// <summary>
    /// Истина, если администратор может редактировать истории, опубликованные другими пользователями,
    /// публиковать истории на странице чата, закреплять истории чата и получать доступ к архиву историй;
    /// Применимо только к супергруппам и каналам
    /// </summary>
    public required bool CanEditStories { get; init; }

    /// <summary>
    /// Истина, если администратор может удалять истории, опубликованные другими пользователями;
    /// Применимо только к супергруппам и каналам
    /// </summary>
    public required bool CanDeleteStories { get; init; }

    /// <summary>
    /// Истина, если администратор не отображается в списке участников чата и отправляет сообщения анонимно;
    /// Применимо только к супергруппам
    /// </summary>
    public required bool IsAnonymous { get; init; }
}