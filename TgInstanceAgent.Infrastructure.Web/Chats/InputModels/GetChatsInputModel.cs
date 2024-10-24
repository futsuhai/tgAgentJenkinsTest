using TgInstanceAgent.Application.Abstractions.TelegramClient.Objects.Enums;
using TgInstanceAgent.Infrastructure.Web.Components.Interfaces;

namespace TgInstanceAgent.Infrastructure.Web.Chats.InputModels;

/// <summary>
/// Модель входных данных для получения чатов.
/// </summary>
public class GetChatsInputModel : IWithInputLimit
{
    /// <summary>
    /// Лимит контактов.
    /// </summary>
    public int Limit { get; init; } = 30;

    /// <summary>
    /// Смещение.
    /// </summary>
    public int? Offset { get; init; }

    /// <summary>
    /// Начиная с идентификатора контакта.
    /// </summary>
    public long? FromChatId { get; init; }
    
    /// <summary>
    /// Тип списка
    /// </summary>
    public required TgChatList List { get; init; }
    
    /// <summary>
    /// Идентификатор папки
    /// </summary>
    public int? ChatFolderId { get; init; }
    
    /// <summary>
    /// Флаг, необходимо ли включить в ответ данные пользователей
    /// </summary>
    public bool IncludeUsers { get; init; }
}