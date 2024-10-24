using TdLib;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Functions.Components;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Functions.Components.Visitors;

namespace TgInstanceAgent.Infrastructure.Telegram.Visitors.Messages;

/// <summary>
/// Реализация посетителя запросов с указанием отправителя сообщения
/// </summary>
public class MessageSenderVisitor : IMessageSenderVisitor
{
    /// <summary>
    /// Отправитель
    /// </summary>
    public TdApi.MessageSender? MessageSender { get; private set; }

    
    /// <summary>
    /// Посетить отправителя, используя идентификатор пользователя.
    /// </summary>
    public void Visit(TgInputMessageSenderUser tgInputMessageSenderUser)
    {
        // Устанавливаем отправителя
        MessageSender = new TdApi.MessageSender.MessageSenderUser
        {
            UserId = tgInputMessageSenderUser.UserId
        };
    }
    
    /// <summary>
    /// Посетить отправителя, используя идентификатор чата.
    /// </summary>
    public void Visit(TgInputMessageSenderChat tgInputMessageSenderChat)
    {
        // Устанавливаем отправителя
        MessageSender = new TdApi.MessageSender.MessageSenderChat
        {
            ChatId = tgInputMessageSenderChat.ChatId
        };
    }
}