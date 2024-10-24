namespace TgInstanceAgent.Infrastructure.Web.Components.Interfaces;

/// <summary>
/// Интерфейс содержащий в себе свойства на выбор для установки различных фонов на фотографию профиля
/// </summary>
public interface IWithInputBackground
{
    /// <summary>
    /// Цвет фона в формате HEX
    /// </summary>
    public string? Color { get; set; }
    
    /// <summary>
    /// Список из 3 или 4 цветов градиентов произвольной формы в формате HEX
    /// </summary>
    public string[]? Colors { get; set; }
    
    /// <summary>
    /// Модель входных данных для возможности установки градиентного фона
    /// </summary>
    public BackgroundGradientInputModel? Gradient { get; set; }
}

/// <summary>
///  Модель входных данных для возможности установки градиентного фона
/// </summary>
public class BackgroundGradientInputModel
{
    /// <summary>
    /// Верхний цвет фона в формате HEX.
    /// </summary>
    public string? TopColor { get; set; }

    /// <summary>
    /// Нижний цвет фона в формате HEX.
    /// </summary>
    public string? BottomColor { get; set; }

    /// <summary>
    /// Угол поворота градиента по часовой стрелке, в градусах; 0-359. Всегда должно делиться на 45
    /// </summary>
    public int RotationAngle { get; set; }
}