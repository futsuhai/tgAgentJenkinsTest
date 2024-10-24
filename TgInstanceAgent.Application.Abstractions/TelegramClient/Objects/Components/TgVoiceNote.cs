namespace TgInstanceAgent.Application.Abstractions.TelegramClient.Objects.Components;

/// <summary>
/// Представляет голосовое сообщение.
/// </summary>
public class TgVoiceNote
{
    /// <summary>
    /// Файл
    /// </summary>
    public required TgFile File { get; init; }

    /// <summary>
    /// Длительность голосового сообщения.
    /// </summary>
    public required long Duration { get; init; }
}