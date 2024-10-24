using TgInstanceAgent.Application.Abstractions.Interfaces;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Exceptions;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Functions.Components;

namespace TgInstanceAgent.Application.Services.Extensions; 

/// <summary>
/// Расширение для преобразования данных фона в соответствующий тип TgInputBackgroundFill.
/// </summary>
public static class BackgroundFillMapper 
{
    /// <summary>
    /// Получаем объект фона из команды установки фотографии.
    /// </summary>
    /// <param name="command">Команда установки профильной фотографии.</param>
    /// <returns>Объект фона профильной фотографии.</returns>
    public static TgInputBackgroundFill GetBackgroundFill(this IWithBackgroundFill command) 
    {
        // Если указан сплошной цвет фона
        if (!string.IsNullOrEmpty(command.Color)) 
        {
            // Возвращаем объект типа TgBackgroundFillSolid 
            return new TgInputBackgroundFillSolid 
            {
                // Задаем цвета фона
                Color = command.Color 
            };
        }

        // Если указан список цветов для свободного градиента
        if (command.Colors != null) 
        {
            // Возвращаем объект типа TgBackgroundFillFreeformGradient 
            return new TgInputBackgroundFillFreeGradient 
            {
                // Задаем список цветов фона
                Colors = command.Colors! 
            };
        }

        if (command.Gradient != null)
        {
            // Если указан градиент фона
            if (!string.IsNullOrEmpty(command.Gradient.BottomColor)
                && !string.IsNullOrEmpty(command.Gradient.TopColor)) 
            {
                // Возвращаем объект типа TgBackgroundFillGradient 
                return new TgInputBackgroundFillGradient 
                {
                    // Задаем верхний цвет градиента
                    TopColor = command.Gradient!.TopColor!, 
                
                    // Задаем нижний цвет градиента
                    BottomColor = command.Gradient.BottomColor!,
                
                    // Задаем угл поворота градиента
                    RotationAngle = command.Gradient.RotationAngle!.Value 
                };
            }
        }
        
        // Вызываем исключение, если не удалось определить тип фона.
        throw new InvalidBackgroundFillException(); 
    }
}