namespace TgInstanceAgent.Application.Abstractions.TelegramClient.Objects.Components;

/// <summary>
/// Абстрактный класс заливки фона
/// </summary>
public abstract class TgBackgroundFill;

/// <summary>
/// Описание freeform градиента фона
/// </summary>
public class TgBackgroundFillFreeformGradient : TgBackgroundFill
{
    /// <summary>
    /// Список трех или четырех цветов freeform градиента в формате RGB24
    /// </summary>
    public required int[] Colors { get; init; }
}

/// <summary>
/// Описание градиентной заливки фона
/// </summary>
public class TgBackgroundFillGradient : TgBackgroundFill
{
    /// <summary>
    /// Верхний цвет фона в формате RGB24
    /// </summary>
    public required int TopColor { get; init; }
    
    /// <summary>
    /// Нижний цвет фона в формате RGB24
    /// </summary>
    public required int BottomColor { get; init; }
    
    /// <summary>
    /// Угол поворота градиента по часовой стрелке, в градусах; 0-359.
    /// Всегда должен быть кратен 45
    /// </summary>
    public required int RotationAngle { get; init; }
}

/// <summary>
/// Описание заливки фона
/// </summary>
public class TgBackgroundFillSolid : TgBackgroundFill
{
    /// <summary>
    /// Цвет заливки фона в формате RGB24
    /// </summary>
    public required int Color { get; init; }
}
