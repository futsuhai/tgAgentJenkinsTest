namespace TgInstanceAgent.Application.Abstractions.TelegramClient.Objects.Components;

/// <summary>
/// Тип фона
/// </summary>
public abstract class TgBackgroundType;

/// <summary>
/// Фон из темы чата;
/// Может быть использован только как фон чата в каналах
/// </summary>
public class TgBackGroundTypeChatTheme : TgBackgroundType
{
    /// <summary>
    /// Имя темы чата
    /// </summary>
    public required string ThemeName { get; init; }
}

/// <summary>
/// Залитый(filled) фон
/// </summary>
public class TgBackgroundTypeFill : TgBackgroundType
{
    /// <summary>
    /// Заливка фона (the fill)
    /// </summary>
    public required TgBackgroundFill Fill { get; init; }
}

/// <summary>
/// PNG или TGV(gzipped subset of SVG with MIME type "application/x-tgwallpattern")
/// паттерн, который будет комбинироваться с фоновой заливкой, выбранной пользователем
/// </summary>
public class TgBackgroundTypePattern : TgBackgroundType
{
    /// <summary>
    /// Заливка фона
    /// </summary>
    public required TgBackgroundFill Fill { get; init; }
    
    /// <summary>
    /// Интенсивность паттерна, когда он показан поверх заливки фона;
    /// 0-100;
    /// </summary>
    public required int Intensity { get; init; }
    
    
    /// <summary>
    /// True, если фоновая заливка должна применяться только к самому паттерну.
    /// Все остальные пиксели в этом случае будут черными.
    /// Только для темных тем
    /// </summary>
    public required bool IsInverted { get; init; }
    
    /// <summary>
    /// True, если фон должен быть слегка сдвинут при наклоне устройства
    /// </summary>
    public required bool IsMoving { get; init; }
}

/// <summary>
/// Описание типа фона
/// </summary>
public class TgBackgroundTypeWallpaper : TgBackgroundType
{
    /// <summary>
    /// True, если обои должны быть уменьшены,
    /// чтобы поместиться в квадрат 450x450, а затем размыты боксом с радиусом 12
    /// </summary>
    public required bool IsBlurred { get; init; }
    
    /// <summary>
    /// True, если фон должен быть слегка сдвинут при наклоне устройства
    /// </summary>
    public required bool IsMoving { get; init; }
}