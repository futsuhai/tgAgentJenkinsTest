using TgInstanceAgent.Application.Abstractions.TelegramClient.Objects.Enums;

namespace TgInstanceAgent.Application.Abstractions.TelegramClient.Objects.Stories;

/// <summary>
/// Представляет информацию об активных историях пользователя
/// </summary>
public class TgChatActiveStories
{
    /// <summary>
    /// Идентификатор чата
    /// </summary>
    public required long ChatId { get; init; }
    
    /// <summary>
    /// Тип списка историй
    /// </summary>
    public TgStoryTypeList? List { get; init; }
    
    /// <summary>
    /// Используется для определения порядка историй в списке
    /// </summary>
    public long? Order { get; init; }
    
    /// <summary>
    /// Идентификатор последней просмотренной активной истории
    /// </summary>
    public required int MaxReadStoryId { get; init; }
    
    /// <summary>
    /// Массив, содержащий в себе информацию о каждой истории
    /// </summary>
    public required TgStoryInfo[] Stories { get; init; }
}

/// <summary>
/// Представляет информацию об активной истории пользователя
/// </summary>
public class TgStoryInfo
{
    /// <summary>
    /// Идентификатор истории
    /// </summary>
    public required int StoryId { get; init; }
    
    /// <summary>
    /// Время публикации истории
    /// </summary>
    public required DateTime Date { get; init; }
    
    /// <summary>
    /// Истинно, если история для близких друзей
    /// </summary>
    public required bool IsForCloseFriends { get; init; }
}