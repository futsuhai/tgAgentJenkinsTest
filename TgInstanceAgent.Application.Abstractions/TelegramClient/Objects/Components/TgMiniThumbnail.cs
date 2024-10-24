namespace TgInstanceAgent.Application.Abstractions.TelegramClient.Objects.Components;

/// <summary>
/// Представляет маленькое превью
/// </summary>
public class TgMiniThumbnail
{
    /// <summary>
    /// Маленькое превью.
    /// </summary>
    public required byte[] Data { get; init; }
    
    /// <summary>
    /// Ширина.
    /// </summary>
    public required int Width { get; init; }
    
    /// <summary>
    /// Высота
    /// </summary>
    public required int Height { get; init; }
}