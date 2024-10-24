using TgInstanceAgent.Application.Abstractions.TelegramClient.Functions.Components.Visitors;

namespace TgInstanceAgent.Application.Abstractions.TelegramClient.Functions.Components;

/// <summary>
/// Абстрактный класс, представляющий фон 
/// </summary>
public abstract class TgInputBackgroundFill
{
    /// <summary>
    /// Метод, принимающий посетителя фона асинхронно
    /// </summary>
    public abstract void Accept(IBackgroundFillVisitor backgroundFillVisitor);
}

/// <summary>
/// Класс, представляющий свободный градиент фона 
/// </summary>
public class TgInputBackgroundFillFreeGradient : TgInputBackgroundFill
{
    /// <summary>
    /// Массив цветов - от 3 до 4 цветов
    /// </summary>
    public required string[] Colors { get; init; }
    
    /// <summary>
    /// Метод, принимающий посетителя фона асинхронно
    /// </summary>
    public override void Accept(IBackgroundFillVisitor backgroundFillVisitor)
    {
        // Вызываем Visit у посетителя с данным типом фона
        backgroundFillVisitor.Visit(this);
    }
}

/// <summary>
/// Класс, представляющий градиент фона 
/// </summary>
public class TgInputBackgroundFillGradient : TgInputBackgroundFill
{
    /// <summary>
    /// Верхний цвет
    /// </summary>
    public required string TopColor { get; init; }
    
    /// <summary>
    /// Нижний цвет
    /// </summary>
    public required string BottomColor { get; init; }
    
    /// <summary>
    /// Угол поворота градиента
    /// </summary>
    public required int RotationAngle { get; init; }
    
    /// <summary>
    /// Метод, принимающий посетителя фона асинхронно
    /// </summary>
    public override void Accept(IBackgroundFillVisitor backgroundFillVisitor)
    {
        // Вызываем Visit у посетителя с данным типом фона
        backgroundFillVisitor.Visit(this);
    }
}

/// <summary>
/// Класс, представляющий сплошной фон 
/// </summary>
public class TgInputBackgroundFillSolid : TgInputBackgroundFill
{
    /// <summary>
    /// Цвет фона.
    /// </summary>
    public required string Color { get; init; }
    
    /// <summary>
    /// Метод, принимающий посетителя фона асинхронно
    /// </summary>
    public override void Accept(IBackgroundFillVisitor backgroundFillVisitor)
    {
        // Вызываем Visit у посетителя с данным типом фона
        backgroundFillVisitor.Visit(this);
    }
}