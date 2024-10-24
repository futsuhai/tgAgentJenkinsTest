namespace TgInstanceAgent.Application.Abstractions.TelegramClient.Objects.Components;

/// <summary>
/// Описание фона чата
/// </summary>
public class TgBackground
{
    /// <summary>
    /// Уникальный ID фона
    /// </summary>
    public required long Id { get; init; }
    
    /// <summary>
    /// True, если это один из стандартных фонов
    /// </summary>
    public required bool IsDefault { get; init; }
    
    /// <summary>
    /// True, если фон темный и рекомендован к использованию с темной темой
    /// </summary>
    public required bool IsDark { get; init; }
    
    /// <summary>
    /// Уникальное имя фона
    /// </summary>
    public required string Name { get; init; }
    
    /// <summary>
    /// Документ фона, может быть null
    /// Только для залитого(filled) фона и chat theme фона
    /// </summary>
    public TgDocument? Document { get; init; }
    
    /// <summary>
    /// Тип фона
    /// </summary>
    public required TgBackgroundType Type { get; init; }
}