namespace TgInstanceAgent.Application.Abstractions.TelegramClient.Functions.Components.Visitors;

/// <summary>
/// Интерфейс посетителя запросов с указанием отправителя сообщения
/// </summary>
public interface IMessageSenderVisitor
{
    /// <summary>
    /// Посетить пользователя, используя идентификатор.
    /// </summary>
    /// <param name="tgMessageSenderUser">Чат с идентификатором.</param>
    void Visit(TgInputMessageSenderUser tgMessageSenderUser);

    /// <summary>
    /// Посетить чат, используя идентификатор.
    /// </summary>
    /// <param name="tgMessageSenderChat">Чат с именем пользователя.</param>
    void Visit(TgInputMessageSenderChat tgMessageSenderChat);
}