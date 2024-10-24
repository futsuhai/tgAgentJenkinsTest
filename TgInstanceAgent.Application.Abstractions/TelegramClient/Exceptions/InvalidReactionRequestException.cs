namespace TgInstanceAgent.Application.Abstractions.TelegramClient.Exceptions;

/// <summary>
/// Представляет исключение, которое возникает, если не указано ни одно свойство, необходимое для реакции
/// </summary>
public class InvalidReactionRequestException() 
    : Exception("You have not specified any of the possible reaction parameters");