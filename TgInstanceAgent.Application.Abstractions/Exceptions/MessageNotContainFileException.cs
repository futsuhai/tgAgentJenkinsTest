namespace TgInstanceAgent.Application.Abstractions.Exceptions;

/// <summary>
/// Представляет исключение, которое вызывается, когда не сообщение не содержит в себе какого-либо файла.
/// </summary>
public class MessageNotContainFileException() : Exception("The message does not contain a inputFile");