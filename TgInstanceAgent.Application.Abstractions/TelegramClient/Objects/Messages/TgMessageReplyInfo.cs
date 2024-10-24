using TgInstanceAgent.Application.Abstractions.TelegramClient.Objects.Components;

namespace TgInstanceAgent.Application.Abstractions.TelegramClient.Objects.Messages;

/// <summary>
/// Информация об ответах на сообщение.
/// </summary>
public class TgMessageReplyInfo
{
    /// <summary>
    /// Количество ответов
    /// </summary>
    public required int ReplyCount { get; init; }
    
    /// <summary>
    /// Массив последних пользователей, которые ответили на сообщение.
    /// </summary>
    public TgMessageSender[]? RecentReplierIds { get; init; }
    
    /// <summary>
    /// Идентификатор последнего прочитанного входящего сообщения.
    /// </summary>
    public long? LastReadInboxMessageId { get; init; }

    /// <summary>
    /// Идентификатор последнего прочитанного исходящего сообщения.
    /// </summary>
    public long? LastReadOutboxMessageId { get; init; }

    /// <summary>
    /// Идентификатор последнего сообщения в ответах.
    /// </summary>
    public long? LastMessageId { get; init; }

}