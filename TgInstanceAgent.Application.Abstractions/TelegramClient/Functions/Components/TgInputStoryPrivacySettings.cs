using TgInstanceAgent.Application.Abstractions.TelegramClient.Functions.Components.Visitors;

namespace TgInstanceAgent.Application.Abstractions.TelegramClient.Functions.Components;

/// <summary>
/// Абстрактный класс настроек приватности истории
/// </summary>
public abstract class TgInputStoryPrivacySettings
{
    /// <summary>
    /// Принять посетителя настроек приватности истории.
    /// </summary>
    public abstract void Accept(IStoryPrivacySettingsVisitor privacySettingsVisitor);
}

/// <summary>
/// Настройки приватности, позволяющие увидеть историю только контактам пользователя
/// </summary>
public class TgInputStoryPrivacySettingsContacts : TgInputStoryPrivacySettings
{
    /// <summary>
    /// Массив идентификаторов пользователей, которые не увидят историю
    /// </summary>
    public required long[] ExceptUserIds { get; init; }
    
    /// <summary>
    /// Принять посетителя настроек приватности истории.
    /// </summary>
    public override void Accept(IStoryPrivacySettingsVisitor privacySettingsVisitor) => privacySettingsVisitor.Visit(this);
}

/// <summary>
/// Настройки приватности, позволяющие увидеть историю всем
/// </summary>
public class TgInputStoryPrivacySettingsEveryone : TgInputStoryPrivacySettings
{
    /// <summary>
    /// Массив идентификаторов пользователей, которые не увидят историю
    /// </summary>
    public required long[] ExceptUserIds { get; init; }
    
    /// <summary>
    /// Принять посетителя настроек приватности истории.
    /// </summary>
    public override void Accept(IStoryPrivacySettingsVisitor privacySettingsVisitor) => privacySettingsVisitor.Visit(this);
}

/// <summary>
/// Настройки приватности, позволяющие увидеть историю выбранным пользователям
/// </summary>
public class TgInputStoryPrivacySettingsSelectedUsers : TgInputStoryPrivacySettings
{
    /// <summary>
    /// Массив идентификаторов пользователей, которые увидят историю
    /// </summary>
    public required long[] UserIds { get; init; }
    
    /// <summary>
    /// Принять посетителя настроек приватности истории.
    /// </summary>
    public override void Accept(IStoryPrivacySettingsVisitor privacySettingsVisitor) => privacySettingsVisitor.Visit(this);
}

/// <summary>
/// Настройки приватности, позволяющие увидеть историю пользователям, находящимся в списке близких друзей
/// </summary>
public class TgInputStoryPrivacySettingsCloseFriends : TgInputStoryPrivacySettings
{
    /// <summary>
    /// Принять посетителя настроек приватности истории.
    /// </summary>
    public override void Accept(IStoryPrivacySettingsVisitor privacySettingsVisitor) => privacySettingsVisitor.Visit(this);
}