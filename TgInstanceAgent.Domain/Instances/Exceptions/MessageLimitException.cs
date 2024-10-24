namespace TgInstanceAgent.Domain.Instances.Exceptions;

/// <summary>
/// Исключение, возникающее, при отправке сообщения, если их лимит исчерпан
/// </summary>
public class MessageLimitException() : Exception("Message limit reached");