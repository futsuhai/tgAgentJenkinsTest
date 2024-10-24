using TgInstanceAgent.Application.Abstractions.TelegramClient.Functions.Enums;

namespace TgInstanceAgent.Application.Abstractions.TelegramClient.Functions.Chats;

/// <summary>
/// Запрос на получение списка чатов
/// </summary>
public class TgCreateChatRequest
{
    /// <summary>
    /// Идентификатор объекта, для которого нужно создать чат (пользователь, базовая группа, супергруппа, секретный чат).
    /// </summary>
    public required long Id { get; init; }

    /// <summary>
    /// Тип чата.
    /// Нужен для того, чтобы создать или загрузить чат с серверов Telegram.
    /// </summary>
    public required TgInputChatType ChatType { get; init; }
}