using TgInstanceAgent.Application.Abstractions.Interfaces;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Functions.Components;

namespace TgInstanceAgent.Application.Services.Extensions;

/// <summary>
/// Расширение для преобразования параметров отправки сообщения в тип TgInputMessageSendOptions.
/// </summary>
public static class SendMessageOptionsMapper
{
    /// <summary>
    /// Получаем объект параметров отправки сообщения из команды отправки сообщения.
    /// </summary>
    /// <param name="command">Команда отправки сообщения.</param>
    /// <returns>Параметры отправки сообщения.</returns>
    public static TgInputMessageSendOptions GetMessageSendOptions(this IWithSendOptions command)
    {
        // Создаем параметры отправки
        var options = new TgInputMessageSendOptions
        {
            // Флаг - выключить оповещение получателя
            DisableNotification = command.DisableNotification,

            // Флаг - можно ли пересылать, сохранять отправленное сообщение
            ProtectContent = command.ProtectContent,
        };

        // Если указана дата отправки
        if (command.SendOnTime != null)
        {
            // Устанавливаем тип планирования по дате
            options.SchedulingState = new TgInputSchedulingStateAtDate
            {
                // Задаем дату отправки сообщения
                SendDate = command.SendOnTime.Value
            };
        }
        
        // Иначе если установлен флаг отправки по онлайну
        else if(command.SendOnOnline)
        {
            // Устанавливаем тип планирования, когда получаетель станет онлайн 
            options.SchedulingState = new TgInputSchedulingStateWhenOnline();
        }

        // Возвращаем параметры настроек
        return options;
    }
}