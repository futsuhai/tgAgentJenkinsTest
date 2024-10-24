using TgInstanceAgent.Application.Abstractions.TelegramClient.Functions.Components;

namespace TgInstanceAgent.Application.Abstractions.TelegramClient.Functions.Stories;

/// <summary>
/// Запрос на установку истории 
/// </summary>
public class TgPostStoryRequest
{
    /// <summary>
    /// Чат
    /// </summary>
    public required TgInputChat Chat { get; init; }
    
    /// <summary>
    /// Контект истории
    /// </summary>
    public required TgInputStoryContent StoryContent { get; init; }
    
    /// <summary>
    /// Настройки приватности истории
    /// </summary>
    public required TgInputStoryPrivacySettings StoryPrivacySettings { get; init; }
    
    /// <summary>
    /// Подпись к файловому сообщению.
    /// </summary>
    public string? Caption { get; init; }
    
    /// <summary>
    /// Период, в который будет доступна история (в часах)
    /// </summary>
    public int ActivePeriod { get; init; }
    
    /// <summary>
    /// Истинно, если история защищена от скриншотов и пересылок
    /// </summary>
    public bool ProtectContent { get; init; }
}