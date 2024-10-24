using TgInstanceAgent.Application.Abstractions.TelegramClient.Functions.Components;

namespace TgInstanceAgent.Application.Abstractions.TelegramClient.Functions.Stories;

/// <summary>
/// Запрос на получение истории
/// </summary>
public class TgGetStoryRequest
{
    /// <summary>
    /// Чат
    /// </summary>
    public required TgInputChat Chat { get; init; }
    
    /// <summary>
    /// Идентификатор истории
    /// </summary>
    public required int StoryId { get; init; }
}