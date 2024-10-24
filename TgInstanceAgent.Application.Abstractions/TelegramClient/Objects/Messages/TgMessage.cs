using TgInstanceAgent.Application.Abstractions.TelegramClient.Objects.Components;

namespace TgInstanceAgent.Application.Abstractions.TelegramClient.Objects.Messages;

/// <summary>
/// Класс сообщения.
/// </summary>
public class TgMessage
{
    /// <summary>
    /// Данные чата.
    /// </summary>
    public required long ChatId { get; init; }

    /// <summary>
    /// Идентификатор отправителя.
    /// </summary>
    public required TgMessageSender SenderId { get; init; }

    /// <summary>
    /// Идентификатор сообщения.
    /// </summary>
    public required long Id { get; init; }

    /// <summary>
    /// Является исходящим сообщением.
    /// </summary>
    public required bool IsOutgoing { get; init; }

    /// <summary>
    /// Может быть отредактировано.
    /// </summary>
    public required bool CanBeEdited { get; init; }

    /// <summary>
    /// Может быть переслано.
    /// </summary>
    public required bool CanBeForwarded { get; init; }

    /// <summary>
    /// Может быть удалено для всех пользователей.
    /// </summary>
    public required bool CanBeDeletedForAllUsers { get; init; }

    /// <summary>
    /// True, if the message is pinned
    /// </summary>
    public required bool IsPinned { get; init; }
    
    /// <summary>
    /// True, if the list of added reactions is available through getMessageAddedReactions
    /// </summary>
    public required bool CanGetAddedReactions { get; init; }

    /// <summary>
    /// True, if the message statistics are available through getMessageStatistics
    /// </summary>
    public required bool CanGetStatistics { get; init; }
    
    /// <summary>
    /// True, if read date of the message can be received through getMessageReadDate
    /// </summary>
    public required bool CanGetReadDate { get; init; }

    /// <summary>
    /// True, if chat members already viewed the message can be received through getMessageViewers
    /// </summary>
    public required bool CanGetViewers { get; init; }
    
    /// <summary>
    /// True, if the message is a channel post. All messages to channels are channel posts, all other messages are not channel posts
    /// </summary>
    public required bool IsChannelPost { get; init; }

    /// <summary>
    /// True, if the message is a forum topic message
    /// </summary>
    public required bool IsTopicMessage { get; init; }
    
    /// <summary>
    /// If non-zero, the identifier of the message thread the message belongs to; unique within the chat to which the message belongs
    /// </summary>
    public long? MessageThreadId { get; init; }
    
    /// <summary>
    /// For channel posts and anonymous group messages, optional author signature
    /// </summary>
    public string? AuthorSignature { get; init; }

    /// <summary>
    /// Unique identifier of an album this message belongs to; 0 if none. Only audios, documents, photos and videos can be grouped together in albums
    /// </summary>
    public long? MediaAlbumId { get; init; }

    /// <summary>
    /// Дата сообщения.
    /// </summary>
    public required DateTime Date { get; init; }

    /// <summary>
    /// Дата редактирования.
    /// </summary>
    public DateTime? EditDate { get; init; }

    /// <summary>
    /// Ответ на сообщение.
    /// </summary>
    public TgMessageReplyTo? ReplyTo { get; init; }

    /// <summary>
    /// Информация о пересылаемом сообщении.
    /// </summary>
    public TgForwardInfo? ForwardInfo { get; init; }
    
    /// <summary>
    /// Содержит информацию о взаимодействиях с сообщением
    /// </summary>
    public TgMessageInteractionInfo? InteractionInfo { get; init; }

    /// <summary>
    /// The scheduling state of the message; may be null if the message isn't scheduled
    /// </summary>
    public TgMessageSchedulingState? SchedulingState { get; init; }
    
    /// <summary>
    /// Данные сообщения.
    /// </summary>
    public required TgMessageContent Content { get; init; }
}
