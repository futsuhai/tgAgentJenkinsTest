using MediatR;
using TgInstanceAgent.Application.Abstractions.Interfaces;

namespace TgInstanceAgent.Application.Abstractions.Commands.Instances;

/// <summary>
/// Команда для обновления настроек вебхука для конкретного инстанса.
/// </summary>
public class UpdateWebhookSettingCommand : IRequest, IWithCommandId, IWithInstanceId
{
    /// <summary>
    /// Уникальный идентификатор команды.
    /// </summary>
    public required Guid CommandId { get; init; }
    
    /// <summary>
    /// Уникальный идентификатор инстанса, для которого обновляются настройки вебхука.
    /// </summary>
    public required Guid InstanceId { get; init; }

    /// <summary>
    /// Флаг для включения или отключения обработки сообщений через вебхук.
    /// Указывает, будут ли сообщения передаваться через вебхук.
    /// </summary>
    public required bool Messages { get; init; }

    /// <summary>
    /// Флаг для включения или отключения обработки статусов через вебхук.
    /// Указывает, будут ли статусы передаваться через вебхук.
    /// </summary>
    public required bool Chats { get; init; }

    /// <summary>
    /// Флаг для включения или отключения обработки пользователей через вебхук.
    /// Указывает, будут ли данные пользователей передаваться через вебхук.
    /// </summary>
    public required bool Users { get; init; }
    
    /// <summary>
    /// Флаг для включения или отключения обработки файдлв через вебхук.
    /// Указывает, будут ли данные файлов передаваться через вебхук.
    /// </summary>
    public required bool Files { get; init; }
    
    /// <summary>
    /// Флаг для включения или отключения обработки историй через вебхук.
    /// Указывает, будут ли данные об историях передаваться через вебхук.
    /// </summary>
    public required bool Stories { get; init; }

    /// <summary>
    /// Флаг для включения или отключения обработки других типов данных через вебхук.
    /// Указывает, будут ли другие данные передаваться через вебхук.
    /// </summary>
    public required bool Other { get; init; }
}