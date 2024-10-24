using TgInstanceAgent.Application.Abstractions.Exceptions;
using TgInstanceAgent.Application.Abstractions.Interfaces;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Functions.Components;

namespace TgInstanceAgent.Application.Services.Extensions;

/// <summary>
/// Расширяющий класс для преобразования объектов IWithChat в TgInputChat
/// </summary>
public static class ChatMapper
{
    /// <summary>
    /// Преобразует объект IWithChat в TgInputChat
    /// </summary>
    /// <param name="command">Объект IWithChat</param>
    /// <returns>Объект TgInputChat</returns>
    public static TgInputChat GetChat(this IWithChat command)
    {
        // Если объект IWithChat содержит свойство Chat, преобразуем его в TgInputChatId
        if (command.ChatId != null)
            return new TgInputChatId { ChatId = command.ChatId.Value};

        // Если объект IWithChat содержит свойство Username, преобразуем его в TgInputChatUsername
        if (!string.IsNullOrEmpty(command.Username))
            return new TgInputChatUsername { Username = command.Username };

        // Если объект IWithChat содержит свойство PhoneNumber, преобразуем его в TgInputChatPhoneNumber
        if (!string.IsNullOrEmpty(command.PhoneNumber))
            return new TgInputChatPhoneNumber { PhoneNumber = command.PhoneNumber };

        // Если объект IWithChat не содержит нужных свойств, генерируем исключение
        throw new InvalidChatRequestException();
    }
}