﻿namespace TgInstanceAgent.Infrastructure.Storage.Entities.Instances;

/// <summary>
/// Модель данных для настроек вебхука, включающая флаги для различных типов данных, которые могут быть обработаны вебхуком.
/// </summary>
public class WebhookSettingModel
{
    /// <summary>
    /// Флаг для включения или отключения обработки сообщений через вебхук.
    /// Указывает, будут ли сообщения передаваться через вебхук.
    /// </summary>
    public bool Messages { get; set; }

    /// <summary>
    /// Флаг для включения или отключения обработки статусов через вебхук.
    /// Указывает, будут ли статусы передаваться через вебхук.
    /// </summary>
    public bool Chats { get; set; }

    /// <summary>
    /// Флаг для включения или отключения обработки пользователей через вебхук.
    /// Указывает, будут ли данные пользователей передаваться через вебхук.
    /// </summary>
    public bool Users { get; set; }
    
    /// <summary>
    /// Флаг для включения или отключения обработки файдлв через вебхук.
    /// Указывает, будут ли данные файлов передаваться через вебхук.
    /// </summary>
    public bool Files { get; set; }
    
    /// <summary>
    /// Флаг для включения или отключения обработки историй через вебхук.
    /// Указывает, будут ли данные об историях передаваться через вебхук.
    /// </summary>
    public bool Stories { get; set; }

    /// <summary>
    /// Флаг для включения или отключения обработки других типов данных через вебхук.
    /// Указывает, будут ли другие данные передаваться через вебхук.
    /// </summary>
    public bool Other { get; set; }

    /// <summary>
    /// Идентификатор инстанса, к которому принадлежат данные настройки вебхука.
    /// Уникальный идентификатор инстанса для связывания настроек вебхука с конкретным инстансом.
    /// </summary>
    public Guid InstanceId { get; set; }
}