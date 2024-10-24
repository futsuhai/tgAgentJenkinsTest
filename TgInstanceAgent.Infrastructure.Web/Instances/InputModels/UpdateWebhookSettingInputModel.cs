﻿namespace TgInstanceAgent.Infrastructure.Web.Instances.InputModels;

/// <summary>
/// Модель ввода для обновления настроек вебхука.
/// </summary>
public class UpdateWebhookSettingInputModel
{
    /// <summary>
    /// Флаг для включения или отключения обработки сообщений через вебхук.
    /// Указывает, будут ли сообщения передаваться через вебхук.
    /// </summary>
    public bool Messages { get; init; }

    /// <summary>
    /// Флаг для включения или отключения обработки статусов через вебхук.
    /// Указывает, будут ли статусы передаваться через вебхук.
    /// </summary>
    public bool Chats { get; init; }

    /// <summary>
    /// Флаг для включения или отключения обработки пользователей через вебхук.
    /// Указывает, будут ли данные пользователей передаваться через вебхук.
    /// </summary>
    public bool Users { get; init; }
    
    /// <summary>
    /// Флаг для включения или отключения обработки файдлв через вебхук.
    /// Указывает, будут ли данные файлов передаваться через вебхук.
    /// </summary>
    public bool Files { get; init; }
    
    /// <summary>
    /// Флаг для включения или отключения обработки историй через вебхук.
    /// Указывает, будут ли данные об историях передаваться через вебхук.
    /// </summary>
    public bool Stories { get; init; }

    /// <summary>
    /// Флаг для включения или отключения обработки других типов данных через вебхук.
    /// Указывает, будут ли другие данные передаваться через вебхук.
    /// </summary>
    public bool Other { get; init; }
}