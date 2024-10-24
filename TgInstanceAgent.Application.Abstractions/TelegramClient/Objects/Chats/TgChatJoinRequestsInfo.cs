namespace TgInstanceAgent.Application.Abstractions.TelegramClient.Objects.Chats;

/// <summary>
/// Содержит информацию о запросах на вступление в чат, ожидающих подтверждения
/// </summary>
public class TgChatJoinRequestsInfo
{
    /// <summary>
    /// Общее количество ожидающих подтверждения запросов на вступление в чат
    /// </summary>
    public required int TotalCount { get; init; }

    /// <summary>
    /// Идентификаторы не более трех пользователей, отправивших самые новые ожидающие подтверждения запросы на вступление в чат
    /// </summary>
    public required long[] UserIds { get; init; }
}