using TgInstanceAgent.Application.Abstractions.TelegramClient.Functions.Enums;

namespace TgInstanceAgent.Infrastructure.Web.Chats.InputModels;

/// <summary>
/// Модель входных данных для создания (загрузки) чата
/// </summary>
public class CreateChatInputModel
{
    /// <summary>
    /// Идентификатор объекта, для которого нужно создать чат (пользователь, базовая группа, супергруппа, секретный чат).
    /// </summary>
    public long Id { get; init; }

    /// <summary>
    /// Тип чата.
    /// Нужен для того, чтобы создать или загрузить чат с серверов Telegram.
    /// </summary>
    public TgInputChatType ChatType { get; init; }
}