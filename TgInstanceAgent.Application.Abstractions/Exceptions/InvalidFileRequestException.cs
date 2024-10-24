namespace TgInstanceAgent.Application.Abstractions.Exceptions;

/// <summary>
/// Представляет исключение, которое возникает, если не указан ни один из способов доступа к файлу
/// </summary>
public class InvalidFileRequestException() : Exception("You have not specified any of the ways to access the input file");