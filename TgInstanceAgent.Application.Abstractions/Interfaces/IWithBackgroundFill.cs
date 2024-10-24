namespace TgInstanceAgent.Application.Abstractions.Interfaces;

public interface IWithBackgroundFill
{
    /// <summary>
    /// Цвет фона в формате HEX
    /// </summary>
    public string? Color { get; init; }
    
    /// <summary>
    /// Список из 3 или 4 цветов градиентов произвольной формы в формате HEX
    /// </summary>
    public string[]? Colors { get; init; }
    
    /// <summary>
    /// Данные для возможности установки градиентного фона
    /// </summary>
    public BackgroundGradientData? Gradient { get; init; }
}

/// <summary>
/// Модель данных для возможности установки градиентного фона
/// </summary>
public class BackgroundGradientData
{
    /// <summary>
    /// Верхний цвет фона в формате HEX.
    /// </summary>
    public string? TopColor { get; init; }

    /// <summary>
    /// Нижний цвет фона в формате HEX.
    /// </summary>
    public string? BottomColor { get; init; }

    /// <summary>
    /// Угол поворота градиента по часовой стрелке, в градусах; 0-359. Всегда должно делиться на 45
    /// </summary>
    public int? RotationAngle { get; init; }
}