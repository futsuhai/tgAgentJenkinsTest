namespace TgInstanceAgent.Application.Abstractions.Exceptions;

/// <summary>
/// Представляет исключение, которое возникает, если не указано ни одно свойство, необходимое для идентификации чата
/// </summary>
public class InvalidChatRequestException()
    : Exception("You have not specified any of the possible chat identification parameters");