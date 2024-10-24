namespace TgInstanceAgent.Application.Abstractions.TelegramClient.Functions.Components.Visitors;

/// <summary>
/// Интерфейс посетителя запросов с указанием чата
/// </summary>
public interface IChatVisitor
{
    /// <summary>
    /// Посетить чат, используя идентификатор.
    /// </summary>
    /// <param name="tgChatId">Чат с идентификатором.</param>
    Task VisitAsync(TgInputChatId tgChatId);

    /// <summary>
    /// Посетить чат, используя имя пользователя.
    /// </summary>
    /// <param name="tgChatUsername">Чат с именем пользователя.</param>
    Task VisitAsync(TgInputChatUsername tgChatUsername);

    /// <summary>
    /// Посетить чат, используя номер телефона.
    /// </summary>
    /// <param name="tgChatPhone">Чат с номером телефона.</param>
    Task VisitAsync(TgInputChatPhoneNumber tgChatPhone);
}