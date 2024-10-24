using TgInstanceAgent.Application.Abstractions.TelegramClient.Functions.Components;

namespace TgInstanceAgent.Application.Abstractions.TelegramClient.Functions.Users;

/// <summary>
/// Запрос на получение фотографий профиля
/// </summary>
public class TgGetUserProfilePhotosRequest
{
    /// <summary>
    /// Лимит истории чата.
    /// </summary>
    public required int Limit { get; init; }

    /// <summary>
    /// Смещение истории чата.
    /// </summary>
    public int? Offset { get; init; }
    
    /// <summary>
    /// Чат
    /// </summary>
    public required TgInputUser User { get; init; }
}