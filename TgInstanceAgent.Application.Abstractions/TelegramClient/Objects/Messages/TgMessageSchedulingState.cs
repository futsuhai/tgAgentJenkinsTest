namespace TgInstanceAgent.Application.Abstractions.TelegramClient.Objects.Messages;

/// <summary>
/// Состояние отложенной отправки сообщения
/// </summary>
public abstract class TgMessageSchedulingState;

/// <summary>
/// The message will be sent when the other user is online. Applicable to private chats only and when the exact online status of the other user is known
/// </summary>
public class TgMessageSchedulingStateSendWhenOnline : TgMessageSchedulingState;

/// <summary>
/// Contains information about the time when a scheduled message will be sent
/// </summary>
public class TgMessageSchedulingStateSendAtDate : TgMessageSchedulingState
{
    /// <summary>
    /// Point in time (Unix timestamp) when the message will be sent. The date must be within 367 days in the future
    /// </summary>
    public required DateTime SendDate { get; init; }
}