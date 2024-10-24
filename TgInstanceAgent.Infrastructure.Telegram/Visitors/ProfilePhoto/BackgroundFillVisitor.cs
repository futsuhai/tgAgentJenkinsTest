using TdLib;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Functions.Components;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Functions.Components.Visitors;

namespace TgInstanceAgent.Infrastructure.Telegram.Visitors.ProfilePhoto;

/// <summary>
/// Посетитель для обработки фонов.
/// </summary>
public class BackgroundFillVisitor : IBackgroundFillVisitor
{
    /// <summary>
    /// Заполнение фона.
    /// </summary>
    public TdApi.BackgroundFill? BackgroundFill { get; private set; }

    /// <summary>
    /// Метод посещения объектов для типа TgBackgroundFillGradient.
    /// </summary>
    /// <param name="tgBackgroundFillGradient">Объект градиентного заполнения фона.</param>
    /// <returns>Задача, представляющая асинхронное выполнение метода.</returns>
    public void Visit(TgInputBackgroundFillGradient tgBackgroundFillGradient)
    {
        // Создаем объект градиентного заполнения фона
        BackgroundFill = new TdApi.BackgroundFill.BackgroundFillGradient
        {
            // Преобразование верхнего цвета из HEX в RGB
            TopColor = HexToRgb(tgBackgroundFillGradient.TopColor),
            
            // Преобразование нижнего цвета из HEX в RGB
            BottomColor = HexToRgb(tgBackgroundFillGradient.BottomColor),
            
            // Угол поворота
            RotationAngle = tgBackgroundFillGradient.RotationAngle
        };
    }

    /// <summary>
    /// Метод посещения объектов для типа TgBackgroundFillFreeformGradient.
    /// </summary>
    /// <param name="tgBackgroundFillFreeformGradient">Объект свободного градиентного заполнения фона.</param>
    /// <returns>Задача, представляющая асинхронное выполнение метода.</returns>
    public void Visit(TgInputBackgroundFillFreeGradient tgBackgroundFillFreeformGradient)
    {
        // Создание объекта свободного градиентного заполнения фона
        BackgroundFill = new TdApi.BackgroundFill.BackgroundFillFreeformGradient
        {
            // Преобразование списка цветов из HEX в RGB и преобразование его в массив
            Colors = tgBackgroundFillFreeformGradient.Colors.Select(HexToRgb).ToArray()
        };
    }

    /// <summary>
    /// Метод посещения объектов для типа TgBackgroundFillSolid.
    /// </summary>
    /// <param name="tgBackgroundFillSolid">Объект сплошного заполнения фона.</param>
    /// <returns>Задача, представляющая асинхронное выполнение метода.</returns>
    public void Visit(TgInputBackgroundFillSolid tgBackgroundFillSolid)
    {
        // Создание объекта сплошного заполнения фона
        BackgroundFill = new TdApi.BackgroundFill.BackgroundFillSolid
        {
            // Преобразование цвета из HEX в RGB
            Color = HexToRgb(tgBackgroundFillSolid.Color)
        };
    }
        
    /// <summary>
    /// Преобразование HEX цвета в RGB.
    /// </summary>
    /// <param name="hexColor">HEX представление цвета.</param>
    /// <returns>Целое число, представляющее RGB значение цвета.</returns>
    private static int HexToRgb(string hexColor)
    {
        // Удаляем символ # из строки, если есть
        hexColor = hexColor.TrimStart('#'); 

        // Парсим каждые два символа HEX строки и преобразуем в целое число
        var r = Convert.ToInt32(hexColor.Substring(0, 2), 16);
        var g = Convert.ToInt32(hexColor.Substring(2, 2), 16);
        var b = Convert.ToInt32(hexColor.Substring(4, 2), 16);

        // Собираем RGB24 значение, объединяя значения R, G и B
        var rgb24 = (r << 16) + (g << 8) + b;

        // Возвращаем значение
        return rgb24;
    }
}