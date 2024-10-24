namespace TgInstanceAgent.Application.Abstractions.TelegramClient.Exceptions;

/// <summary>
/// Представляет исключение, которое возникает, если не указано ни одно свойство, необходимое для фона фотографии профиля
/// </summary>
public class InvalidBackgroundFillException()
    : Exception("None of the mandatory parameters are specified for filling the background of the chat profile photo");