namespace TgInstanceAgent.Application.Abstractions.TelegramClient.Objects.Messages;

/// <summary>
/// Информация о пересылаемом сообщении.
/// </summary>
public abstract class TgMessageOrigin;

/// <summary>
/// Информация о пересылаемом сообщении чата.
/// </summary>
public class TgMessageOriginChat : TgMessageOrigin
{
    /// <summary>
    /// Подпись автора.
    /// </summary>
    public string? AuthorSignature { get; init; }

    /// <summary>
    /// Идентификатор чата отправителя.
    /// </summary>
    public required long SenderChatId { get; init; }
}

/// <summary>
/// Информация о пересылаемом сообщении пользователя.
/// </summary>
public class TgMessageOriginUser : TgMessageOrigin
{
    /// <summary>
    /// Идентификатор чата отправителя.
    /// </summary>
    public required long SenderUserId { get; init; }
}

/// <summary>
/// Информация о пересылаемом сообщении скрытого пользователя.
/// </summary>
public class TgMessageOriginHiddenUser : TgMessageOrigin
{
    /// <summary>
    /// Имя отправителя.
    /// </summary>
    public string? SenderName { get; init; }
}

/// <summary>
/// Информация о пересылаемом сообщении канала.
/// </summary>
public class TgMessageOriginChannel : TgMessageOrigin
{
    /// <summary>
    /// Подпись автора.
    /// </summary>
    public string? AuthorSignature { get; init; }

    /// <summary>
    /// Идентификатор чата отправителя.
    /// </summary>
    public required long ChatId { get; init; }

    /// <summary>
    /// Идентификатор сообщения отправителя.
    /// </summary>
    public required long MessageId { get; init; }
}