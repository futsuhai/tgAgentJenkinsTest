using TgInstanceAgent.Application.Abstractions.Exceptions;
using TgInstanceAgent.Application.Abstractions.Interfaces;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Functions.Components;

namespace TgInstanceAgent.Application.Services.Extensions;

/// <summary>
/// Расширяющий класс для преобразования объектов IWithUser в TgInputUser
/// </summary>
public static class UserMapper
{
    /// <summary>
    /// Преобразует объект IWithUser в TgInputUser
    /// </summary>
    /// <param name="command">Объект IWithUser</param>
    /// <returns>Объект TgInputUser</returns>
    public static TgInputUser GetUser(this IWithUser command)
    {
        // Если объект IWithUser содержит свойство UserId, преобразуем его в TgInputUserId
        if (command.UserId != null)
            return new TgInputUserId { UserId = command.UserId.Value };

        // Если объект IWithUser содержит свойство Username, преобразуем его в TgInputUserUsername
        if (!string.IsNullOrEmpty(command.Username))
            return new TgInputUserUsername { Username = command.Username };

        // Если объект IWithUser содержит свойство PhoneNumber, преобразуем его в TgInputUserPhoneNumber
        if (!string.IsNullOrEmpty(command.PhoneNumber))
            return new TgInputUserPhoneNumber { PhoneNumber = command.PhoneNumber };

        // Если объект IWithUser не содержит нужных свойств, генерируем исключение
        throw new InvalidChatRequestException();
    }
}