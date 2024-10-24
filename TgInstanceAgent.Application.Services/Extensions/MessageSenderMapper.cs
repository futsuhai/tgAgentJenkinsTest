using TgInstanceAgent.Application.Abstractions.Interfaces;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Functions.Components;

namespace TgInstanceAgent.Application.Services.Extensions;

/// <summary>
/// Расширяющий класс для преобразования объектов IWithMessageSender в TgInputMessageSender
/// </summary>
public static class MessageSenderMapper
{
    /// <summary>
    /// Преобразует объект IWithMessageSender в TgInputMessageSender
    /// </summary>
    /// <param name="command">Объект IWithMessageSender</param>
    /// <returns>Объект TgInputMessageSender</returns>
    public static TgInputMessageSender? GetMessageSender(this IWitMessageSender command)
    {
        // Если объект IWithMessageSender содержит свойства SenderId и TgMessageSenderType
        // Преобразовываем их в TgInputMessageSender
        if (command.SenderChatId.HasValue)
        {
            return new TgInputMessageSenderChat { ChatId = command.SenderChatId.Value };
        }
        
        if (command.SenderUserId.HasValue)
        {
            return new TgInputMessageSenderUser() { UserId = command.SenderUserId.Value };
        }

        return null;
    }
}