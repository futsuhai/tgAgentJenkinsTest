namespace TgInstanceAgent.Application.Abstractions.TelegramClient.Functions.Components.Visitors;

/// <summary>
/// Интерфейс посетителя фонов.
/// </summary>
public interface IBackgroundFillVisitor
{
    /// <summary>
    /// Метод посещения объектов для типа TgBackgroundFillGradient.
    /// </summary>
    /// <param name="tgBackgroundFillGradient">Объект градиентного заполнения фона.</param>
    /// <returns>Задача, представляющая асинхронное выполнение метода.</returns>
    void Visit(TgInputBackgroundFillGradient tgBackgroundFillGradient);

    /// <summary>
    /// Метод посещения объектов для типа TgBackgroundFillFreeformGradient.
    /// </summary>
    /// <param name="tgBackgroundFillFreeformGradient">Объект свободного градиентного заполнения фона.</param>
    /// <returns>Задача, представляющая асинхронное выполнение метода.</returns>
    void Visit(TgInputBackgroundFillFreeGradient tgBackgroundFillFreeformGradient);

    /// <summary>
    /// Метод посещения объектов для типа TgBackgroundFillSolid.
    /// </summary>
    /// <param name="tgBackgroundFillSolid">Объект сплошного заполнения фона.</param>
    /// <returns>Задача, представляющая асинхронное выполнение метода.</returns>
    void Visit(TgInputBackgroundFillSolid tgBackgroundFillSolid);
}