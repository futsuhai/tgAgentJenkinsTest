namespace TgInstanceAgent.Application.Abstractions.TelegramClient.Objects.Users;

/// <summary>
/// Статус онлайна пользователя.
/// </summary>
public abstract class TgUserStatus;

/// <summary>
/// Описывает последнее время, когда пользователь был в сети
/// </summary>
public class TgUserStatusEmpty : TgUserStatus;

/// <summary>
/// Пользователь не в сети, но был в сети в прошлом месяце
/// </summary>
public class TgUserStatusLastMonth : TgUserStatus
{
    /// <summary>
    /// Точный статус пользователя скрыт, потому что текущий пользователь включил настройку конфиденциальности userPrivacySettingShowStatus для пользователя и не имеет Telegram Premium
    /// </summary>
    public required bool ByMyPrivacySettings { get; init; }
}

/// <summary>
/// Пользователь не в сети, но был в сети на прошлой неделе
/// </summary>
public class TgUserStatusLastWeek : TgUserStatus
{
    /// <summary>
    /// Точный статус пользователя скрыт, потому что текущий пользователь включил настройку конфиденциальности userPrivacySettingShowStatus для пользователя и не имеет Telegram Premium
    /// </summary>
    public required bool ByMyPrivacySettings { get; init; }
}

/// <summary>
/// Пользователь не в сети
/// </summary>
public class TgUserStatusOffline : TgUserStatus
{
    /// <summary>
    /// Момент времени, когда пользователь был в сети в последний раз
    /// </summary>
    public required DateTime WasOnline { get; init; }
}

/// <summary>
/// Пользователь в сети
/// </summary>
public class TgUserStatusOnline : TgUserStatus
{
    /// <summary>
    /// Момент времени, когда статус пользователя в сети истечет
    /// </summary>
    public required DateTime Expires { get; init; }
}

/// <summary>
/// Пользователь был в сети недавно
/// </summary>
public class TgUserStatusRecently : TgUserStatus
{
    /// <summary>
    /// Точный статус пользователя скрыт, потому что текущий пользователь включил настройку конфиденциальности userPrivacySettingShowStatus для пользователя и не имеет Telegram Premium
    /// </summary>
    public required bool ByMyPrivacySettings { get; init; }
}