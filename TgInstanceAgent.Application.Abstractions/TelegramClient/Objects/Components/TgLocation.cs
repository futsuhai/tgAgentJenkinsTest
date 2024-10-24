namespace TgInstanceAgent.Application.Abstractions.TelegramClient.Objects.Components;

/// <summary>
/// Представляет местоположение.
/// </summary>
public class TgLocation
{
    /// <summary>
    /// Широта.
    /// </summary>
    public required double Latitude { get; init; }

    /// <summary>
    /// Долгота.
    /// </summary>
    public required double Longitude { get; init; }

    /// <summary>
    /// Горизонтальная точность.
    /// </summary>
    public double? HorizontalAccuracy { get; init; }
}