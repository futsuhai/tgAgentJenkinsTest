namespace TgInstanceAgent.Application.Abstractions.TelegramClient.Objects.Chats;

/// <summary>
/// Базовый класс для типа чата
/// </summary>
public abstract class TgChatType;

/// <summary>
/// Базовая группа (чат с 0-200 другими пользователями)
/// </summary>
public class TgChatTypeBasicGroup : TgChatType
{
    /// <summary>
    /// Идентификатор базовой группы
    /// </summary>
    public required long BasicGroupId { get; init; }
}

/// <summary>
/// Описывает тип чата
/// </summary>
public class TgChatTypePrivate : TgChatType
{
    /// <summary>
    /// Идентификатор пользователя
    /// </summary>
    public required long UserId { get; init; }
}

/// <summary>
/// Секретный чат с пользователем
/// </summary>
public class TgChatTypeSecret : TgChatType
{
    /// <summary>
    /// Идентификатор секретного чата
    /// </summary>
    public required int SecretChatId { get; init; }

    /// <summary>
    /// Идентификатор пользователя, с которым ведется секретный чат
    /// </summary>
    public required long UserId { get; init; }
}

/// <summary>
/// Супергруппа или канал (с неограниченным количеством участников)
/// </summary>
public class TgChatTypeSupergroup : TgChatType
{
    /// <summary>
    /// Идентификатор супергруппы или канала
    /// </summary>
    public required long SupergroupId { get; init; }

    /// <summary>
    /// True, если супергруппа является каналом
    /// </summary>
    public required bool IsChannel { get; init; }
}