using TgInstanceAgent.Application.Abstractions.TelegramClient.Objects.Components;

namespace TgInstanceAgent.Application.Abstractions.TelegramClient.Objects.Users;

/// <summary>
/// Класс представляющий контакт.
/// </summary>
public class TgUser
{
    /// <summary>
    /// Идентификатор.
    /// </summary>
    public required long Id { get; init; }

    /// <summary>
    /// Имя.
    /// </summary>
    public required string FirstName { get; init; }

    /// <summary>
    /// Фамилия.
    /// </summary>
    public string? LastName { get; init; }

    /// <summary>
    /// Имя пользователя чата. Если установлено, Chat может быть не задан.
    /// </summary>
    public string? Username { get; init; }

    /// <summary>
    /// Номер телефона пользователя. Если установлено, Chat и Username могут быть не заданы.
    /// </summary>
    public string? PhoneNumber { get; init; }
    
    /// <summary>
    /// Это контакт
    /// </summary>
    public required bool IsContact { get; init; }
    
    /// <summary>
    /// Является близким другом.
    /// </summary>
    public required bool IsCloseFriend { get; init; }

    /// <summary>
    /// Является взаимным контактом.
    /// </summary>
    public required bool IsMutualContact { get; init; }

    /// <summary>
    /// Является фейковым контактом.
    /// </summary>
    public required bool IsFake { get; init; }

    /// <summary>
    /// Является скамом.
    /// </summary>
    public required bool IsScum { get; init; }

    /// <summary>
    /// Верифицирован.
    /// </summary>
    public required bool IsVerified { get; init; }

    /// <summary>
    /// Есть истории
    /// </summary>
    public required bool HasActiveStories { get; init; }

    /// <summary>
    /// Есть непросмотренные истории
    /// </summary>
    public required bool HasUnreadActiveStories { get; init; }

    /// <summary>
    /// Премиум аккаунт
    /// </summary>
    public required bool IsPremium { get; init; }
    
    /// <summary>
    /// Смайлик в статусе
    /// </summary>
    public TgEmojiStatus? EmojiStatus { get; init; }

    /// <summary>
    /// Фото профиля
    /// </summary>
    public required TgProfilePhoto? ProfilePhoto { get; init; }

    /// <summary>
    /// Статус
    /// </summary>
    public TgUserStatus? Status { get; init; }
}