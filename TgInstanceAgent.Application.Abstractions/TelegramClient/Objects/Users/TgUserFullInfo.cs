using TgInstanceAgent.Application.Abstractions.TelegramClient.Objects.Components;

namespace TgInstanceAgent.Application.Abstractions.TelegramClient.Objects.Users;

/// <summary>
/// Представляет полную информацию о пользователе.
/// </summary>
public class TgUserFullInfo
{
    /// <summary>
    /// Фотография профиля пользователя, установленная текущим пользователем для контакта; может быть нулевым.
    /// </summary>
    public TgChatPhoto? PersonalPhoto { get; init; }

    /// <summary>
    /// Фотография профиля пользователя.
    /// </summary>
    public TgChatPhoto? Photo { get; init; }

    /// <summary>
    /// Фотография профиля пользователя видна, если основная фотография скрыта настройками конфиденциальности.
    /// </summary>
    public TgChatPhoto? PublicPhoto { get; init; }

    /// <summary>
    /// Флаг, заблокированы ли истории пользователя.
    /// </summary>
    public required bool IsStoriesBlocked { get; init; }
    
    /// <summary>
    /// Флаг, заблокирован ли пользователь.
    /// </summary>
    public required bool IsBlocked { get; init; }

    /// <summary>
    /// Правда, если пользователя можно вызвать.
    /// </summary>
    public required bool CanBeCalled { get; init; }

    /// <summary>
    /// Правда, если с пользователем можно создать видеозвонок.
    /// </summary>
    public required bool SupportsVideoCalls { get; init; }

    /// <summary>
    /// Истинно, если пользователю невозможно позвонить из-за его настроек конфиденциальности.
    /// </summary>
    public required bool HasPrivateCalls { get; init; }

    /// <summary>
    /// Истинно, если пользователя нельзя связать в пересылаемых сообщениях из-за его настроек конфиденциальности.
    /// </summary>
    public required bool HasPrivateForwards { get; init; }

    /// <summary>
    /// Истинно, если голосовые и видеозаметки невозможно отправить или переслать пользователю.
    /// </summary>
    public required bool HasRestrictedVoiceAndVideoNoteMessages { get; init; }

    /// <summary>
    /// Истинно, если пользователь закрепил истории.
    /// </summary>
    public required bool HasPinnedStories { get; init; }

    /// <summary>
    /// Истинно, если текущему пользователю необходимо явно разрешить делиться своим номером телефона с пользователем при использовании метода addContact.
    /// </summary>
    public required bool NeedPhoneNumberPrivacyException { get; init; }

    /// <summary>
    /// Краткая биография пользователя; может быть нулевым для ботов.
    /// </summary>
    public required TgFormatedText? Bio { get; init; }

    /// <summary>
    /// Количество групповых чатов, участниками которых являются и другой пользователь, и текущий пользователь; 0 для текущего пользователя.
    /// </summary>
    public required int GroupInCommonCount { get; init; }
}