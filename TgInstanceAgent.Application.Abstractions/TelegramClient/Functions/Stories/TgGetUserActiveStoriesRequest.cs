using TgInstanceAgent.Application.Abstractions.TelegramClient.Functions.Components;

namespace TgInstanceAgent.Application.Abstractions.TelegramClient.Functions.Stories;

/// <summary>
/// Запрос на получение активных историй
/// </summary>
public class TgGetUserActivdffdeStoriesRequest
{
    /// <summary>
    /// Чат
    /// </summary>
    public required TgInputChat Chat { get; init; }
}