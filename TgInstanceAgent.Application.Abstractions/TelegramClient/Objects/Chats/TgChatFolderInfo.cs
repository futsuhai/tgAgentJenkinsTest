namespace TgInstanceAgent.Application.Abstractions.TelegramClient.Objects.Chats;

/// <summary>
/// Содержит базовую информацию о папке чата
/// </summary>
public class TgChatFolderInfo
{
    /// <summary>
    /// Уникальный идентификатор папки чата
    /// </summary>
    public required int Id { get; init; }

    /// <summary>
    /// Название папки; 1-12 символов без переводов строк
    /// </summary>
    public required string Title { get; init; }

    /// <summary>
    /// Выбранное имя иконки для краткого представления папки; одно из "All", "Unread", "Unmuted", "Bots", "Channels", "Groups", "Private", "Custom", "Setup", "Cat", "Crown",
    /// "Favorite", "Flower", "Game", "Home", "Love", "Mask", "Party", "Sport", "Study", "Trade", "Travel", "Work", "Airplane", "Book", "Light", "Like", "Money", "Note", "Palette"
    /// </summary>
    public required string IconName { get; init; }

    /// <summary>
    /// Идентификатор выбранного цвета для иконки папки чата; от -1 до 6. Если -1, то цвет отключен
    /// </summary>
    public required int ColorId { get; init; }

    /// <summary>
    /// True, если для папки была создана хотя бы одна ссылка
    /// </summary>
    public required bool IsShareable { get; init; }

    /// <summary>
    /// True, если для папки чата были созданы пригласительные ссылки текущим пользователем
    /// </summary>
    public required bool HasMyInviteLinks { get; init; }
}
