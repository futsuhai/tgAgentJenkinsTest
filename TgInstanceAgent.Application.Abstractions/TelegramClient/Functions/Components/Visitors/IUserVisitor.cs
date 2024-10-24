namespace TgInstanceAgent.Application.Abstractions.TelegramClient.Functions.Components.Visitors;

/// <summary>
/// Интерфейс посетителя запросов с указанием чата
/// </summary>
public interface IUserVisitor
{
    /// <summary>
    /// Посетить чат, используя идентификатор.
    /// </summary>
    /// <param name="tgChatId">Чат с идентификатором.</param>
    Task VisitAsync(TgInputUserId tgChatId);

    /// <summary>
    /// Посетить чат, используя имя пользователя.
    /// </summary>
    /// <param name="tgChatUsername">Чат с именем пользователя.</param>
    Task VisitAsync(TgInputUserUsername tgChatUsername);

    /// <summary>
    /// Посетить чат, используя номер телефона.
    /// </summary>
    /// <param name="tgChatPhone">Чат с номером телефона.</param>
    Task VisitAsync(TgInputUserPhoneNumber tgChatPhone);
}